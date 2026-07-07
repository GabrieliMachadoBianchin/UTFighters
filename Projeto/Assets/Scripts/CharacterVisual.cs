using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
    [SerializeField] private CharacterManager characterManager;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private CharacterState currentState = CharacterState.Idle;
    private bool facingRight = true;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (characterManager == null)
            characterManager = GetComponent<CharacterManager>();
    }

    private void Start()
    {
        SetState(CharacterState.Idle);
    }

    // Chamado pelo PlayerController sempre que o jogador vira.
    public void SetFacing(bool faceRight)
    {
        facingRight = faceRight;
        ApplyFlip();
    }

    public void SetState(CharacterState state)
    {
        currentState = state;

        if (characterManager == null || characterManager.Data == null || spriteRenderer == null)
            return;

        CharacterData data = characterManager.Data;

        switch (state)
        {
            case CharacterState.Idle:
                spriteRenderer.sprite = data.idleSprite;
                break;

            case CharacterState.Punch:
                spriteRenderer.sprite = data.punchSprite;
                break;

            case CharacterState.Kick:
                spriteRenderer.sprite = data.kickSprite;
                break;

            case CharacterState.Special:
                spriteRenderer.sprite = data.specialSprite;
                break;
        }

        // Toda vez que trocamos a sprite (ex.: começar um chute), reaplicamos
        // o espelhamento para que o golpe fique virado para o mesmo lado que
        // o personagem está encarando.
        ApplyFlip();
    }

    private void ApplyFlip()
    {
        if (spriteRenderer == null || characterManager == null || characterManager.Data == null)
            return;

        bool spriteFacesRight = SpriteFacesRight(currentState);

        // flipX só é ativado quando a direção desejada difere do lado para o
        // qual aquela sprite específica foi desenhada.
        spriteRenderer.flipX = (facingRight != spriteFacesRight);
    }

    private bool SpriteFacesRight(CharacterState state)
    {
        CharacterData data = characterManager.Data;

        switch (state)
        {
            case CharacterState.Punch:
                return data.punchFacesRight;
            case CharacterState.Kick:
                return data.kickFacesRight;
            case CharacterState.Special:
                return data.specialFacesRight;
            default:
                return data.idleFacesRight;
        }
    }
}
