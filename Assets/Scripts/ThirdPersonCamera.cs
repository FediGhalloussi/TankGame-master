using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public float distance = 80f;
    public float height = 16f;
    public float smoothSpeed = 5f;

    private Transform target;

    void Start()
    {
        FindPlayer();
    }

    void LateUpdate()
    {
        if (target == null)
        {
            FindPlayer();
            return;
        }

        // Calculate the desired position based on distance and height
        Vector3 desiredPosition = target.position - target.forward * distance + Vector3.up * height;

        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Make the camera look at the target
        transform.LookAt(target);
    }

    void FindPlayer()
    {
        GameObject player = GameObject.FindObjectOfType<PlayerTankController>()?.gameObject;
        if (player != null)
        {
            target = player.transform;
        }
    }
}