using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ResolutionManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    private List<UnityEngine.Resolution> filteredResolutions = new List<UnityEngine.Resolution>();

    void Start()
    {
        BuildResolutionDropdown();
    }

    void BuildResolutionDropdown()
    {
        filteredResolutions.Clear();
        List<string> options = new List<string>();
        UnityEngine.Resolution[] allResolutions = Screen.resolutions;

        int currentResolutionIndex = 0; // Aktif çözünürlüðün sýrasýný tutacaðýz

        foreach (UnityEngine.Resolution res in allResolutions)
        {
            if (!filteredResolutions.Exists(r => r.width == res.width && r.height == res.height))
            {
                filteredResolutions.Add(res);
                options.Add(res.width + " x " + res.height);

                // Oyunun þu anki çözünürlüðünü listede bulursak sýrasýný kaydediyoruz
                if (res.width == Screen.width && res.height == Screen.height)
                {
                    currentResolutionIndex = filteredResolutions.Count - 1;
                }
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        // Menüde varsayýlan olarak mevcut çözünürlüðü seçili göster
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // YENÝ EKLENEN KISIM: Seçilen çözünürlüðü ekrana uygular
    public void SetResolution(int resolutionIndex)
    {
        UnityEngine.Resolution resolution = filteredResolutions[resolutionIndex];
        // Seçilen geniþliði, yüksekliði uygula ve tam ekran modunu koru
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}