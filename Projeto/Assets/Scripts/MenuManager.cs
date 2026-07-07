using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject painelExplicacao;

    public void Jogar()
    {
        SceneManager.LoadScene("SelectCharacter");
    }

    public void AbrirExplicacao()
    {
        painelExplicacao.SetActive(true);
    }

    public void FecharExplicacao()
    {
        painelExplicacao.SetActive(false);
    }

    public void Sair()
    {
        Application.Quit();

        // Apenas para testes no Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}