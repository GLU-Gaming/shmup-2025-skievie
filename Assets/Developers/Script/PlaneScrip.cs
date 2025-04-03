using System.Collections;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Shooting")]
    [SerializeField] private GameObject laserKogel;
    [SerializeField] private GameObject bulletSpawnPointTest;
    public float shootCooldownDuration = 0.2f;
    private float shootCooldownTimer = 0f;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float verticalMoveSpeed = 3f;
    public float acceleration = 5f;
    public float deceleration = 8f;
    private Vector3 currentVelocity;

    [Header("Dash")]
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashingPower = 10f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer TR;

    [Header("Animation")]
    [SerializeField] private float maxRollAngle = 40f; // Renamed from maxTiltAngle for clarity
    [SerializeField] private float maxPitchAngle = 20f; // New pitch angle for up/down movement
    [SerializeField] private float tiltSmoothness = 10f;
    [SerializeField] private float rotationSmoothness = 15f;
    [SerializeField] private float defaultRotationZ = 90f;
    private float currentTiltY = 0f;
    private float currentTiltZ = 0f;
    private float currentTiltX = 0f;
    private Quaternion targetRotation;

    public GameManagement game;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(0f, 0f, defaultRotationZ);
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        HandleShooting();
        if (isDashing) return;
        HandleMovement();
        HandleDash();
        UpdateRotation();
    }

    private void HandleShooting()
    {
        if (Input.GetKey(KeyCode.Space) && shootCooldownTimer <= 0f)
        {
            Shoot();
            shootCooldownTimer = shootCooldownDuration;
        }

        if (shootCooldownTimer > 0f)
        {
            shootCooldownTimer -= Time.deltaTime;
        }
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetKey(KeyCode.D) ? 1f : (Input.GetKey(KeyCode.A) ? -1f : 0f);
        float verticalInput = Input.GetKey(KeyCode.W) ? 1f : (Input.GetKey(KeyCode.S) ? -1f : 0f);

        Vector3 targetVelocity = new Vector3(horizontalInput * moveSpeed, verticalInput * verticalMoveSpeed, 0f);

        currentVelocity = targetVelocity.magnitude > 0.1f
            ? Vector3.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime)
            : Vector3.Lerp(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);

        rb.velocity = currentVelocity;

        // Roll (tilt around Z axis when moving left/right)
        float targetRoll = -horizontalInput * maxRollAngle;

        // Pitch (tilt around X axis when moving up/down)
        float targetPitch = -verticalInput * maxPitchAngle;

        // Slight Z rotation adjustment when moving left/right
        float targetZRotation = defaultRotationZ - Mathf.Abs(horizontalInput) * 5f;

        currentTiltY = Mathf.Lerp(currentTiltY, targetRoll, tiltSmoothness * Time.deltaTime);
        currentTiltX = Mathf.Lerp(currentTiltX, targetPitch, tiltSmoothness * Time.deltaTime);
        currentTiltZ = Mathf.Lerp(currentTiltZ, targetZRotation, tiltSmoothness * Time.deltaTime);

        targetRotation = Quaternion.Euler(currentTiltX, currentTiltY, currentTiltZ);
    }

    private void UpdateRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
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

        float dashTilt = dashDirection.x > 0 ? -25f : 25f;
        targetRotation = Quaternion.Euler(0f, dashTilt * 2f, defaultRotationZ + dashTilt);

        yield return new WaitForSeconds(dashingTime);

        TR.emitting = false;
        rb.useGravity = true;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            game.ReportPlayerHit();
            Destroy(collision.gameObject);
        }
    }

    private void Shoot()
    {
        if (laserKogel == null || bulletSpawnPointTest == null)
        {
            Debug.LogError("Assign bullet prefab and spawn point in the Inspector!");
            return;
        }

        GameObject bullet = Instantiate(laserKogel, bulletSpawnPointTest.transform.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = Vector3.right * 50f;
        }
    }
}
