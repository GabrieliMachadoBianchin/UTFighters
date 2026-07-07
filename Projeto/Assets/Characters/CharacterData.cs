using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Fighting Game/Character")]
public class CharacterData : ScriptableObject
{
    [Header("Informações")]
    public string characterName;

    [Header("Sprites")]
    public Sprite idleSprite;
    public Sprite punchSprite;
    public Sprite kickSprite;
    public Sprite specialSprite;

    [Header("Atributos")]
    public int maxHealth = 100;
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    [Header("Dano")]
    public int punchDamage = 10;
    public int kickDamage = 15;
    public int specialDamage = 30;
}