using UnityEngine;

public class PlaneScrip : MonoBehaviour
{
    private Rigidbody rb; // rigidbody Player

    [SerializeField] private GameObject laserKogel;
    [SerializeField] private GameObject bulletSpawnPointTest;

    public float shootCooldownTimer = 0f;
    public float shootCooldownDuration = 10f;

    public float moveSpeed = 3f; // In game is ie op 6 trouwens

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // object ophalen
    }


    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)) // controls W
        {
            rb.linearVelocity = transform.forward * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.S)) // controls S
        {
            rb.linearVelocity = -transform.forward * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.A)) // controls A
        {
            rb.linearVelocity = -transform.right * moveSpeed;
        }else if (Input.GetKey(KeyCode.D)) // controls D
        {
            rb.linearVelocity = transform.right * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.Space) && shootCooldownTimer <= 0) // kogel afvuren
        {
            if (laserKogel != null && bulletSpawnPointTest != null)
            {
                Instantiate(laserKogel, bulletSpawnPointTest.transform.position, transform.rotation);
                shootCooldownTimer = 0.5f;
            }
        }

    }

}
