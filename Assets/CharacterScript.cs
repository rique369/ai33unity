Como um assistente de IA, não consigo criar jogos ou programas de forma direta. No entanto, eu posso orientá-lo através do processo de como aplicar essas suas necessidades no Unity.

Primeiramente, sugiro que certifique-se de ter o Unity instalado no seu computador e de ter todos os arquivos PNG mencionados prontos e carregados na pasta 'Assets' do seu projeto Unity.

1. Criação do personagem principal:
   - No painel Hierarchy, clique com o botão direito e escolha ‘Create Empty’.
   - Atribua o nome de ‘Character’ a esse objeto vazio, adicione o componente 'Rigidbody 2D' para o personagem ter física.
   - Finalmente, no componente 'Sprite Renderer', clique no pequeno círculo à direita de 'Sprite' e escolha 'personagem.png' para a imagem do personagem.
    
2. Mover e pular o personagem:
   - Você precisará de um script C# para isso. Crie um script chamado 'CharacterMovement' e adicione-o ao personagem. Você precisará ativar o código para detectar inputs do usuário e, em seguida, usar esses inputs para mover e pular o personagem aplicando força ao Rigidbody 2D.

3. Criar o cenário:
   - Procure a opção 'Create' e então vá em 'Sprite' e escolha 'Square' para criar uma nova plataforma.
   - Atribua a 'plataforma.png' à plataforma. Repita o processo para o fundo, atribuindo a 'fundo.png'.
    
4. Adicionar o inimigo:
   - O processo é similar ao do personagem principal, só que na imagem atribua a 'inimigo.png'. Crie um novo script C# chamado 'EnemyMovement' e, nesse caso, em vez de buscar os inputs do usuário, o inimigo simplesmente se moverá entre duas posições predeterminadas.

Espero que isso seja útil e dê uma base para começar! Lembre-se, é possível que detalhes possam variar ligeiramente dependendo da sua versão do Unity e como você quer que o seu jogo funcione.