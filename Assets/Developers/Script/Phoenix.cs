using System.Collections;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float fireRate = 0.2f;
    private float nextFireTime = 0f;

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

    [Header("Rotation Settings")]
    [SerializeField] private float maxRollAngle = 40f;
    [SerializeField] private float maxPitchAngle = 40f;
    [SerializeField] private float tiltSmoothness = 10f;
    [SerializeField] private float rotationSmoothness = 15f;
    private float currentRollAngle = 0f;
    private float currentPitchAngle = 0f;
    private Quaternion targetRotation;
    private Quaternion baseRotation = Quaternion.Euler(0, 90, 90);

    public GameManagement game;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = baseRotation; // Enforce initial rotation
        targetRotation = baseRotation;
    }

    private void Update()
    {
        HandleShooting();
        if (isDashing) return;
        HandleMovement();
        HandleDash();
        UpdateRotation();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetKey(KeyCode.D) ? 1f : (Input.GetKey(KeyCode.A) ? -1f : 0f);
        float verticalInput = Input.GetKey(KeyCode.W) ? 1f : (Input.GetKey(KeyCode.S) ? -1f : 0f);

        // Use world-space directions: X (left/right), Y (up/down)
        Vector3 targetVelocity = new Vector3(horizontalInput * moveSpeed, verticalInput * verticalMoveSpeed, 0f);

        currentVelocity = targetVelocity.magnitude > 0.1f
            ? Vector3.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime)
            : Vector3.Lerp(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);

        rb.velocity = currentVelocity;

        float pitchMultiplier = 2f; 

        // Visual banking/tilting
        currentRollAngle = Mathf.Lerp(currentRollAngle, -verticalInput * maxRollAngle, tiltSmoothness * Time.deltaTime);
        currentPitchAngle = Mathf.Lerp(currentPitchAngle, -horizontalInput * maxPitchAngle * pitchMultiplier, tiltSmoothness * Time.deltaTime);

        targetRotation = baseRotation * Quaternion.Euler(currentPitchAngle, 0f, currentRollAngle);
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
            dashDirection = transform.right; // Dash forward by default
        }

        rb.velocity = dashDirection * dashingPower;
        TR.emitting = true;

        // Add dramatic tilt during dash
        float dashRoll = dashDirection.x > 0 ? -45f : 45f;
        targetRotation = baseRotation * Quaternion.Euler(0, 0, dashRoll);

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
            EnemyBulletScript bullet = collision.gameObject.GetComponent<EnemyBulletScript>();
            if (bullet != null)
            {
                game.ReportPlayerHit(bullet.damage);
                Destroy(collision.gameObject);
            }
        }
    }

    private void HandleShooting()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
        }
    }
}