using UnityEngine;

public class kitaplik : MonoBehaviour
{
    public GameObject yazýObjesi;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            yazýObjesi.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            yazýObjesi.SetActive(false);
        }
    }
}
