using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Üretilecek Zombi Türleri")]
    public GameObject[] zombiePrefabs;

    [Header("Doðma Noktalarý")]
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;

    [Header("Zaman Ayarlarý")]
    private float spawnInterval;
    [SerializeField] private float bloodMoonTime = 10f;
    public bool canSpawn = true;
    public AudioSource audio;
    public AudioClip zombieHowls;
    public AudioClip zombieHorde;
    public AudioClip rainSoundEffect;

    [Header("Limit Ayarlarý")]
    public int maxZombiesToSpawn = -1; 
    private int spawnedZombiesCount = 0; 

    private SceneFader sf;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        spawnInterval = 2f;
        sf = FindAnyObjectByType<SceneFader>();
        audio.PlayOneShot(rainSoundEffect);

        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Level 1")
        {
            maxZombiesToSpawn = 15;
        }
        else if (currentScene == "Level 2")
        {
            maxZombiesToSpawn = 30;
        }
        else if (currentScene == "Level 3")
        {
            maxZombiesToSpawn = -1; 
            StartCoroutine(BloodMoonRoutine());
        }

        StartCoroutine(SpawnZombieRoutine());
    }

    private IEnumerator SpawnZombieRoutine()
    {
        while (true)
        {
            if (canSpawn)
            {
                if (maxZombiesToSpawn != -1 && spawnedZombiesCount >= maxZombiesToSpawn)
                {
                    Debug.Log("Zombi kotasý doldu! Üretim durduruluyor.");
                    canSpawn = false;
                    yield break; 
                }

                SpawnZombie();
                spawnedZombiesCount++; 

                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void PlayHowlSound()
    {
        audio.PlayOneShot(zombieHowls);
    }

    public void UretimiDurdur()
    {
        StopAllCoroutines();
        this.enabled = false;
        Debug.Log("Zombi üretimi durduruldu, seviye tamamlandý!");
    }

    private void SpawnZombie()
    {
        if (zombiePrefabs.Length == 0) return;

        int randomZombieIndex = Random.Range(0, zombiePrefabs.Length);
        GameObject selectedZombie = zombiePrefabs[randomZombieIndex];

        int randomSide = Random.Range(0, 2);
        Transform selectedPoint = randomSide == 0 ? leftSpawnPoint : rightSpawnPoint;

        if (selectedZombie != null && selectedPoint != null)
        {
            Instantiate(selectedZombie, selectedPoint.position, selectedPoint.rotation);
        }
    }

    private IEnumerator BloodMoonRoutine()
    {
        yield return new WaitForSeconds(bloodMoonTime);

        canSpawn = false;

        if (sf != null)
        {
            yield return StartCoroutine(sf.BloodMoonSequence());
        }

        spawnInterval = 0.8f;
        canSpawn = true;
        audio.PlayOneShot(zombieHorde);
    }
}