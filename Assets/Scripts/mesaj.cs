using UnityEngine;

public class mesaj : MonoBehaviour
{
    public GameObject yaz�Objesi;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            yaz�Objesi.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            yaz�Objesi.SetActive(false);
        }
    }
}
