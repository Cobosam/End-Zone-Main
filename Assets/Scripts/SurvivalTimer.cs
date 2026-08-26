using UnityEngine;
using TMPro; 

public class SurvivalTimer : MonoBehaviour
{
    [Header("UI Bağlantısı")]
    public TextMeshProUGUI timerText;

    private float gecenZaman = 0f;
    private bool zamanIsliyor = true;

    void Update()
    {
        if (zamanIsliyor)
        {
            gecenZaman += Time.deltaTime;
            ZamaniGuncelle();
        }
    }

    private void ZamaniGuncelle()
    {
        int dakika = Mathf.FloorToInt(gecenZaman / 60);
        int saniye = Mathf.FloorToInt(gecenZaman % 60);

        timerText.text = string.Format("{0:00}:{1:00}", dakika, saniye);
    }

    public void SayaciDurdur()
    {
        zamanIsliyor = false;
        Debug.Log("Oyun bitti! Hayatta kalınan süre: " + timerText.text);
    }
}