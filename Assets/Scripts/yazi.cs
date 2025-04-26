using System.Collections;
using UnityEngine;
using TMPro;

public class YaziGosterici : MonoBehaviour
{
    public GameObject panel; // Paneli buraya sürükleyeceðiz
    public TextMeshProUGUI textObjesi; // Yazýyý göstereceðimiz TMP text
    public float yazmaHizi = 0.05f; // Harfler arasý bekleme süresi
    [TextArea(3, 10)] public string tamYazi; // Yazmak istediðimiz uzun yazý

    private void OnEnable()
    {
        if (panel != null)
            panel.SetActive(true); // Paneli aç
        StartCoroutine(HarfHarfYaz());
    }

    IEnumerator HarfHarfYaz()
    {
        textObjesi.text = ""; // Yazýyý temizle
        foreach (char harf in tamYazi)
        {
            textObjesi.text += harf; // Bir harf ekle
            yield return new WaitForSeconds(yazmaHizi); // Bekle
        }

        // Ýstersen yazý bitince paneli gizleyebilirsin (þu an açýk kalýyor)
        // panel.SetActive(false);
    }
}
