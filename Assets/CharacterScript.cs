Como um assistente de IA, n�o consigo criar jogos ou programas de forma direta. No entanto, eu posso orient�-lo atrav�s do processo de como aplicar essas suas necessidades no Unity.

Primeiramente, sugiro que certifique-se de ter o Unity instalado no seu computador e de ter todos os arquivos PNG mencionados prontos e carregados na pasta 'Assets' do seu projeto Unity.

1. Cria��o do personagem principal:
   - No painel Hierarchy, clique com o bot�o direito e escolha �Create Empty�.
   - Atribua o nome de �Character� a esse objeto vazio, adicione o componente 'Rigidbody 2D' para o personagem ter f�sica.
   - Finalmente, no componente 'Sprite Renderer', clique no pequeno c�rculo � direita de 'Sprite' e escolha 'personagem.png' para a imagem do personagem.
    
2. Mover e pular o personagem:
   - Voc� precisar� de um script C# para isso. Crie um script chamado 'CharacterMovement' e adicione-o ao personagem. Voc� precisar� ativar o c�digo para detectar inputs do usu�rio e, em seguida, usar esses inputs para mover e pular o personagem aplicando for�a ao Rigidbody 2D.

3. Criar o cen�rio:
   - Procure a op��o 'Create' e ent�o v� em 'Sprite' e escolha 'Square' para criar uma nova plataforma.
   - Atribua a 'plataforma.png' � plataforma. Repita o processo para o fundo, atribuindo a 'fundo.png'.
    
4. Adicionar o inimigo:
   - O processo � similar ao do personagem principal, s� que na imagem atribua a 'inimigo.png'. Crie um novo script C# chamado 'EnemyMovement' e, nesse caso, em vez de buscar os inputs do usu�rio, o inimigo simplesmente se mover� entre duas posi��es predeterminadas.

Espero que isso seja �til e d� uma base para come�ar! Lembre-se, � poss�vel que detalhes possam variar ligeiramente dependendo da sua vers�o do Unity e como voc� quer que o seu jogo funcione.