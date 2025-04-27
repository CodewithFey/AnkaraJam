using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource; // Müzik çalacak AudioSource
    public Slider volumeSlider;     // Ses ayarı için Slider

    private void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = musicSource.volume; // Slider başlangıçta müzik sesine eşit olsun
            volumeSlider.onValueChanged.AddListener(ChangeVolume); // Slider hareket edince ChangeVolume çağrılır
        }
    }

    private void ChangeVolume(float value)
    {
        musicSource.volume = value; // Slider değeri müzik sesini ayarlar
    }
}
