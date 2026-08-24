using UnityEngine;

public class AnimationBridge : MonoBehaviour
{
    // Üst objemizdeki ana scripti tutacaðýmýz deðiþken
    private Zombie_Big mainZombieScript;

    void Start()
    {
        // Oyun baþladýðýnda bir üst objeye (Parent) çýk ve Zombie_Big scriptini bul
        mainZombieScript = GetComponentInParent<Zombie_Big>();
    }

    // Animator'ýn göreceði ve tetikleyeceði köprü fonksiyonu
    public void CallDealDamage()
    {
        if (mainZombieScript != null)
        {
            // Asýl hasar verme kodunu çalýþtýr
            mainZombieScript.DealDamage();
        }
    }
    public void CallEnableMovement()
    {
        if (mainZombieScript != null)
        {
            // Asýl hasar verme kodunu çalýþtýr
            mainZombieScript.EnableMovement();
        }
    }
    public void CallDisableMovement()
    {
        if (mainZombieScript != null)
        {
            // Asýl hasar verme kodunu çalýþtýr
            mainZombieScript.DisableMovement();
        }
    }
    public void CallDamageSound()
    {
        if (mainZombieScript != null)
        {
            // Asýl hasar verme kodunu çalýþtýr
            mainZombieScript.DamageSound();
        }
    }
}
