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
        shootCooldownTimer -= Time.deltaTime;

        if (isDashing) return;

        Vector3 moveDirection = Vector3.zero;
        float tiltAngle = 10f; // hoeveel graden plane draait
        float tiltAngleX = 10f;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward * moveSpeed;
          
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= transform.forward * moveSpeed;
          
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= transform.right * moveSpeed;
            transform.rotation = Quaternion.Euler(-90 + tiltAngle, -90, tiltAngleX + 90); // Links
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection += transform.right * moveSpeed;
            transform.rotation = Quaternion.Euler(-90 + -tiltAngle, -90, tiltAngleX + 90); // Rechts
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 90); // Reset rotation when not turning
        }

        rb.linearVelocity = moveDirection;

        // Schieten
        if (shootCooldownTimer > 0)
        {
            shootCooldownTimer -= Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && shootCooldownTimer <= 0)
        {
            if (laserKogel != null && bulletSpawnPointTest != null)
            {
                Instantiate(laserKogel, bulletSpawnPointTest.transform.position, laserKogel.transform.rotation);
                shootCooldownTimer = 1f;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && canDash)
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

    public void OnCollisionEnter(Collision collision) // collide om de player te verwijderen, geldt ook voor de kogel 
    {
        if (collision.gameObject.CompareTag("EnemyBullet") == true)
        {
            game.ReportPlayerHit();
            Destroy(collision.gameObject);
            collision.transform.position = new Vector3(-6, 0, 12);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false; // activeert
        isDashing = true; // voert het uit
        rb.useGravity = false;
        Vector3 dashDirection = rb.linearVelocity.normalized * dashingPower;
        rb.linearVelocity = dashDirection; // vliegt / word gebruikt
        TR.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        TR.emitting = false;
        rb.useGravity = true;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true; // kan weer dashen
    }


}
