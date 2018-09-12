using AlphaPuzzle.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : UIPanel
{
    public Slider MusicSlider;
    public Slider AudioSlider;

    protected override void OnEnable()
    {
        base.OnEnable();
        MusicSlider.value = Music.Instance.VolumeLevel;
        AudioSlider.value = SoundManager.Instance.VolumeLevel;
    }
    
    public void ChangeMusicVol()
    {
        Music.Instance.Volume(MusicSlider.value);
    }

    public void ChangeSoundVol()
    {
        SoundManager.Instance.Volume(AudioSlider.value);
    }
    
}
