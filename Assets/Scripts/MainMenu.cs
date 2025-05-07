using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject howToPlayPanel;
    public GameObject deathPanel;

    [Header("Sound Settings")]
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource clickSound;

    private bool isInEndCreditScene = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (SceneManager.GetActiveScene().name == "EndCredit")
        {
            isInEndCreditScene = true;
        }

        if (backgroundMusic != null && !backgroundMusic.isPlaying)
        {
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }

    void Update()
    {
        if (isInEndCreditScene && Input.anyKeyDown)
        {
            StartCoroutine(PlayClickAndBackToMainMenu());
        }
    }

    public void StartGame()
    {
        ResetGameStatus();
        Time.timeScale = 1f;
        StartCoroutine(PlayClickAndLoadScene("TutorialScene"));
    }

    public void TryAgain()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }

        ResetGameStatus();
        Time.timeScale = 1f;

        if (clickSound != null)
        {
            clickSound.Play();
            Invoke(nameof(ReloadCurrentScene), clickSound.clip.length);
        }
        else
        {
            ReloadCurrentScene();
        }
    }

    private void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        if (clickSound != null)
        {
            clickSound.Play();
        }

        Application.Quit();
    }

    public void GoToCreditScene()
    {
        StartCoroutine(PlayClickAndLoadScene("EndCredit"));
    }

    public void BackToMainMenu()
    {
        ResetGameStatus();
        StartCoroutine(PlayClickAndLoadScene("MainMenu"));
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

    private IEnumerator PlayClickAndLoadScene(string sceneName)
    {
        if (clickSound != null)
        {
            clickSound.Play();
            yield return new WaitForSeconds(clickSound.clip.length);
        }
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator PlayClickAndBackToMainMenu()
    {
        if (clickSound != null)
        {
            clickSound.Play();
            yield return new WaitForSeconds(clickSound.clip.length);
        }
        ResetGameStatus();
        SceneManager.LoadScene("MainMenu");
    }
}