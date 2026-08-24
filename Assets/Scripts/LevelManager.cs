using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI level1ZombieText;
    [Header("Bölüm Ayarlarý")]
    public int hedeflenenZombiSayisi = 10; 
    private int oldurulenZombi = 0;        

    [Header("Sonraki Sahne")]
    public string sonrakiLevelinAdi;       

    [Header("Baðlantýlar")]
    public ZombieSpawner zombieSpawner;    
    public SceneFader sceneFader;

    void Update()
    {
        level1ZombieText.text=oldurulenZombi.ToString();
    }
    public void ZombiOlduruldu()
    {
        oldurulenZombi++;
        Debug.Log("Zombi öldürüldü! Kalan hedef: " + (hedeflenenZombiSayisi - oldurulenZombi));

        if (oldurulenZombi >= hedeflenenZombiSayisi)
        {
            LeveliBitir();
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
            sceneFader.FadeOutAndLoad(sonrakiLevelinAdi);
        }
        else
        {
            SceneManager.LoadScene(sonrakiLevelinAdi);
        }
    }
}
