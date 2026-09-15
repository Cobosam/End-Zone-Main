using System.Collections;
using UnityEngine;

public class AmmoPlace : MonoBehaviour
{
    [Header("Mermi Ayarlarý")]
    [SerializeField] private float ammoCooldown = 20f; 
    [SerializeField] private int reduceAdrenalin = 33;
    [SerializeField] private int ammoIncreaseAmount = 30;
    [SerializeField] private float ammoIncreaseDuration = 8f; 

    private bool isPlayerNear = false;
    private bool canAmmo = true;
    private Player player;

    void Update()
    {
        if (isPlayerNear && canAmmo && Input.GetKeyDown(KeyCode.G))
        {
            if (player != null && player.currentAdrenalin >= reduceAdrenalin)
            {
                StartCoroutine(player.PlayAmmoEffect());
                player.currentAdrenalin -= reduceAdrenalin;
                player.UpdateAdrenalin();
                player.ekstraMermiGucu += ammoIncreaseAmount;
                StartCoroutine(RemovePowerRoutine(player, ammoIncreaseAmount, ammoIncreaseDuration));
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
        canAmmo = false;
        yield return new WaitForSeconds(ammoCooldown);
        canAmmo = true;
    }

    private IEnumerator RemovePowerRoutine(Player buffedPlayer, int verilenGuc, float sure)
    {
        yield return new WaitForSeconds(sure);

        if (buffedPlayer != null)
        {
            buffedPlayer.ekstraMermiGucu -= verilenGuc;
        }
    }
}