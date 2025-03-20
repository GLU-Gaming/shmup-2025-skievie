using UnityEngine;

public class Shooter : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] public float Speed = 100;
    [SerializeField] private float destroyTime = 2;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * Speed; // afvuur snelheid

        Destroy(gameObject, destroyTime); // gaat weg na aantal seconde
    }

    
    void Update()
    {
        
    }
}
