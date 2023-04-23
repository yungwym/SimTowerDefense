using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAudioController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    public void ToggleMusic()
    {
        AudioManager.audioManagerInstance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.audioManagerInstance.ToggleSFX();
    }

    public void ChangeMusicVolume()
    {
        AudioManager.audioManagerInstance.ChangeMusicVolume(musicSlider.value);
    }

    public void ChangeSFXVolume()
    {
        AudioManager.audioManagerInstance.ChangeSFXVolume(SFXSlider.value);
    }

}
