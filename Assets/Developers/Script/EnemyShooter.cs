using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] public float Speed = 100;
    [SerializeField] private float destroyTime = 2;

    public GameManagement game; // script aan script 

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
