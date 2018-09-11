﻿using UnityEngine;
using SBK.Unity;
using AlphaPuzzle.State;

public class Music : PSingle<Music>, IAudio
{    
    public static int MusicIndex { get; set; }
    public float VolumeLevel { get; set; }

    public float FadeOutTime = 3f;
    public float FadeInTime = 3f;    

    public const float FadeStopTime = 0.5f;

    [HideInInspector]
    public bool MusicEnabled = true;


    private grumbleAMP _grumble;


    protected override void PAwake()
    {
        _grumble = gameObject.GetComponent<grumbleAMP>();
    }

    protected override void PDestroy()
    {

    }

    void Update()
    {
        if (SceneSelector.Instance == null || _grumble == null) return;
        int jungleTrack = (int)MusicTracks.Jungle;
        switch (SceneSelector.Instance.CurrentBoard)
        {
            case BoardType.Adventure:
            case BoardType.FreeMap:
                if (MusicIndex == 0 || MusicIndex == 2)
                {
                    UnPauseTrack();
                    MusicIndex = jungleTrack;
                }
                break;
            case BoardType.Puzzle:
                if (MusicIndex == jungleTrack)
                {
                    PauseTrack();
                    MusicIndex = 0;
                }
                break;
        }
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
            //MusicManager.play(MusicClips[MusicIndex], FadeOutTime, FadeInTime);
            //_mp.playMusic(MusicClips[MusicIndex], FadeOutTime, FadeInTime);
            _grumble.PlaySong(index, 0, FadeInTime);
        }
    }

    public void PauseTrack()
    {
        //MusicManager.pause();
        //_mp.pauseMusic();
        _grumble.Pause();
    }

    public void UnPauseTrack()
    {
        //MusicManager.unpause();
        //_mp.unpauseMusic();
        _grumble.UnPause();
    }

    public void StopMusic()
    {
        //MusicManager.stop(FadeStopTime);
        //_mp.stopMusic(FadeStopTime);
        _grumble.StopAll(FadeStopTime);
    }

    public void Volume(float percent)
    {
        //MusicManager.setVolume(percent);
        //_mp.setMusicVolume(percent, 0f);
        _grumble.setGlobalVolume(percent);
        GameState.SettingsData.MusicVolume = percent;
        VolumeLevel = percent;
    }
}

public enum MusicTracks
{
    Jazzy = 0,
    Jungle = 1,
    Victory = 2
}