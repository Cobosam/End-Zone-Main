using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneFader : MonoBehaviour
{
    [Header("Siyah Ekran Objesi")]
    public Image blackScreen;
    [Header("BloodMoon Text")]
    public CanvasGroup bloodMoonText;

    [Header("Açýlma/Kapanma Hýzý")]
    public float fadeSpeed = 1f;

    private ZombieSpawner zs;

    void Start()
    {
        zs= FindAnyObjectByType<ZombieSpawner>();
        StartCoroutine(FadeInRoutine());
        if (bloodMoonText != null)
        {
            bloodMoonText.alpha = 0f;
            bloodMoonText.interactable = false;
            bloodMoonText.blocksRaycasts = false;
        }
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
    }
    public void FadeOutAndLoad(string sceneName)
    {
        StartCoroutine(FadeOutRoutine(sceneName));
    }

    private IEnumerator FadeOutRoutine(string sceneName)
    {
        Color objectColor = blackScreen.color;

        while (objectColor.a < 1)
        {
            objectColor.a += fadeSpeed * Time.deltaTime;
            blackScreen.color = objectColor;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
    public void CallBloodMoonText()
    {
        StartCoroutine(BloodMoonSequence());
    }
    private IEnumerator TextDuration()
    {
        yield return new WaitForSeconds(5);
    }
    public IEnumerator BloodMoonSequence()
    {
        zs.PlayHowlSound();
        while (bloodMoonText.alpha < 1f)
        {
            bloodMoonText.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }
        bloodMoonText.alpha = 1f; 
        bloodMoonText.interactable = true;
        bloodMoonText.blocksRaycasts = true;

        yield return new WaitForSeconds(2.5f);

        while (bloodMoonText.alpha > 0f)
        {
            bloodMoonText.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
        bloodMoonText.alpha = 0f; 
        bloodMoonText.interactable = false;
        bloodMoonText.blocksRaycasts = false;
    }
}
