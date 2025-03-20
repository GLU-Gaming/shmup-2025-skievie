using UnityEngine;

public class Shooter : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] public float Speed = 100;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * Speed;
    }

    
    void Update()
    {
        
    }
}
