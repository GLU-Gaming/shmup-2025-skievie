using UnityEngine;

public class Sine_Left_to_right : MonoBehaviour
{
    public float moveSpeed = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;

        pos.x -= moveSpeed * Time.deltaTime;

        if (pos.x < -20
            )
        {
            Destroy(gameObject);
        }


        transform.position = pos;
    }
}
