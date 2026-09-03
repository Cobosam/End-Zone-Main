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


    private SceneFader sf;

    void Start()
    {
        audio= GetComponent<AudioSource>();
        spawnInterval = 2f;
        StartCoroutine(SpawnZombieRoutine());
        sf= FindAnyObjectByType<SceneFader>();

        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Level 3")
        {
            StartCoroutine(BloodMoonRoutine());
        }
    }

    private IEnumerator SpawnZombieRoutine()
    {
        while (true) 
        {
            if (canSpawn)
            {
                SpawnZombie();
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
