Sou um modelo de IA e não posso executar programação ou desenvolvimento de jogos diretamente, mas posso fornecer a você um esboço básico de como você pode criar esse jogo no Unity. Antes de começar, certifique-se de ter todos os recursos (personagem.png, plataforma.png, fundo.png, inimigo.png) prontos para serem usados. Além disso, as instruções a seguir presumem que você esteja familiarizado com o uso básico do Unity.

1. Criando o Personagem Principal:
   - Importe o "personagem.png" para o projeto em Unity.
   - Em seguida, crie um novo GameObject e adicione um componente SpriteRenderer a ele.
   - Atribua o "personagem.png" ao SpriteRenderer.
   - Adicione um Rigidbody2D para permitir a física.
   - Finalmente, crie um script de controle de movimento em C# que faz o personagem se mover para a esquerda/direita quando as teclas direcionais são pressionadas e pular quando a tecla de espaço é pressionada.

2. Criando o Cenário:
   - Importe "fundo.png" e "plataforma.png" para o projeto.
   - Crie GameObjects separados para cada um e adicione componentes de SpriteRenderer a eles.
   - Atribua as imagens correspondentes aos seus SpriteRenderers.

3. Criando o Inimigo:
   - Importe o "inimigo.png" para o projeto.
   - Crie um novo GameObject e adicione um componente SpriteRenderer a ele.
   - Atribua o "inimigo.png" ao SpriteRenderer.
   - Adicione um Rigidbody2D e um BoxCollider2D.
   - Crie um script que faz o inimigo se mover para a esquerda e para a direita.

4. Implementando a Física:
   - Já adicionamos Rigidbody2D ao nosso personagem e inimigo, o que lhes permitirá interagir fisicamente com o ambiente.

Nota: Isso é apenas um esboço conceitual e pode haver vários pequenos passos envolvidos, dependendo de como você deseja seu jogo. Recomendamos que você assista a tutoriais de Unity para obter um melhor entendimento se tiver dificuldades.