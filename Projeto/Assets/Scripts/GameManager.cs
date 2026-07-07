using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Personagens escolhidos (preenchido pela tela de seleção)")]
    public CharacterData player1Character;
    public CharacterData player2Character;

    [Header("Configurações da partida")]
    public float roundTime = 60f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
