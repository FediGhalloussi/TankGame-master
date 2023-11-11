using UnityEngine;

public class PlayerTankController : TankController
{
    void Update()
    {
        base.Update();

        // Player Tank Movement
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movement = transform.forward * verticalInput * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // Player Tank Rotation
        float turn = horizontalInput * turnSpeed * Time.deltaTime;
        transform.Rotate(0f, turn, 0f);

        // Player Tank Shooting
        if (Input.GetKeyDown(KeyCode.Space) && CanFire())
        {
            Shoot();
        }
    }

    public override void DestroyTank()
    {
        base.DestroyTank();
        GameManager.Instance.TankDestroyed(true);
    }
}