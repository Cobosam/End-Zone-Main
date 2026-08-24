using System.Collections;
using UnityEngine;

public class GameOverTitle : MonoBehaviour
{
    [Header("Game Over Ekraný")]
    public CanvasGroup gameOverCanvas;
    public CanvasGroup gameOverMenu;

    [Header("Belirme Hýzý")]
    public float fadeSpeed = 1f;

    void Start()
    {
        // Oyun baþladýðýnda yazýnýn kesinlikle görünmez olduðundan ve týklanmalarý engellediðinden emin oluyoruz
        if (gameOverCanvas != null)
        {
            gameOverCanvas.alpha = 0f;
            gameOverCanvas.interactable = false;
            gameOverCanvas.blocksRaycasts = false;
        }
        if(gameOverMenu != null)
        {
            gameOverMenu.alpha = 0f;
            gameOverMenu.interactable = false;
            gameOverMenu.blocksRaycasts = false;
        }
    }

    // Karakter öldüðünde dýþarýdan bu fonksiyonu çaðýracaðýz
    public void KarakterOldu()
    {
        FadeIn();
    }

    private IEnumerator FadeInGameOver()
    {
        // Þeffaflýðý 0'dan 1'e doðru yavaþça artýr
        while (gameOverCanvas.alpha < 1f)
        {
            gameOverCanvas.alpha += fadeSpeed * Time.deltaTime;
            yield return null; // Bir sonraki kareyi bekle
        }

        // Yazý tamamen göründüðünde, eðer ileride buton eklersen týklanabilmelerini aktif et
        gameOverCanvas.interactable = true;
        gameOverCanvas.blocksRaycasts = true;
    }
    private IEnumerator FadeInGameOverMenu()
    {
        // Þeffaflýðý 0'dan 1'e doðru yavaþça artýr
        while (gameOverMenu.alpha < 1f)
        {
            gameOverMenu.alpha += fadeSpeed * Time.deltaTime;
            yield return null; // Bir sonraki kareyi bekle
        }

        // Yazý tamamen göründüðünde, eðer ileride buton eklersen týklanabilmelerini aktif et
        gameOverMenu.interactable = true;
        gameOverMenu.blocksRaycasts = true;
    }
    private IEnumerator FadeOutGameOver()
    {
        // Þeffaflýðý 0'dan 1'e doðru yavaþça artýr
        while (gameOverCanvas.alpha >0f)
        {
            gameOverCanvas.alpha -= fadeSpeed * Time.deltaTime;
            yield return null; // Bir sonraki kareyi bekle
        }

        // Yazý tamamen göründüðünde, eðer ileride buton eklersen týklanabilmelerini aktif et
        gameOverCanvas.interactable = false;
        gameOverCanvas.blocksRaycasts = false;
    }
    private IEnumerator FadeOutGameOverMenu()
    {
        // Þeffaflýðý 0'dan 1'e doðru yavaþça artýr
        while (gameOverMenu.alpha >0f)
        {
            gameOverMenu.alpha -= fadeSpeed * Time.deltaTime;
            yield return null; // Bir sonraki kareyi bekle
        }

        // Yazý tamamen göründüðünde, eðer ileride buton eklersen týklanabilmelerini aktif et
        gameOverMenu.interactable = false;
        gameOverMenu.blocksRaycasts = false;
    }
    public void FadeIn()
    {
        StartCoroutine(FadeInGameOver());
        StartCoroutine(FadeInGameOverMenu());
    }
    public void FadeOut()
    {
        StartCoroutine(FadeOutGameOver());
        StartCoroutine(FadeOutGameOverMenu());
    }
}