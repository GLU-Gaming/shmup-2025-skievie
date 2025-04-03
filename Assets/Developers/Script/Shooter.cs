using UnityEngine;

public class Shooter : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] public float Speed = 100;
    [SerializeField] private float destroyTime = 2;

    public GameManagement game;

    void Start()  
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * Speed; 

        Destroy(gameObject, destroyTime); 
    }

    
    void Update()
    {
        
    }
}
