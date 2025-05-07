using UnityEngine;
using static ItemType;

public class Chest : MonoBehaviour
{
    [System.Serializable]
    public class ChestItem
    {
        public ItemType type;
        public int amount;
    }

    private AudioManager audioManager;
    [SerializeField] private Animator animator;
    [SerializeField] private ChestItem[] itemsInChest;
    [SerializeField] private bool isOpened = false;

    private bool playerInRange = false;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isOpened)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        isOpened = true;
        if (animator != null)
            animator.SetTrigger("open");

        audioManager.PlayItem();

        foreach (var item in itemsInChest)
        {
            InventoryManager.instance.AddItem(item.type, item.amount);
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