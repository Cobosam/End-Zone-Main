using UnityEngine;
using UnityEngine.Audio; // AudioMixer için gerekli
using UnityEngine.UI;    // Slider için gerekli

public class SettingsManager : MonoBehaviour
{
    [Header("Ses Ayarlarý")]
    public AudioMixer mainMixer;
    public Slider volumeSlider;

    void Start()
    {
        // 1. Oyuncu daha önce bir ses ayarý yaptýysa onu hafýzadan çek (Yoksa varsayýlan olarak 1, yani en yüksek sesi al)
        float savedVolume = PlayerPrefs.GetFloat("SavedMasterVolume", 1f);

        // 2. Slider'ýn çubuðunu kaydedilmiþ deðere getir
        volumeSlider.value = savedVolume;

        // 3. Sesi o deðere ayarla
        SetVolume(savedVolume);
    }

    // Bu fonksiyon Slider hareket ettikçe çalýþacak
    public void SetVolume(float volume)
    {
        // Unity'nin AudioMixer'ý 0 ile 1 arasýnda deðil, logaritmik desibel (dB) sisteminde çalýþýr.
        // Bu yüzden Slider'dan gelen 0-1 deðerini þu formülle desibele çeviriyoruz:
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);

        // Oyuncu oyunu kapatýp açtýðýnda ayarý hatýrlanmasý için hafýzaya (PlayerPrefs) kaydediyoruz
        PlayerPrefs.SetFloat("SavedMasterVolume", volume);
    }
}