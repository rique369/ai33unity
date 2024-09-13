# -*- coding: utf-8 -*-


import sqlite3

import requests

import numpy as np

import pandas as pd

from datetime import datetime

from tensorflow.keras.models import Sequential

from tensorflow.keras.layers import LSTM, Dense, Dropout, Input

from sklearn.preprocessing import MinMaxScaler

import pandas_ta as ta

import tensorflow as tf


# Funcao para coletar dados dos endpoints

def fetch_data():

    users_url = 'https://orbitsprint.cloud/ai/10user.php'

    swap_url = 'https://orbitsprint.cloud/ai/swap.php'

    btc_url = 'https://orbitsprint.cloud/ai/btc.php'


    users_data = requests.get(users_url).json()

    swap_data = requests.get(swap_url).json()

    btc_data = requests.get(btc_url).json()


    users_value = users_data['value'] if isinstance(users_data, dict) and 'value' in users_data else users_data

    swap_value = swap_data['value'] if isinstance(swap_data, dict) and 'value' in swap_data else swap_data

    btc_value = btc_data['price_change_percentage_24h'] if isinstance(btc_data, dict) and 'price_change_percentage_24h' in btc_data else btc_data


    return users_value, swap_value, btc_value


# Conectar ao banco de dados SQLite

conn = sqlite3.connect('historical_data.db')

c = conn.cursor()


# Criar tabela para armazenar dados historicos se ainda nao existir

c.execute('''

    CREATE TABLE IF NOT EXISTS historical_data (

        date TEXT,

        users INTEGER,

        swaps INTEGER,

        btc_variation REAL,

        rsi REAL,

        macd REAL,

        macd_signal REAL,

        withdrawal_rate REAL

    )

''')

conn.commit()


# Inserir dados simulados para exemplo

def insert_simulated_data():

    data = {

        'date': pd.date_range(start='1/1/2023', periods=100, freq='D'),

        'users': np.random.randint(100, 200, 100),

        'swaps': np.random.randint(50, 150, 100),

        'btc_variation': np.random.uniform(-5, 5, 100),

        'withdrawal_rate': np.random.uniform(1, 5, 100)

    }


    df = pd.DataFrame(data)


    # Calcular RSI e MACD

    df['rsi'] = ta.rsi(df['btc_variation'], length=14)

    macd = ta.macd(df['btc_variation'], fast=12, slow=26, signal=9)

    df['macd'] = macd['MACD_12_26_9']

    df['macd_signal'] = macd['MACDs_12_26_9']


    df.to_sql('historical_data', conn, if_exists='append', index=False)


# Descomente a linha abaixo para inserir dados simulados inicialmente

# insert_simulated_data()


# Carregar dados historicos do banco de dados

df = pd.read_sql('SELECT * FROM historical_data', conn)


# Verificar se o DataFrame esta vazio e remover linhas com valores NaN

if df.empty:

    raise ValueError("O DataFrame carregado do banco de dados esta vazio!")

df = df.dropna()


# Calcular o volume medio de swaps

average_swaps = df['swaps'].mean()


# Normalizar os dados

scaler = MinMaxScaler(feature_range=(0, 1))

scaled_data = scaler.fit_transform(df[['users', 'swaps', 'btc_variation', 'rsi', 'macd', 'macd_signal', 'withdrawal_rate']])


# Preparar dados para treinamento (usando 60 dias de historico para prever o proximo dia)

def create_dataset(dataset, look_back=60):

    if dataset.shape[0] <= look_back:

        raise ValueError("O tamanho do dataset e menor ou igual ao look_back.")

    

    X, Y = [], []

    for i in range(len(dataset)-look_back):

        a = dataset[i:(i+look_back), :]

        X.append(a)

        Y.append(dataset[i + look_back, 6])  # Prevendo a taxa de saque

    return np.array(X), np.array(Y)


look_back = 60

X, Y = create_dataset(scaled_data, look_back)


# Verificar se ha dados suficientes para dividir

if len(X) == 0 or len(Y) == 0:

    raise ValueError("Nao ha dados suficientes apos criar o conjunto de dados.")


