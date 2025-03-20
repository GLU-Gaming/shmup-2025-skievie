using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 2;
    private Rigidbody rb;

    private void Start()
    {
        rb = Getc
    }


    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * moveSpeed, ForceMode.Force);
        }


        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * moveSpeed, ForceMode.Force);
        }


        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(Vector3.down * (turnSpeed * multiplier));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(Vector3.up * (turnSpeed * multiplier));
        }

    }

}
