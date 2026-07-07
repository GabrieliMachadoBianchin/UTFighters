using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Teclas")]
    public KeyCode punchKey;
    public KeyCode kickKey;
    public KeyCode specialKey;

    [Header("Tempo dos golpes")]
    public float attackDuration = 0.25f;

    [Header("Detecção de acerto")]
    public Transform attackPoint;
    public float attackRange = 0.6f;
    public LayerMask opponentLayer;

    private CharacterVisual characterVisual;
    private CharacterManager characterManager;
    private bool isAttacking;

    private void Awake()
    {
        characterVisual = GetComponent<CharacterVisual>();
        characterManager = GetComponent<CharacterManager>();
    }

    void Update()
    {
        if (isAttacking)
            return;

        if (Input.GetKeyDown(punchKey))
        {
            StartCoroutine(Attack(CharacterState.Punch));
        }

        if (Input.GetKeyDown(kickKey))
        {
            StartCoroutine(Attack(CharacterState.Kick));
        }

        if (Input.GetKeyDown(specialKey))
        {
            StartCoroutine(Attack(CharacterState.Special));
        }
    }

    IEnumerator Attack(CharacterState state)
    {
        isAttacking = true;

        characterVisual.SetState(state);
        DealDamage(state);

        yield return new WaitForSeconds(attackDuration);

        characterVisual.SetState(CharacterState.Idle);

        isAttacking = false;
    }

    private void DealDamage(CharacterState state)
    {
        if (attackPoint == null || characterManager == null || characterManager.Data == null)
            return;

        int damage = 0;
        switch (state)
        {
            case CharacterState.Punch:
                damage = characterManager.Data.punchDamage;
                break;
            case CharacterState.Kick:
                damage = characterManager.Data.kickDamage;
                break;
            case CharacterState.Special:
                damage = characterManager.Data.specialDamage;
                break;
        }

        if (damage <= 0) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, opponentLayer);

        foreach (Collider2D hit in hits)
        {
            PlayerHealth opponentHealth = hit.GetComponentInParent<PlayerHealth>();
            if (opponentHealth != null)
            {
                opponentHealth.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}