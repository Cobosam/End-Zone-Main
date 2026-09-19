using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI level1ZombieText;
    [SerializeField]TextMeshProUGUI level2ZombieText;
    [SerializeField]TextMeshProUGUI level3ZombieText;
    [Header("Bölüm Ayarlarý")]
    public int targetZombieAmount = 10; 
    private int deadZombies = 0;
    [SerializeField]private float surviveTime = 140;
    public float currentTime=0;

    [Header("Sonraki Sahne")]
    public string nextScene;       

    [Header("Baðlantýlar")]
    public ZombieSpawner zombieSpawner;    
    public SceneFader sceneFader;
    public Player player;

    void Start()
    {
        player = FindAnyObjectByType<Player>();    
    }
    void Update()
    {
        CheckTime();
        if(level1ZombieText != null)
        {
            level1ZombieText.text=deadZombies.ToString();
        }
        if (level2ZombieText != null)
        {
            level2ZombieText.text = deadZombies.ToString();
        }
        if (level3ZombieText != null)
        {
            level3ZombieText.text = deadZombies.ToString();
        }
    }
    public void ZombiOlduruldu()
    {
        deadZombies++;
        Debug.Log("Zombi öldürüldü! Kalan hedef: " + (targetZombieAmount - deadZombies));

        if (deadZombies >= targetZombieAmount)
        {
            LeveliBitir();
        }
    }
    public void CheckTime()
    {
        if(player.isAlive)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= surviveTime)
            {
                LeveliBitir();
            }
        }
    }

    private void LeveliBitir()
    {
        
        if (zombieSpawner != null)
        {
            zombieSpawner.UretimiDurdur();
        }

        if (sceneFader != null)
        {
            sceneFader.FadeOutAndLoad(nextScene);
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
