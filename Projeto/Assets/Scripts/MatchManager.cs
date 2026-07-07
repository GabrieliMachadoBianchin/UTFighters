using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour
{
    [Header("Jogador 1")]
    public CharacterManager player1CharacterManager;
    public PlayerHealth player1Health;
    public PlayerController player1Controller;
    public PlayerCombat player1Combat;
    public TMP_Text player1NameText;

    [Header("Jogador 2")]
    public CharacterManager player2CharacterManager;
    public PlayerHealth player2Health;
    public PlayerController player2Controller;
    public PlayerCombat player2Combat;
    public TMP_Text player2NameText;

    [Header("Timer")]
    public GameTimer gameTimer;

    [Header("Tela de resultado")]
    public GameObject resultPanel;
    public TMP_Text resultText;
    public Button rematchButton;
    public Button menuButton;
    public string menuSceneName = "Menu";

    private bool matchEnded;

    private void Awake()
    {
        if (resultPanel != null)
            resultPanel.SetActive(false);
    }

    private void Start()
    {
        // Aplica os personagens escolhidos na tela de seleção (GameManager)
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.player1Character != null)
                player1CharacterManager.SetCharacter(GameManager.Instance.player1Character);

            if (GameManager.Instance.player2Character != null)
                player2CharacterManager.SetCharacter(GameManager.Instance.player2Character);
        }

        // Configura a vida de cada jogador com base no maxHealth do CharacterData
        player1Health.Setup(player1CharacterManager.Data.maxHealth);
        player2Health.Setup(player2CharacterManager.Data.maxHealth);

        if (player1NameText != null)
            player1NameText.text = player1CharacterManager.Data.characterName;
        if (player2NameText != null)
            player2NameText.text = player2CharacterManager.Data.characterName;

        // KO: se a vida de alguém chegar a zero, o outro vence na hora
        player1Health.OnDeath += () => EndMatch(player2CharacterManager.Data.characterName);
        player2Health.OnDeath += () => EndMatch(player1CharacterManager.Data.characterName);

        // Fim de tempo: vence quem tiver mais vida
        if (gameTimer != null)
            gameTimer.OnTimeUp += EndMatchByTime;

        if (rematchButton != null)
            rematchButton.onClick.AddListener(Rematch);
        if (menuButton != null)
            menuButton.onClick.AddListener(GoToMenu);
    }

    private void EndMatchByTime()
    {
        if (matchEnded) return;

        if (player1Health.CurrentHealth > player2Health.CurrentHealth)
            EndMatch(player1CharacterManager.Data.characterName);
        else if (player2Health.CurrentHealth > player1Health.CurrentHealth)
            EndMatch(player2CharacterManager.Data.characterName);
        else
            EndMatch(null); // empate
    }

    private void EndMatch(string winnerName)
    {
        if (matchEnded) return;
        matchEnded = true;

        gameTimer?.StopTimer();

        // Trava os controles para ninguém continuar lutando/andando
        if (player1Controller != null) player1Controller.enabled = false;
        if (player2Controller != null) player2Controller.enabled = false;
        if (player1Combat != null) player1Combat.enabled = false;
        if (player2Combat != null) player2Combat.enabled = false;

        if (resultPanel != null)
            resultPanel.SetActive(true);

        if (resultText != null)
            resultText.text = winnerName != null ? $"{winnerName} venceu!" : "Empate!";
    }

    private void Rematch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
