using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Barricade : MonoBehaviour, IDamageable
{
    [SerializeField]private int currentHealth;
    [SerializeField]private int maxHealth = 500;
    [SerializeField]private float colorTime = 0.5f;
    private SpriteRenderer sr;
    private Color originalColor;
    void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        originalColor=sr.color;
    }

    void Update()
    {
        if(currentHealth<=0)
        {
            GetComponent<Collider2D>().enabled = false;
            BarricadeBreak();
        }
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(ColorChange());
        currentHealth -=damage;
    }
    private void BarricadeBreak()
    {
        Destroy(gameObject);
    }
    private IEnumerator ColorChange()
    {
        sr.color = Color.darkRed;
        yield return new WaitForSeconds(colorTime);
        sr.color = originalColor;
    }
}
