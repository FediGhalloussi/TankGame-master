using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class TankController : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float turnSpeed = 30f;
    public AudioClip hitSound;
    public AudioClip explosionSound;
    public Shell shellPrefab;
    public Transform shootPoint;
    private float nextFireTime;
    private float fireRate = 0.2f; // 5 shells per second
    private int hitCount = 0;
    public int numberOfShells = 0;
    
    void Start()
    {
        RandomizeColor();
    }

    protected void Update()
    {
        Vector3 currentPosition = transform.position;
        if (!GameManager.Instance.IsInsideFightingZone(currentPosition))
        {
            Vector3 newPosition = GameManager.Instance.GetRandomPosition();
            transform.position = newPosition;
        }
    }

    public void TakeDamage()
    {
        hitCount++;

        // Play hit sound
        if (hitSound != null)
        {
            GameManager.Instance.PlayHitSound();
        }

        if (hitCount >= 3)
        {
            DestroyTank();
        }
    }

    public virtual void DestroyTank()
    {
        Destroy(gameObject);
    }

    protected void Shoot()
    {
        if (CanFire())
        {
            Shell shell = (Shell)Instantiate(shellPrefab, shootPoint.position,
                Quaternion.LookRotation(transform.forward));
            shell.Tank = this;
            numberOfShells++;
            nextFireTime = Time.time + fireRate;
            //GameManager.Instance.PlayFireSound();
        }
    }

    protected bool CanFire()
    {
        // Check if the tank has reached the maximum number of simultaneous shells
        if (numberOfShells >= 5 || Time.time < nextFireTime)
        {
            return false;
        }

        return true;
    }
    
    void RandomizeColor()
    {
        // Get the renderer component attached to this GameObject
        Renderer rend = GetComponentInChildren<Renderer>();

        // Check if a renderer is attached
        if (rend != null)
        {
            // Generate a random color
            Color randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);

            // Set the material color to the random color
            rend.material.color = randomColor;
        }
        else
        {
            Debug.LogWarning("Renderer component not found.");
        }
    }
}