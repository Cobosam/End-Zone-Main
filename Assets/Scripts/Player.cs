using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.Editor;

public class Player : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private WeaponManager weaponManager;
    private CapsuleCollider2D cl;
    public Transform atisNoktasi;
    private bool isReloading = false;
    private float reloadTime = 1.5f;
    public AudioClip deathSound;
    public AudioSource heartBeat;
    private AudioSource audio;
    private Color originalColor;
    public bool isAlive = true;
    private ZombieSpawner zs;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float PfireDuration = 0.5f;
    [SerializeField] private float SfireDuration = 1f;
    [SerializeField] private float RfireDuration = 0.2f;
    public Image healthBarFill;

    bool isShoot = false;

    void Start()
    {
        currentHealth = maxHealth;
        CanBariniGuncelle();
        audio= GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        cl=GetComponent<CapsuleCollider2D>();
        zs=FindAnyObjectByType<ZombieSpawner>();
        originalColor = sr.color;
        weaponManager = GetComponent<WeaponManager>();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            heartBeat.Stop();
            isAlive = false;
            rb.linearVelocity = new Vector2(0, 0);
            this.enabled = false;
            cl.enabled = false;
            zs.UretimiDurdur();
            GameOverTitle gameOverManager = FindAnyObjectByType<GameOverTitle>();
            if (gameOverManager != null)
            {
                gameOverManager.KarakterOldu();
            }
            SurvivalTimer timer = FindAnyObjectByType<SurvivalTimer>();
            if (timer != null)
            {
                timer.SayaciDurdur();
            }
            audio.PlayOneShot(deathSound);
            anim.SetTrigger("isDeath");
            weaponManager.WeaponDie();
            return;
        }
        HandleMovement();
        HandleFlips();
        HandleAnimations();
        HandleGun();
        HandleHPBar();
    }

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            moveX = moveSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveX = -moveSpeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveY = moveSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveY = -moveSpeed;
        }

        rb.linearVelocity = new Vector2(moveX, moveY);
    }

    private void HandleFlips()
    {
        float yatay = Input.GetAxisRaw("Horizontal");
        if (yatay > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (yatay < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void HandleAnimations()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
    }

    private void AtesEt()
    {
        GameObject dogruMermi = weaponManager.GetCurrentBullet();
        if(dogruMermi != null)
        {
            // Her silah için ateþ ettikten sonra bekleme süresi ekle
            if(dogruMermi == weaponManager.pistolBullet && isShoot==false)
            {
                isShoot = true;
                Instantiate(dogruMermi, atisNoktasi.position, atisNoktasi.rotation);
                weaponManager.PlayFireSound();
                weaponManager.DecreaseAmmo();
                StartCoroutine(FireDuration(PfireDuration));
            }
            else if (dogruMermi == weaponManager.rifleBullet && isShoot == false)
            {
                isShoot = true;
                Instantiate(dogruMermi, atisNoktasi.position, atisNoktasi.rotation);
                weaponManager.PlayFireSound();
                weaponManager.DecreaseAmmo();
                StartCoroutine(FireDuration(RfireDuration));
            }
            if (dogruMermi == weaponManager.shotgunBullet && isShoot == false)
            {
                isShoot = true;
                Instantiate(dogruMermi, atisNoktasi.position, atisNoktasi.rotation);
                weaponManager.PlayFireSound();
                weaponManager.DecreaseAmmo();
                StartCoroutine(FireDuration(SfireDuration));
            }
        }
    }
    private IEnumerator FireDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        isShoot = false;
    }

    private void HandleGun()
    {
        float yatay = Input.GetAxisRaw("Horizontal");

        if (yatay > 0f)
        {
            atisNoktasi.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (yatay < 0f)
        {
            atisNoktasi.localRotation = Quaternion.Euler(0f, 180f, 0f);
        }

        if (isReloading) return;
        
        if (Input.GetKeyDown(KeyCode.R) && weaponManager.currentWeaponAnim != null)
        {
            StartCoroutine(ReloadRoutine());
            return;
        }

        if (weaponManager.GetCurrentAmmo() <= 0)
        {
            StartCoroutine(ReloadRoutine());
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AtesEt();
        }
    }

    private System.Collections.IEnumerator ReloadRoutine()
    {
        isReloading = true;
        if (weaponManager.currentWeaponAnim != null) weaponManager.currentWeaponAnim.SetTrigger("isReloading");
        yield return new WaitForSeconds(reloadTime);
        weaponManager.ReloadCurrentWeapon();
        isReloading = false;
    }
    private System.Collections.IEnumerator ChangeColor()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = originalColor;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(ChangeColor());
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        CanBariniGuncelle();
    }
    private void HandleHPBar()
    {
        if (currentHealth <= 50 && currentHealth > 0)
        {
            if (!heartBeat.isPlaying)
            {
                heartBeat.Play();
            }
        }
        else
        {
            if (heartBeat.isPlaying)
            {
                heartBeat.Stop();
            }
        }
    }
    private void CanBariniGuncelle()
    {
        if (healthBarFill != null)
        {
            // Fill Amount 0 ile 1 arasý deðer alýr. 
            // Örneðin: 50 / 100 = 0.5 (Yarýsý dolu)
            // (float) yazmamýzýn sebebi, tam sayýlarý bölerken sonucun sýfýrlanmasýný engellemektir.
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }
    
}