using UnityEngine;

public class ComputerTankController : TankController
{
    public float minTurnSpeed = -50f;
    public float maxTurnSpeed = 50f;
    private float nextTurnTime;


    void Update()
    {
        base.Update();
        // Computer Tank Movement
        Vector3 movement = transform.forward * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // Computer Tank Rotation
        if (Time.time >= nextTurnTime)
        {
            Debug.Log("Turning");
            float turnSpeed = Random.Range(minTurnSpeed, maxTurnSpeed);
            transform.Rotate(0f, turnSpeed * Time.deltaTime, 0f);
            nextTurnTime = Time.time + 1f; // Change direction every second
        }

        // Computer Tank Shooting
        Shoot();
    }
    
    
    public override void DestroyTank()
    {
        base.DestroyTank();
        GameManager.Instance.TankDestroyed(false);
    }
}