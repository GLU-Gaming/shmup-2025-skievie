using System.Collections;
using UnityEngine;

public class PlaneScrip : MonoBehaviour
{
    private Rigidbody rb; // rigidbody Player

    [SerializeField] private GameObject laserKogel;
    [SerializeField] private GameObject bulletSpawnPointTest;

    public float shootCooldownTimer = 0f;
    public float shootCooldownDuration = 10f;

    public float moveSpeed = 3f; // In game is ie op 6 trouwens

    public GameManagement game; // script aan script 

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 4f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private TrailRenderer TR;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // object ophalen
    }


    private void Update()
    {
        shootCooldownTimer -= Time.deltaTime; // aftellen

        if (isDashing)
        {
            return;
        }


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
        }
        else if (Input.GetKey(KeyCode.D)) // controls D
        {
            rb.linearVelocity = transform.right * moveSpeed;
        }
        else // stopt bewegen
        {
            rb.linearVelocity = Vector3.zero;
        }

        if (shootCooldownTimer > 0) // aftellen
        {
            shootCooldownTimer -= Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && shootCooldownTimer <= 0) // kogel afvuren
        {
            if (laserKogel != null && bulletSpawnPointTest != null)
            {
                Instantiate(laserKogel, bulletSpawnPointTest.transform.position, laserKogel.transform.rotation);
                shootCooldownTimer = 1f;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && canDash) // dash gebruiken knop
        {
            StartCoroutine(Dash());
        }


    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }



    }

    private IEnumerator Dash()
    {
        canDash = false; // activeert
        isDashing = true; // voert het uit
        rb.useGravity = false;
        Vector3 dashDirection = rb.transform.position * dashingPower;
        rb.AddForce(dashDirection, ForceMode.VelocityChange); // vliegt / word gebruikt
        TR.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        TR.emitting = false;
        rb.useGravity = true;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true; // kan weer dashen

    }

}
