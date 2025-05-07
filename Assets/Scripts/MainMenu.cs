using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public GameObject deathPanel;

    public void StartGame()
    {
        ResetGameStatus();
        Time.timeScale = 1f;
        SceneManager.LoadScene("TutorialScene");
    }

    public void TryAgain()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }

        ResetGameStatus();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToCreditScene()
    {
        SceneManager.LoadScene("EndCredit");
    }

    public void BackToMainMenu()
    {
        ResetGameStatus();
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowHowToPlay()
    {
        howToPlayPanel.SetActive(true);
    }

    public void HideHowToPlay()
    {
        howToPlayPanel.SetActive(false);
    }

    private void ResetGameStatus()
    {
        PlayerStatusManager instance = FindFirstObjectByType<PlayerStatusManager>();
        if (instance != null)
        {
            Destroy(instance.gameObject);
            PlayerStatusManager.instance = null;
        }
    }
}
