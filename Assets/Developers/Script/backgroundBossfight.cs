using UnityEngine;

public class backgroundBossfight : MonoBehaviour
{
    public float speed;

    [SerializeField] private Renderer bgRenderer;


    private void Update()
    {
        bgRenderer.material.mainTextureOffset -= new Vector2(speed * Time.deltaTime, 0);
    }
}
