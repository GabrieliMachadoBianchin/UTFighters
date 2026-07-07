using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    [Header("Controles")]
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private PlayerCombat combat;
    private CharacterVisual characterVisual;

    private bool isGrounded;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        combat = GetComponent<PlayerCombat>();
        characterVisual = GetComponent<CharacterVisual>();

        // Garante que a sprite comece coerente com a direção inicial.
        ApplyFlip();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float move = 0f;

        if (Input.GetKey(leftKey))
            move = -1f;

        if (Input.GetKey(rightKey))
            move = 1f;

        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        // Espelha a sprite conforme a seta pressionada.
        if (move > 0f && !facingRight)
            Flip(true);
        else if (move < 0f && facingRight)
            Flip(false);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer);

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void Flip(bool faceRight)
    {
        facingRight = faceRight;
        ApplyFlip();
    }

    void ApplyFlip()
    {
        // O espelhamento visual é delegado ao CharacterVisual, que conhece a
        // orientação original de cada sprite (idle, soco, chute, poder) e
        // mantém o golpe sempre virado para o lado que o personagem encara.
        if (characterVisual != null)
            characterVisual.SetFacing(facingRight);
        else if (spriteRenderer != null)
            spriteRenderer.flipX = !facingRight; // fallback simples

        // Espelha o AttackPoint para o golpe continuar saindo à frente
        // do personagem, mesmo depois de virar.
        if (combat != null && combat.attackPoint != null)
        {
            Vector3 pos = combat.attackPoint.localPosition;
            pos.x = Mathf.Abs(pos.x) * (facingRight ? 1f : -1f);
            combat.attackPoint.localPosition = pos;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}