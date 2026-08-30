using System.Collections;
using UnityEngine;

public class Healing : MonoBehaviour
{
    [Header("İyileştirme Ayarları")]
    [SerializeField] private int healAmount = 75;
    [SerializeField] private float healCooldown = 15f; 

    private bool isPlayerNear = false; 
    private bool canHeal = true;       
    private Player player;

    void Update()
    {
        if (isPlayerNear && canHeal && Input.GetKeyDown(KeyCode.H))
        {
            if (player != null)
            {
                player.Heal(healAmount); 
                StartCoroutine(CooldownRoutine());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
            player = collision.GetComponent<Player>(); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = false;
            player = null; 
        }
    }

    private IEnumerator CooldownRoutine()
    {
        canHeal = false;

        Debug.Log("Çorba içildi! Yenisinin pişmesi bekleniyor...");

        yield return new WaitForSeconds(healCooldown);

        canHeal = true;

        Debug.Log("Çorba tekrar hazır!");
    }
}
