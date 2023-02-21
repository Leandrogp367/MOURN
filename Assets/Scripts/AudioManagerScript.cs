using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerScript : MonoBehaviour
{
    public AudioMixer mainMixer;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("MasterVolumeParam"))
        {
            mainMixer.SetFloat("MasterVolumeParam", PlayerPrefs.GetFloat("MasterVolumeParam"));
        }

        if (PlayerPrefs.HasKey("MusicVolumeParam"))
        {
            mainMixer.SetFloat("MusicVolumeParam", PlayerPrefs.GetFloat("MusicVolumeParam"));
        }

        if (PlayerPrefs.HasKey("SfxVolumeParam"))
        {
            mainMixer.SetFloat("SfxVolumeParam", PlayerPrefs.GetFloat("SfxVolumeParam"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