# Dividir os dados em treino e teste

train_size = int(len(X) * 0.8)

test_size = len(X) - train_size

X_train, X_test = X[:train_size], X[train_size:]

Y_train, Y_test = Y[:train_size], Y[train_size:]


# Construir o modelo LSTM

model = Sequential()

model.add(Input(shape=(look_back, 7)))

model.add(LSTM(50, return_sequences=True))

model.add(Dropout(0.2))

model.add(LSTM(50, return_sequences=False))

model.add(Dropout(0.2))

model.add(Dense(25))

model.add(Dense(1))


model.compile(optimizer='adam', loss='mean_squared_error')


# Prevenindo retracing excessivo

@tf.function

def predict_with_model(model, X_data):

    return model(X_data, training=False)


# Treinar o modelo

model.fit(X_train, Y_train, batch_size=1, epochs=10)


# Prever a taxa de saque

predicted_withdrawal_rate = predict_with_model(model, X_test)

predicted_withdrawal_rate = scaler.inverse_transform(

    np.concatenate((X_test[:, -1, :-1], predicted_withdrawal_rate.numpy()), axis=1)

)[:, -1]


# Funcao para ajustar a taxa de saque

def adjust_withdrawal_rate(current_rate, user_data, swap_data, btc_data, predicted_rate, average_swaps):

    adjustment = 0.02

    max_rate = 0.50

    min_rate = 0.01


    # Calcular volume relativo

    volume_relative = swap_data / average_swaps


    # Ajustar a taxa de saque com base no volume relativo e outros fatores

    if volume_relative > 1 or user_data > 20:

        new_rate = min(current_rate + adjustment, max_rate)

    else:

        new_rate = max(current_rate - adjustment, min_rate)


    if btc_data > 0:

        new_rate = max(new_rate - adjustment, min_rate)

    else:

        new_rate = min(new_rate + adjustment, max_rate)


    return new_rate


# Funcao para enviar a taxa de saque ajustada para o site

def send_withdrawal_rate(url, rate):

    payload = {'rate': rate}

    response = requests.post(url, json=payload)

    if response.status_code == 200:

        print(f'Taxa de saque enviada com sucesso: {rate:.2%}')

    else:

        print(f'Erro ao enviar a taxa de saque: {response.status_code}')


# Atualizar a taxa de saque com base nos dados mais recentes

users_data, swap_data, btc_data = fetch_data()

current_rate = 0.05  # Exemplo de taxa atual

new_rate = adjust_withdrawal_rate(current_rate, users_data, swap_data, btc_data, predicted_withdrawal_rate[-1], average_swaps)

print(f'Nova taxa de saque: {new_rate:.2%}')


# Enviar a nova taxa de saque para o site

send_withdrawal_rate('https://orbitsprint.cloud/taxa33.php', new_rate)


# Adicionar novo dado ao banco de dados

new_data = {

    'date': datetime.now().strftime('%Y-%m-%d'),

    'users': users_data,

    'swaps': swap_data,

    'btc_variation': btc_data,

    'rsi': ta.rsi([btc_data], length=14).iloc[0] if len([btc_data]) >= 14 else 0,

    'macd': ta.macd([btc_data], fast=12, slow=26, signal=9)['MACD_12_26_9'].iloc[0] if len([btc_data]) >= 26 else 0,

    'macd_signal': ta.macd([btc_data], fast=12, slow=26, signal=9)['MACDs_12_26_9'].iloc[0] if len([btc_data]) >= 26 else 0,

    'withdrawal_rate': new_rate

}

c.execute('''

    INSERT INTO historical_data (date, users, swaps, btc_variation, rsi, macd, macd_signal, withdrawal_rate)

    VALUES (?, ?, ?, ?, ?, ?, ?, ?)

''', (new_data['date'], new_data['users'], new_data['swaps'], new_data['btc_variation'], new_data['rsi'], new_data['macd'], new_data['macd_signal'], new_data['withdrawal_rate']))

conn.commit()


# Fechar a conexao com o banco de dados

conn.close()
