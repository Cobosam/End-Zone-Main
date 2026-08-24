using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Sahne yüklemek için bu kütüphaneyi ekledik

public class SceneFader : MonoBehaviour
{
    [Header("Siyah Ekran Objesi")]
    public Image blackScreen;

    [Header("Açýlma/Kapanma Hýzý")]
    public float fadeSpeed = 1f;

    void Start()
    {
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        Color objectColor = blackScreen.color;
        while (objectColor.a > 0)
        {
            objectColor.a -= fadeSpeed * Time.deltaTime;
            blackScreen.color = objectColor;
            yield return null;
        }
        //blackScreen.gameObject.SetActive(false);
    }

    // --- YENÝ EKLENEN KISIM ---

    // Dýþarýdan (Menüden) bu fader'ý tetiklemek için kullanacaðýmýz metod
    public void FadeOutAndLoad(string sceneName)
    {
        StartCoroutine(FadeOutRoutine(sceneName));
    }

    private IEnumerator FadeOutRoutine(string sceneName)
    {
        // Ekraný tekrar görünür yap
        //blackScreen.gameObject.SetActive(true);
        Color objectColor = blackScreen.color;

        // Þeffaflýk (Alpha) 1 olana kadar (tamamen siyah olana kadar) döngüyü çalýþtýr
        while (objectColor.a < 1)
        {
            objectColor.a += fadeSpeed * Time.deltaTime;
            blackScreen.color = objectColor;
            yield return null;
        }

        // Ekran zifiri karanlýk olduðunda diðer sahneyi yükle
        SceneManager.LoadScene(sceneName);
    }
}
