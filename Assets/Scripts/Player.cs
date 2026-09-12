using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rb2D;
    private float move;
    public float jumpForce = 4;
    
    // Variables para la deteccion del suelo
    private bool isGrounded;
    public Transform groundCheck;
    public float groundRadius = 0.1f;
    public LayerMask groundLayer;

    private Animator animator;

    void Start()
    {
        // Al arrancar el juego, obtenemos y guardamos las referencias directas a las físicas  y animaciones  del personaje.
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. MOVIMIENTO HORIZONTAL: Usamos GetAxisRaw para lograr una respuesta inmediata y frenados en seco, sin inercia.
        move = Input.GetAxisRaw("Horizontal");
        rb2D.linearVelocity = new Vector2(move * speed, rb2D.linearVelocity.y);

        // 2. ORIENTACION: Gira la escala del sprite hacia la direccion en la que nos movemos.
        if (move != 0) {
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);
        }
        
        // 3. SALTO CONTROLADO: Aplica fuerza de salto SOLO si se presiona el boton y el personaje esta tocando el suelo.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
        }

        // Sincronizacion de variables con el Animator
        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetFloat("VerticalVelocity", rb2D.linearVelocity.y);
        animator.SetBool("IsGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        // 4. PREVENCION DE SALTO INFINITO: OverlapCircle crea un radio de deteccion en los pies filtrando por la capa "Floor".
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 5. COLECCIONABLES: Identifica la moneda mediante su Tag y la destruye al hacer contacto limpio (Trigger).
        if (collision.transform.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }
    }
}