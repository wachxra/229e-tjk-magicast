using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName, bool resetPlayerStatus)
    {
        if (resetPlayerStatus && PlayerStatusManager.instance != null)
        {
            Destroy(PlayerStatusManager.instance.gameObject);
            PlayerStatusManager.instance = null;
        }

        SceneManager.LoadScene(sceneName);
    }
}