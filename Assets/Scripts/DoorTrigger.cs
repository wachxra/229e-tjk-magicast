using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Transform destinationPoint;
    [SerializeField] private bool useSceneTransition = true;
    [SerializeField] private Camera targetCamera;

    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (InventoryManager.instance.HasKey())
            {
                InventoryManager.instance.UseKey();

                if (useSceneTransition)
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
                else
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player != null && destinationPoint != null)
                    {
                        player.transform.position = destinationPoint.position;

                        Camera[] allCameras = Camera.allCameras;
                        foreach (Camera cam in allCameras)
                        {
                            cam.gameObject.SetActive(false);
                        }

                        if (targetCamera != null)
                        {
                            targetCamera.gameObject.SetActive(true);
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Need a key to open this door.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = false;
    }
}
