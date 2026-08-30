using TMPro;
using UnityEngine;
// Görselleri (GameObject olarak) açýp kapatacaðýmýz için ekstra bir kütüphaneye gerek yok

public class WeaponManager : MonoBehaviour
{
    [Header("Silah Objeleri")]
    public GameObject pistol;
    public GameObject rifle;
    public GameObject shotgun; // Ýsimlendirmeyi shotgun olarak düzelttim
    
    [Header("Silah Mermileri")]
    public GameObject pistolBullet;
    public GameObject rifleBullet;
    public GameObject shotgunBullet;

    [Header("Mermi Miktarlarý")]
    public int pistolAmmo = 20;
    public int rifleAmmo = 30;
    public int shotgunAmmo = 8;

    [Header("UI Metinleri (Text)")]
    public TextMeshProUGUI pistolText;
    public TextMeshProUGUI rifleText;
    public TextMeshProUGUI shotgunText;

    [Header("UI Görselleri (Ýkonlar)")]
    // Oyun ekranýndaki silah resimlerini buraya atayacaðýz
    public GameObject pistolIcon;
    public GameObject rifleIcon;
    public GameObject shotgunIcon;
    
    [Header("Ses Efektleri (Audio Clips)")]
    public AudioClip pistolFireSound;
    public AudioClip rifleFireSound;
    public AudioClip shotgunFireSound; // Pompalý ateþ sesi

    public AudioClip pistolReloadSound;
    public AudioClip rifleReloadSound;
    public AudioClip shotgunReloadSound; // Pompalý mermi doldurma sesi

    private AudioSource audioSource;
    public Animator currentWeaponAnim;

    [Header("Muzzle Flash Efektleri")]
    public Animator pistolMuzzle;
    public Animator shoutgunMuzzle;
    public Animator rifleMuzzle;



    void Start()
    {
        SwitchWeapon(0);
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayFireSound()
    {
        if (audioSource == null) return;

        // Hangi silah aktifse onun ateþ sesini çal
        if (pistol.activeSelf && pistolFireSound != null)
        {
            audioSource.PlayOneShot(pistolFireSound);
            if (pistolMuzzle != null)
            {
                pistolMuzzle.SetTrigger("Fire");
            }
        }
        else if (rifle.activeSelf && rifleFireSound != null)
        {
            audioSource.PlayOneShot(rifleFireSound);
            if (rifleMuzzle != null)
            {
                rifleMuzzle.SetTrigger("Fire");
            }
        }
        else if (shotgun.activeSelf && shotgunFireSound != null)
        {
            audioSource.PlayOneShot(shotgunFireSound);
            if (shoutgunMuzzle != null)
            {
                shoutgunMuzzle.SetTrigger("Fire");
            }
        }
    }
    public void PlayReloadSound()
    {
        if (audioSource == null) return;

        if (pistol.activeSelf && pistolReloadSound != null) audioSource.PlayOneShot(pistolReloadSound);
        else if (rifle.activeSelf && rifleReloadSound != null) audioSource.PlayOneShot(rifleReloadSound);
        else if (shotgun.activeSelf && shotgunReloadSound != null) audioSource.PlayOneShot(shotgunReloadSound);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchWeapon(2);



        UpdateAmmoText();
    }

    private void SwitchWeapon(int weaponIndex)
    {
        pistol.SetActive(false);
        rifle.SetActive(false);
        shotgun.SetActive(false);

        pistolText.gameObject.SetActive(false);
        rifleText.gameObject.SetActive(false);
        shotgunText.gameObject.SetActive(false);

        if (pistolIcon != null) pistolIcon.SetActive(false);
        if (rifleIcon != null) rifleIcon.SetActive(false);
        if (shotgunIcon != null) shotgunIcon.SetActive(false);

        if (weaponIndex == 0)
        {
            pistol.SetActive(true);
            pistolText.gameObject.SetActive(true);
            if (pistolIcon != null) pistolIcon.SetActive(true);
            currentWeaponAnim = pistol.GetComponent<Animator>();

            if (pistolMuzzle != null)
            {
                pistolMuzzle.Rebind();
                pistolMuzzle.GetComponent<SpriteRenderer>().sprite = null;
            }
        }
        else if (weaponIndex == 1)
        {
            rifle.SetActive(true);
            rifleText.gameObject.SetActive(true);
            if (rifleIcon != null) rifleIcon.SetActive(true);
            currentWeaponAnim = rifle.GetComponent<Animator>();

            if (rifleMuzzle != null)
            {
                rifleMuzzle.Rebind();
                rifleMuzzle.GetComponent<SpriteRenderer>().sprite = null;
            }
        }
        else if (weaponIndex == 2)
        {
            shotgun.SetActive(true);
            shotgunText.gameObject.SetActive(true);
            if (shotgunIcon != null) shotgunIcon.SetActive(true);
            currentWeaponAnim = shotgun.GetComponent<Animator>();

            if (shoutgunMuzzle != null)
            {
                shoutgunMuzzle.Rebind();
                shoutgunMuzzle.GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }

    private void UpdateAmmoText()
    {
        if (pistol.activeSelf)
        {
            pistolText.text = pistolAmmo.ToString();
        }
        else if (rifle.activeSelf)
        {
            rifleText.text = rifleAmmo.ToString();
        }
        else if (shotgun.activeSelf)
        {
            shotgunText.text = shotgunAmmo.ToString();
        }
    }
    public int GetCurrentAmmo()
    {
        if (pistol.activeSelf) return pistolAmmo;
        if (rifle.activeSelf) return rifleAmmo;
        if (shotgun.activeSelf) return shotgunAmmo;
        return 0; 
    }

    public void DecreaseAmmo()
    {
        if (pistol.activeSelf && pistolAmmo > 0) pistolAmmo--;
        else if (rifle.activeSelf && rifleAmmo > 0) rifleAmmo--;
        else if (shotgun.activeSelf && shotgunAmmo > 0) shotgunAmmo--;
    }

    public void ReloadCurrentWeapon()
    {
        if (pistol.activeSelf) pistolAmmo = 12;       
        else if (rifle.activeSelf) rifleAmmo = 20;    
        else if (shotgun.activeSelf) shotgunAmmo = 8; 
    }
    public GameObject GetCurrentBullet()
    {
        if (pistol.activeSelf) return pistolBullet;
        if (rifle.activeSelf) return rifleBullet;
        if (shotgun.activeSelf) return shotgunBullet;
        return null; 
    }
    public void WeaponDie()
    {
        if (pistol.activeSelf)
        {
            pistol.SetActive(false);
        }
        else if (rifle.activeSelf)
        {
            rifle.SetActive(false);
        }
        else if (shotgun.activeSelf)
        {
            shotgun.SetActive(false);
        }
    }
}