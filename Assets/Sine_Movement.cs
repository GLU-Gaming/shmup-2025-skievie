using UnityEngine;

public class Sine_Movement : MonoBehaviour
{
    float sinCenterY;
    public float amplitude = 1;
    public float frequency = 0.5f;

    public bool inverted = false;

    void Start()
    {
        sinCenterY = transform.position.y;
    }

  
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        Vector3 pos = transform.position;

        float sin = Mathf.Sin(pos.x * frequency) * amplitude;
        if (inverted)
        {
            sin *= -1;
        }
        pos.y = sinCenterY + sin;

        transform.position = pos;
    }
}
