using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public int damage = 1;
    public float lifetime = 3f;
    public GameObject impactEffect;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bullet"))
            return;

        if (other.CompareTag("Enemy"))
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            enemy?.TakeDamage(damage);
        }

        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}