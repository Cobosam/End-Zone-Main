using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private WeaponManager weaponManager;
    [SerializeField]private AudioClip backgroundSound;
    private AudioSource audioSource;
    void Start()
    {
        weaponManager = GetComponentInParent<WeaponManager>();
        audioSource = GetComponentInParent<AudioSource>();
        PlayBackgroundMusic();
    }

    // Update is called once per frame
    public void ReloadGunSound()
    {
        weaponManager.PlayReloadSound();
    }
    public void PlayBackgroundMusic()
    {
        if (audioSource != null) 
        {
            audioSource.PlayOneShot(backgroundSound);
        }
        else
        {
            return;
        }
    }
}
