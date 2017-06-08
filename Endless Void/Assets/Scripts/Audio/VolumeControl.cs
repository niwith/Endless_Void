using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour {

    public Slider masterVolumeSlider;
    public float masterVolume;
    public Slider musicVolumeSlider;
    public float musicVolume;
    public Slider effectVolumeSlider;
    public float effectVolume;

    private bool hasInit = false;

    public void UpdateVolume()
    {
        if (hasInit)
        {
            masterVolume = masterVolumeSlider.value;
            musicVolume = musicVolumeSlider.value;
            effectVolume = effectVolumeSlider.value;
            AudioListener.volume = masterVolume;
        }
    }

    // Set all audio volumes and place the sliders in the right location
    void Start () {
        AudioListener.volume = masterVolume;
        masterVolumeSlider.value = masterVolume;
        musicVolumeSlider.value = musicVolume;
        effectVolumeSlider.value = effectVolume;
        hasInit = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
