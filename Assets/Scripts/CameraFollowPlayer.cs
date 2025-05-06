using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float yOffset = 0f;
    public float smoothSpeed = 0.2f;
    private float currentY;

    void Start()
    {
        currentY = transform.position.y;
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = new Vector3(player.position.x, currentY, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        smoothedPosition.x = Mathf.Round(smoothedPosition.x * 100f) / 100f;
        smoothedPosition.y = Mathf.Round(smoothedPosition.y * 100f) / 100f;

        transform.position = smoothedPosition;
    }

    public void SetCameraY(float newY)
    {
        currentY = newY;
    }
}