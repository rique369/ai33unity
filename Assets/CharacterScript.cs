Desculpe pela confus�o, mas como uma IA de texto, n�o tenho a capacidade de criar jogos em Unity diretamente. No entanto, posso lhe dar uma estrutura b�sica sobre como voc� pode abordar seu projeto:

**Cria��o de personagem:**

1. V� para `Hierarquia -> Clique com o bot�o direito -> 2D Object -> Sprite`.
2. No painel de Inspector, defina a imagem para `personagem.png`.
3. Adicione um componente de Rigidbody 2D para f�sica b�sica.
4. Adicione um componente de BoxCollider2D para detec��o de colis�es.

Para permitir o movimento do personagem, voc� precisa criar um script. Este script deve ser anexado ao personagem:

```csharp
public class PlayerMovement : MonoBehaviour 
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private bool isJumping = false;
    private Rigidbody2D rb;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if(!isJumping && Input.GetButtonDown("Jump")) 
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
    }
}
```

**Cria��o de cen�rio:**

1. V� para `Hierarquia -> Clique com o bot�o direito -> 2D Object -> Tilemap` e desenhe a plataforma usando 'plataforma.png'.
2. Crie um novo Sprite para o fundo e defina a imagem para 'fundo.png'.

**Adicionar inimigo:**

1. Semelhante � cria��o do personagem, crie um novo Sprite para o inimigo e atribua a imagem do inimigo.
2. Adicione um script ao inimigo para mover-se para a esquerda e direita.

Neste exemplo a seguir, o inimigo se move entre dois pontos:

```csharp
public class EnemyMovement : MonoBehaviour 
{
    public float speed = 2.0f;
    public float moveLimitLeft = 2.0f;
    public float moveLimitRight = 2.0f;

    private Vector3 direction = Vector3.left;

    private void Update() 
    {
        if (transform.position.x >= moveLimitRight) 
        {
            direction = Vector3.left;
        } 
        else if (transform.position.x <= moveLimitLeft) 
        {
            direction = Vector3.right;
        }

        transform.position += direction * speed * Time.deltaTime;
    }
}
```

Lembre-se, voc� precisar� ajustar os valores e o c�digo para atender �s suas necessidades espec�ficas. Espero que isso de alguma forma ajude a direcion�-lo de maneira adequada.