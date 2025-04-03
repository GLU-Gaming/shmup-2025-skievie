using System.Collections;
using UnityEngine;

public class BackgroundBossfight : MonoBehaviour
{
    public float speed;
    [SerializeField] private Gradient objectGradient;
    [SerializeField] private float gradientSpeed = 0.1f;

    [SerializeField] private Renderer objectRenderer; // GameObject met een Renderer
    private float _totalTime;

    [SerializeField] private Renderer bgRenderer; // Achtergrond renderer

    void Start()
    {
        // Controleer of objectRenderer correct is toegewezen
        if (objectRenderer == null)
        {
            Debug.LogError("Renderer is niet toegewezen! Sleep een object met een Renderer in de Inspector.");
            return;
        }

        StartCoroutine(AnimateObjectColor());
    }

    private void Update()
    {
        // Laat de achtergrond bewegen
        bgRenderer.material.mainTextureOffset -= new Vector2(speed * Time.deltaTime, 0);
    }

    IEnumerator AnimateObjectColor()
    {
        while (true)
        {
            // Bereken de kleur op basis van de tijd
            Color newColor = objectGradient.Evaluate((_totalTime % 1f));
            _totalTime += Time.deltaTime * gradientSpeed;

            // Pas de kleur toe op het materiaal
            objectRenderer.material.color = newColor;

            yield return null; // Wacht 1 frame voordat de loop doorgaat
        }
    }
}
