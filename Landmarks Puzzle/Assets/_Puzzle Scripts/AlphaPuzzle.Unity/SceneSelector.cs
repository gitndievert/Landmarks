using AlphaPuzzle.State;
using UnityEngine;
using SBK.Unity;
using System;
using System.Collections;
using System.Linq;

public class SceneSelector : DSingle<SceneSelector>
{
    public const float BOARD_CAMERA_SIZE = 8.5f;
    public const float MAP_CAMERA_SIZE = 9f;

    public bool StartPuzzle = false;
    public Node CurrentNode;    
    public BoardType CurrentBoard = BoardType.World;
    public float CameraMoveSpeed = 0.3f;
    public bool IsCameraLocked = true;    

    public GameObject InGameUI;
    public GameObject MapViewUI;
    public GameObject PuzzleView;
    public GameObject HandAnimation;

    public AudioClip MapInstructions;

    private BoardType _selectedBoardType;
    private readonly string[] _selectedFreeLetter = new string[2];

    private readonly Vector3 _countingBoardPos = new Vector3(-90f, 0 ,-10f);
    private readonly Vector3 _puzzleBoardPos = new Vector3(0, 0, -10f);        
    private readonly Vector3 _mapStartingPos = new Vector3(86.8f,6.5f,-10f);
    
    protected override void PAwake()
    {        
        
    }

    protected override void PDestroy()
    {
        
    }

    void Start()
    {
        GameState.LoadData();
        GameState.DragEnabled = true;
        if (HandAnimation != null)
            StartCoroutine("DisplayHand");
        StartGameScene(GameState.LoadBoard);
    }

    public void StartGameScene(BoardType gameBoardType)
    {
        _selectedBoardType = gameBoardType;        
        MoveToMapBoard(_mapStartingPos);
        Music.Instance.PlayMusicTrack(MusicTracks.Map);
        if (MapInstructions != null)
            SoundManager.PlaySoundWithDelay(MapInstructions, 2f);    
        //Banner Ads
        //AdMobBanners.Instance.ShowAdBanner();
        //UnityAdServices.Instance.PauseAd();
        InGameUI.SetActive(false);
        MapViewUI.SetActive(true);
        PuzzleView.SetActive(false);
    }

    public void MoveCamera(Vector3 movepoint, bool lerp, Camera camera = null)
    {
        Camera cam = camera ?? Camera.main;
        if (lerp)
            StartCoroutine(Camera.main.transform.Lerp(new Vector3(movepoint.x, movepoint.y, -10f), CameraMoveSpeed));
        else
            cam.transform.position = movepoint;
    }

    public void MoveToPuzzleBoard()
    {
        StopAllCoroutines();        
        CurrentBoard = BoardType.Puzzle;
        Camera.main.orthographicSize = BOARD_CAMERA_SIZE;
        IsCameraLocked = true;
        //Banner Ads
        //AdMobBanners.Instance.HideAdBanner();        
        //UnityAdServices.Instance.PauseAd();       
        
        MoveCamera(_puzzleBoardPos, false);
        PuzzleView.SetActive(true);
        InGameUI.SetActive(true);
        MapViewUI.SetActive(false);        
    }

    private void TriggerWinEvents()
    {
        var state = GameState.SettingsData;
        state.WinCount++;
        if (state.Reviewed) return;
        Notifications.Instance.InGameNotification(InGameNotificationTypes.RateOurApp);        
        state.Reviewed = true;        
        GameState.SaveData();
    }

    public void MoveToMapBoard(Vector3 lastPos)
    {
        CurrentBoard = _selectedBoardType;        
        IsCameraLocked = false;
        Camera.main.orthographicSize = MAP_CAMERA_SIZE;
        //var banner = UnityAdServices.Instance;
        //banner.UnPauseAd();
        Camera.main.transform.position = lastPos;        

        InGameUI.SetActive(false);
        MapViewUI.SetActive(true);
        PuzzleView.SetActive(false);
    }

    public void MoveToMapBoard()
    {
        MoveToMapBoard(_mapStartingPos);
    }

    private IEnumerator DisplayHand()
    {
        HandAnimation.SetActive(true);
        yield return new WaitForSeconds(3f);
        HandAnimation.SetActive(false);
    }   

}