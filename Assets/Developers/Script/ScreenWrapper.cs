using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    private float screenLeft, screenRight, screenTop, screenBottom;

    private void Start()
    {
        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // reken bounds 
        screenLeft = -camWidth;
        screenRight = camWidth;
        screenTop = camHeight;
        screenBottom = -camHeight;
    }

    private void Update()
    {
        Vector3 newPosition = transform.position;

        // X
        if (newPosition.x > screenRight)
        {
            newPosition.x = screenLeft;
        }
        else if (newPosition.x < screenLeft)
        {
            newPosition.x = screenRight;
        }

        // Y 
        if (newPosition.y > screenTop)
        {
            newPosition.y = screenBottom;
        }
        else if (newPosition.y < screenBottom)
        {
            newPosition.y = screenTop;
        }

        transform.position = newPosition;
    }
}
