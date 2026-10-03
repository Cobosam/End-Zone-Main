using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Sahne Geçiþ Yöneticisi")]
    public GameOverTitle gameOverTitle;
    public SceneFader sceneFader; 
    [SerializeField]private AudioClip menuClip;
    private AudioSource menuSource;
    void Start()
    {
        sceneFader=FindAnyObjectByType<SceneFader>();
        gameOverTitle=FindAnyObjectByType<GameOverTitle>();
        menuSource = GetComponent<AudioSource>();
        menuSource.PlayOneShot(menuClip);
    }
    public void OyunaBasla()
    {
        if (sceneFader != null)
        {
            sceneFader.FadeOutAndLoad("FirstCutScene");
            MenuMusic music = FindAnyObjectByType<MenuMusic>();
            if (music != null)
            {
                Destroy(music.gameObject);
            }
        }
    }
    public void Gameplay()
    {
        if (sceneFader!=null)
        {
            sceneFader.FadeOutAndLoad("Gameplay");
        }
    }

    public void OyundanCik()
    {
        Debug.Log("Oyundan çýkýldý!");
        Application.Quit();
    }

    public void MenuTurn()
    {
        if (sceneFader != null)
        {
            sceneFader.FadeOutAndLoad("MainMenu");
        }
    }
    public void Settings()
    {
        if (sceneFader != null)
        {
            sceneFader.FadeOutAndLoad("Settings");
        }
    }
    public void GameOverTurnMenu()
    {
        if(sceneFader != null)
        {
            if (gameOverTitle.gameOverMenu != null && gameOverTitle.gameOverCanvas != null)
            {
                sceneFader.FadeOutAndLoad("MainMenu");
                gameOverTitle.FadeOut();
            }
        }
    }
}
