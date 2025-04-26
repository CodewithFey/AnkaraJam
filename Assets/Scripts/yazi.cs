using System.Collections;
using UnityEngine;
using TMPro;

public class YaziGosterici : MonoBehaviour
{
    public GameObject panel; // Paneli buraya s�r�kleyece�iz
    public TextMeshProUGUI textObjesi; // Yaz�y� g�sterece�imiz TMP text
    public float yazmaHizi = 0.05f; // Harfler aras� bekleme s�resi
    [TextArea(3, 10)] public string tamYazi; // Yazmak istedi�imiz uzun yaz�

    private void OnEnable()
    {
        if (panel != null)
            panel.SetActive(true); // Paneli a�
        StartCoroutine(HarfHarfYaz());
    }

    IEnumerator HarfHarfYaz()
    {
        textObjesi.text = ""; // Yaz�y� temizle
        foreach (char harf in tamYazi)
        {
            textObjesi.text += harf; // Bir harf ekle
            yield return new WaitForSeconds(yazmaHizi); // Bekle
        }

        // �stersen yaz� bitince paneli gizleyebilirsin (�u an a��k kal�yor)
        // panel.SetActive(false);
    }
}
