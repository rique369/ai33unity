Como um modelo de inteligência artificial, não consigo criar ou programar jogos de maneira física como um humano faria. No entanto, posso fornecer um esboço básico de como você pode fazer isso no Unity. Aqui está:

1. Criação de Personagem.
Primeiro, você precisa importar a imagem 'personagem.png' para o Unity. Em seguida, você cria um novo objeto de jogo 2D (GameObjet > 2D Object > Sprite) e atribui a imagem ao Sprite Renderer desse objeto.

```csharp
public class PlayerController : MonoBehaviour 
{
    public float speed = 10.0f;
    public float jumpingForce = 5f;
    private bool isJumping = false;
    public Rigidbody2D rb;

    void Update() 
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");

        rb.velocity = new Vector2 (moveHorizontal * speed, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpingForce), ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
```

2. Cenário e plataforma.
De maneira semelhante ao personagem, importe os arquivos 'plataforma.png' e 'fundo.png', crie objetos de jogo para cada um e atribua as imagens apropriadas ao Sprite Renderer de cada objeto. Certifique-se de que a plataforma tem um Collider 2D para que o personagem possa interagir com ela.

3. Inimigo.
Da mesma forma, importe a imagem 'inimigo.png', crie um objeto de jogo e atribua a imagem. Para que o inimigo se mova, crie um script que alterna a direção do inimigo quando ele chega a determinados limites no cenário.

```csharp
public class EnemyController : MonoBehaviour
{
    public float speed = 2.0f;
    private bool movingRight = true;
    public Transform groundDetection;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        if(groundInfo.collider == false)
        {
            if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            } else {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}
```
Lembre-se de apontar as tags corretas para o chão e o jogador para garantir interações corretas. 

Infelizmente, essa é uma versão muito simplificada do que seu jogo pode ser e existem muitos outros aspectos que você pode querer ajustar, como animações, som, inimigos adicionais, power-ups, etc. Você pode estudar mais, explorando a documentação oficial do Unity ou tutoriais online.