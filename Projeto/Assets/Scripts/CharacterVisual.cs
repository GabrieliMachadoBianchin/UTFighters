using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
    [SerializeField] private CharacterManager characterManager;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        SetState(CharacterState.Idle);
    }

    private void Awake()
{
    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
}

    public void SetState(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Idle:
                spriteRenderer.sprite = characterManager.Data.idleSprite;
                break;

            case CharacterState.Punch:
                spriteRenderer.sprite = characterManager.Data.punchSprite;
                break;

            case CharacterState.Kick:
                spriteRenderer.sprite = characterManager.Data.kickSprite;
                break;

            case CharacterState.Special:
                spriteRenderer.sprite = characterManager.Data.specialSprite;
                break;
        }
    }
}