using UnityEngine;

public class ısık : MonoBehaviour
{
    public float rotationSpeed = 100f;  // Dönme hızı
    public float opacitySpeed = 0.5f;   // Opaklık artma hızı
    public float scaleSpeed = 0.1f;     // Büyüme hızı
    public float maxScaleMultiplier = 1.5f; // Maksimum büyüme oranı

    private SpriteRenderer spriteRenderer;
    private Color color;
    private bool basladi = false;
    private Vector3 baslangicOlcegi;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        color.a = 0f;  // Başlangıçta görünmez
        spriteRenderer.color = color;
        baslangicOlcegi = transform.localScale;
    }

    void Update()
    {
        if (!basladi) return;

        // Döndür
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        // Opaklığı artır
        if (color.a < 1f)
        {
            color.a += opacitySpeed * Time.deltaTime;
            spriteRenderer.color = color;
        }

        // Yavaş yavaş büyüt (ama sınırlı)
        if (transform.localScale.x < baslangicOlcegi.x * maxScaleMultiplier)
        {
            transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            basladi = true;
        }
    }
}
