using System.Collections;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private GameObject laserKogel;
    [SerializeField] private GameObject bulletSpawnPointTest;

    public float shootCooldownTimer = 0f;
    public float shootCooldownDuration = 0.2f;
    public float moveSpeed = 6f;
    public float verticalMoveSpeed = 3f;

    public GameManagement game;

    // Dash variables
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private TrailRenderer TR;

    // Animation variables
    private float tiltAngle = 40f;
    private float currentTiltY = 0f; // For Y-axis rotation (A/D keys)
    private float currentTiltZ = 0f; // For Z-axis rotation (W/S keys)
    private float tiltSmoothness = 20f;
    private float defaultRotationZ = 90f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX; // Only freeze X rotation if needed
        transform.rotation = Quaternion.Euler(0f, 0f, defaultRotationZ);
    }

    private void Update()
    {
        if (isDashing) return;

        HandleMovement();
        HandleShooting();
        HandleDash();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        float horizontalInput = Input.GetKey(KeyCode.D) ? 1f : (Input.GetKey(KeyCode.A) ? -1f : 0f);
        float verticalInput = Input.GetKey(KeyCode.W) ? 2f : (Input.GetKey(KeyCode.S) ? -2f : 0f);

        moveDirection += Vector3.right * horizontalInput * moveSpeed;
        moveDirection += Vector3.up * verticalInput * verticalMoveSpeed;

        rb.velocity = moveDirection;

        float targetTiltY = horizontalInput * tiltAngle;  
        float targetTiltX = -verticalInput * tiltAngle * 0.5f;  

        currentTiltY = Mathf.Lerp(currentTiltY, -targetTiltY, tiltSmoothness * Time.deltaTime);

        transform.rotation = Quaternion.Euler(targetTiltX, currentTiltY, defaultRotationZ);
    }

    private void HandleShooting()
    {
        shootCooldownTimer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && shootCooldownTimer <= 0)
        {
            if (laserKogel != null && bulletSpawnPointTest != null)
            {
                Instantiate(laserKogel, bulletSpawnPointTest.transform.position, transform.rotation);
                shootCooldownTimer = shootCooldownDuration;
            }
        }
    }

    private void HandleDash()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.useGravity = false;

        Vector3 dashDirection = rb.velocity.normalized;
        if (dashDirection == Vector3.zero)
        {
            dashDirection = Vector3.right;
        }

        rb.velocity = dashDirection * dashingPower;
        TR.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        TR.emitting = false;
        rb.useGravity = true;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            game.ReportPlayerHit();
            Destroy(collision.gameObject);
        }
    }
}
