using UnityEngine;

public class A : MonoBehaviour
{
    public float speed = 0f;
    public float hareketHizi = 3f; // Yürüyüş hızı
    private Rigidbody2D r2d;
    private Animator animator;

    void Start()
    {
        r2d = GetComponent<Rigidbody2D>(); // Rigidbody2D'yi alıyoruz
        animator = GetComponent<Animator>(); // Animator'u alıyoruz
    }

    void Update()
    {
        // Space tuşuna basılıyorsa
        if (Input.GetKey(KeyCode.Space))
        {
            speed = 1f;
        }
        else
        {
            speed = 0f;
        }

        // Animasyonu güncelle
        animator.SetFloat("speed", speed);
    }

    void FixedUpdate()
    {
        // Fiziksel hareketi uygula
        if (speed > 0f)
        {
            r2d.linearVelocity = new Vector2(hareketHizi, r2d.linearVelocity.y);
        }
        else
        {
            r2d.linearVelocity = new Vector2(0f, r2d.linearVelocity.y);
        }
    }
}
