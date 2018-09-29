using UnityEngine;
using SBK.Unity;
using AlphaPuzzle.State;

public class Music : PSingle<Music>, IAudio
{    
    public static int MusicIndex { get; set; }
    public float VolumeLevel { get; set; }        
    public float FadeInTime = 3f;    
    public const float FadeStopTime = 0.5f;

    [HideInInspector]
    public bool MusicEnabled = true;

    private grumbleAMP _grumble;

    protected override void PAwake()
    {
        _grumble = GetComponent<grumbleAMP>();
    }

    protected override void PDestroy()
    {

    }        

    void Update()
    {
        
    }

    public void PlayMusicTrack(MusicTracks track)
    {
        PlayMusicTrack((int)track);
    }

    public void PlayMusicTrack(int index)
    {
        MusicIndex = index;

        if (MusicEnabled)
        {            
            _grumble.PlaySong(index, 0, FadeInTime);
        }
    }

    public void PauseTrack()
    {        
        _grumble.Pause();
    }

    public void UnPauseTrack()
    {     
        _grumble.UnPause();
    }

    public void StopMusic()
    {   
        _grumble.StopAll(FadeStopTime);
    }

    public void Volume(float percent)
    {   
        _grumble.setGlobalVolume(percent);
        GameState.SettingsData.MusicVolume = percent;
        VolumeLevel = percent;
    }
}

public enum MusicTracks
{
    Menu = 0,
    Map = 1,
    Puzzle = 2
}