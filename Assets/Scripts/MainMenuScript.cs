using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class MainMenuScript : MonoBehaviour
{
    public string level;
    public Toggle vsyncToggle; 
    public int resolutionValue, displayValue;
    public List<ResList> resolutionList = new List<ResList>();
    public List<DisplayList> displayList = new List<DisplayList>();
    public string walkForwardKey, walkBackwardKey, walkRightKey, walkLeftKey, interactKey;
    public TMP_Text resolutionLabel, displayLabel;
    public FullScreenMode displayMode;
    public TMP_Text masterValueLabel, musicValueLabel, sfxValueLabel;
    public Slider masterSlider, musicSlider, sfxSlider;
    public AudioMixer mainMixer;


    void Start()
    {
        UpdateResLabel();
        UpdateDisplayLabel();

        if(QualitySettings.vSyncCount == 0)
        {
            vsyncToggle.isOn = false;
        }
        else
        {
            vsyncToggle.isOn = true;
        }

        float vol;
        mainMixer.GetFloat("MasterVolumeParam", out vol);
        masterSlider.value = vol;
        masterValueLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();

        mainMixer.GetFloat("MusicVolumeParam", out vol);
        masterSlider.value = vol;
        musicValueLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        mainMixer.GetFloat("SfxVolumeParam", out vol);
        masterSlider.value = vol;
        sfxValueLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(level);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResLeft()
    {
        resolutionValue--;
        if(resolutionValue < 0)
        {
            resolutionValue = resolutionList.Count - 1;
        }
        UpdateResLabel();
    }
    public void ResRight()
    {
        resolutionValue++;
        if(resolutionValue > resolutionList.Count - 1)
        {
            resolutionValue = 0;
        }
        UpdateResLabel();
    }
    public void DisplayLeft()
    {
        displayValue--;
        if (displayValue < 0)
        {
            displayValue = displayList.Count - 1;
        }
        UpdateDisplayLabel();
    }
    public void DisplayRight()
    {
        displayValue++;
        if (displayValue > displayList.Count - 1)
        {
            displayValue = 0;
        }
        UpdateDisplayLabel();
    }
    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutionList[resolutionValue].horizontal.ToString() + "x" + resolutionList[resolutionValue].vertical.ToString();
    }
    public void UpdateDisplayLabel()
    {
        displayLabel.text = displayList[displayValue].label;
        displayMode = (FullScreenMode) displayList[displayValue].value;
    }
    public void ApplyVideoSettings()
    {
        Screen.SetResolution(resolutionList[resolutionValue].horizontal, resolutionList[resolutionValue].vertical, displayMode);

        if(vsyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }
    public void SetMasterVolume()
    {
        masterValueLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();

        mainMixer.SetFloat("MasterVolumeParam", masterSlider.value);

        PlayerPrefs.SetFloat("MasterVolumeParam", masterSlider.value);
    }
    public void SetMusicVolume()
    {
        musicValueLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        mainMixer.SetFloat("MusicVolumeParam", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVolumeParam", musicSlider.value);
    }
    public void SetSfxVolume()
    {
        sfxValueLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();

        mainMixer.SetFloat("SfxVolumeParam", sfxSlider.value);

        PlayerPrefs.SetFloat("SfxVolumeParam", sfxSlider.value);
    }
}

[System.Serializable]
public class ResList
{
    public int horizontal, vertical;
}

[System.Serializable]
public class DisplayList
{
    public string label;
    public int value;
}
