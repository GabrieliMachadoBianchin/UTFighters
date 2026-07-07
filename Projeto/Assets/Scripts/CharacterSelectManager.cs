using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectManager : MonoBehaviour
{
    [Header("Personagens disponíveis")]
    public CharacterData[] characters;

    [Header("Botões - Jogador 1 (mesma ordem de 'characters')")]
    public Button[] player1Buttons;

    [Header("Botões - Jogador 2 (mesma ordem de 'characters')")]
    public Button[] player2Buttons;

    [Header("Pré-visualização - Jogador 1")]
    public Image player1PreviewImage;
    public TMP_Text player1NameText;

    [Header("Pré-visualização - Jogador 2")]
    public Image player2PreviewImage;
    public TMP_Text player2NameText;

    [Header("Início da partida")]
    public Button startButton;
    public string arenaSceneName = "Arena1";

    private CharacterData player1Selected;
    private CharacterData player2Selected;

    private void Start()
    {
        for (int i = 0; i < player1Buttons.Length && i < characters.Length; i++)
        {
            CharacterData data = characters[i];
            player1Buttons[i].onClick.AddListener(() => SelectPlayer1(data));
        }

        for (int i = 0; i < player2Buttons.Length && i < characters.Length; i++)
        {
            CharacterData data = characters[i];
            player2Buttons[i].onClick.AddListener(() => SelectPlayer2(data));
        }

        if (startButton != null)
        {
            startButton.interactable = false;
            startButton.onClick.AddListener(StartMatch);
        }

        UpdatePreview(1);
        UpdatePreview(2);
    }

    private void SelectPlayer1(CharacterData data)
    {
        player1Selected = data;
        UpdatePreview(1);
        CheckReady();
    }

    private void SelectPlayer2(CharacterData data)
    {
        player2Selected = data;
        UpdatePreview(2);
        CheckReady();
    }

    private void UpdatePreview(int player)
    {
        if (player == 1)
        {
            bool hasChar = player1Selected != null;
            if (player1PreviewImage != null)
                player1PreviewImage.sprite = hasChar ? player1Selected.idleSprite : null;
            if (player1NameText != null)
                player1NameText.text = hasChar ? player1Selected.characterName : "Escolha um personagem";
        }
        else
        {
            bool hasChar = player2Selected != null;
            if (player2PreviewImage != null)
                player2PreviewImage.sprite = hasChar ? player2Selected.idleSprite : null;
            if (player2NameText != null)
                player2NameText.text = hasChar ? player2Selected.characterName : "Escolha um personagem";
        }
    }

    private void CheckReady()
    {
        if (startButton != null)
            startButton.interactable = player1Selected != null && player2Selected != null;
    }

    private void StartMatch()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager não encontrado! Crie um GameObject com o script GameManager na cena Menu.");
            return;
        }

        GameManager.Instance.player1Character = player1Selected;
        GameManager.Instance.player2Character = player2Selected;

        SceneManager.LoadScene(arenaSceneName);
    }
}
