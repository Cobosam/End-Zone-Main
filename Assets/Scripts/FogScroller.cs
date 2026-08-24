using UnityEngine;
using UnityEngine.UI;

public class FogScroller : MonoBehaviour
{
    [Header("Sis Objesi")]
    public RawImage fogImage;

    [Header("Kayma Hýzý")]
    public float scrollSpeedX = 0.05f; // Saða doðru kayma hýzý
    public float scrollSpeedY = 0.02f; // Yukarý doðru kayma hýzý

    void Update()
    {
        // Resmin UV kordinatlarýný (iç dokusunu) al
        Rect rect = fogImage.uvRect;
        
        // Hýza ve zamana göre kordinatlarý kaydýr
        rect.x += scrollSpeedX * Time.deltaTime;
        //rect.y += scrollSpeedY * Time.deltaTime;
        
        // Yeni kordinatlarý resme geri yükle
        fogImage.uvRect = rect;
    }
}