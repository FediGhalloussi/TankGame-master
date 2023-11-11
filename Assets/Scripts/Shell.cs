using UnityEngine;

public class Shell : MonoBehaviour
{
    public float shellSpeed = 30f;
    public float timeScalingFactor = 5f;
    public AudioClip hitSound;
    public AudioClip explosionSound;

    private int hitCount = 0;

    private Vector3 initialPos;
    private Vector3 initialVelocity;
    
    private float startTime;
    public TankController Tank { get; set; }

    
    void Start()
    {
        startTime = Time.time;
        initialPos = transform.position;
        Quaternion rotation = transform.rotation;
        initialVelocity = new Vector3(shellSpeed * Mathf.Cos((rotation.eulerAngles.y-90) * Mathf.Deg2Rad),
            shellSpeed * Mathf.Sin(Mathf.Deg2Rad * 15),
            -shellSpeed * Mathf.Sin((rotation.eulerAngles.y-90) * Mathf.Deg2Rad));
    }
    void Update()
    {
        MoveShell();
    }

    void MoveShell()
    {
        float t = Time.time - startTime;

        // Applying ballistic curve equations
        float x = initialPos.x + 5 * initialVelocity.x * t;
        float y = initialPos.y + 5 * initialVelocity.y * t - (5 * t) * (5 * t) * 9.81f / 2; //todo no hardcoding later
        float z = initialPos.z + 5 * initialVelocity.z * t;

        transform.position = new Vector3(x, y, z);

        if (y <= 0f) // Assuming ground level is at y = 0 todo no hardcoding later
        {
            DestroyShell();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tank"))
        {
            TankController tankController = other.GetComponent<TankController>();
            if (tankController != null && tankController != Tank)
            {
                tankController.TakeDamage();
                hitCount++;

                // Play hit sound
                GameManager.Instance.PlayHitSound();

                if (hitCount >= 3) //todo no hardcoding later
                {
                    DestroyTank(tankController);
                }

                DestroyShell();
            }
        }
    }

    void DestroyShell()
    {
        Tank.numberOfShells--;
        Destroy(gameObject);
    }

    void DestroyTank(TankController tankController)
    {
        Destroy(tankController.gameObject);
    }
    
    
}