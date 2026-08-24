using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Üretilecek Zombi Türleri")]
    // Köþeli parantez [] bu deðiþkenin bir "liste" olduðunu belirtir.
    // Unity arayüzünde buraya istediðin kadar zombi türü ekleyebilirsin.
    public GameObject[] zombiePrefabs;

    [Header("Doðma Noktalarý")]
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;

    [Header("Zaman Ayarlarý")]
    public float spawnInterval = 3f; // Kaç saniyede bir zombi doðacak?

    void Start()
    {
        // Oyun baþladýðýnda zombi üretme döngüsünü baþlatýr
        StartCoroutine(SpawnZombieRoutine());
    }

    private IEnumerator SpawnZombieRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnZombie();
        }
    }
    public void UretimiDurdur()
    {
        StopAllCoroutines(); 
        this.enabled = false; 
        Debug.Log("Zombi üretimi durduruldu, seviye tamamlandý!");
    }

    private void SpawnZombie()
    {
        // Güvenlik kontrolü: Eðer listeye hiç zombi eklenmemiþse hata vermesin diye iþlemi iptal et
        if (zombiePrefabs.Length == 0) return;

        // 1. HANGÝ ZOMBÝ? Listeden (0 ile listedeki eleman sayýsý arasýnda) rastgele bir zombi seç
        int randomZombieIndex = Random.Range(0, zombiePrefabs.Length);
        GameObject selectedZombie = zombiePrefabs[randomZombieIndex];

        // 2. HANGÝ YÖN? 0 veya 1 sayýsýný rastgele seç (0 ise Sol, 1 ise Sað)
        int randomSide = Random.Range(0, 2);
        Transform selectedPoint = randomSide == 0 ? leftSpawnPoint : rightSpawnPoint;

        // 3. ÜRETÝM AÞAMASI: Seçilen zombiyi, seçilen noktada oyuna dahil et
        if (selectedZombie != null && selectedPoint != null)
        {
            Instantiate(selectedZombie, selectedPoint.position, selectedPoint.rotation);
        }
    }
}
