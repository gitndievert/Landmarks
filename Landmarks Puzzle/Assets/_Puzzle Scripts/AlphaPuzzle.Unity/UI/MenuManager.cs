﻿using UnityEngine;
using AlphaPuzzle.State;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public bool OpenApk = false;
    public string GooglePlayStoreUrl;
    public string IOSStoreUrl;
    public string AmazonStoreUrl;

    public GameObject LoadingScreen;

    void Start()
    {        
        if (GameState.LoadBoard == BoardType.Menu)
            Music.Instance.PlayMusicTrack(MusicTracks.Jungle);
        GameState.LoadData();
        GameState.SaveData();
    }

    public void SaveState()
    {
        GameState.SaveData();
    }
    
    public void OnClick_LoadAdventureMode()
    {
        //Notifications.Instance.PopNotification("This is a test");
        //Notifications.Instance.ScheduleNotification("Adventure Puzzle ABC", "Make your way to the tresure!", 60);
        GameState.LoadBoard = BoardType.Adventure;
        //UnityAdServices.Instance.ShowAd();
        LoadMap();
    }

    public void OnClick_LoadFreePlayMode()
    {        
        GameState.LoadBoard = BoardType.FreeMap;
        LoadMap();
    }

    public void OnClick_LoadMainMenu()
    {
        GameState.LoadBoard = BoardType.Menu;
        SceneManager.UnloadSceneAsync("SplitPuzzle");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void OnClick_Quit()
    {
        Application.Quit();
    }    

    public void OnClick_OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void OnClick_OpenStoreUrl()
    {
        if(OpenApk)
            OnClick_OpenURL("");
        else
            OnClick_OpenURL("");
    }
    
    private void LoadMap()
    {
        LoadingScreen.SetActive(true);
        SceneManager.LoadScene("SplitPuzzle", LoadSceneMode.Single);
    }
}
