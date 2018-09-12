using AlphaPuzzle.State;
using UnityEngine;
using SBK.Unity;
using System;
using System.Collections;
using System.Linq;

public class SceneSelector : DSingle<SceneSelector>
{
    public bool StartPuzzle = false;
    public Node CurrentNode;    
    public BoardType CurrentBoard = BoardType.Adventure;
    public float CameraMoveSpeed = 0.3f;
    public bool IsCameraLocked = true;    

    public GameObject InGameUI;
    public GameObject MapViewUI;
    public GameObject PuzzleView;
    public GameObject HandAnimation;
    public VictoryParent VictoryParent { get; private set; }

    private BoardType _selectedBoardType;
    private readonly string[] _selectedFreeLetter = new string[2];

    private readonly Vector3 _countingBoardPos = new Vector3(-90f, 0 ,-10f);
    private readonly Vector3 _puzzleBoardPos = new Vector3(0, 0, -10f);    
    private readonly Vector3 _victoryBoardPos = new Vector3(207f, 3.5f, -10f);
    private readonly Vector3 _mapStartingPos = new Vector3(77.5f,15.5f,-10f);
    
    protected override void PAwake()
    {
        VictoryParent = gameObject.transform.Find("VictoryBoard").GetComponent<VictoryParent>();
        TriggerAdImpression();
    }

    protected override void PDestroy()
    {
        
    }

    void Start()
    {
        GameState.LoadData();
        if (CurrentNode.name == "A")
        {
            CurrentNode.CanSelect = false;
            GameState.DragEnabled = true;
            if (HandAnimation != null)
                StartCoroutine("DisplayHand");
        }

        StartGameScene(GameState.LoadBoard);
    }

    public void StartGameScene(BoardType gameBoardType)
    {
        _selectedBoardType = gameBoardType;
        GameState.Victory = false;
        MoveToStartBoard();        
        //Banner Ads
        AdMobBanners.Instance.ShowAdBanner();
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
        IsCameraLocked = true;
        //Banner Ads
        AdMobBanners.Instance.HideAdBanner();        
        MoveCamera(_puzzleBoardPos, false);
        PuzzleView.SetActive(true);
        InGameUI.SetActive(true);
        MapViewUI.SetActive(false);        
    }

    public void MoveToVictoryBoard()
    {
        StopAllCoroutines();
        CurrentBoard = BoardType.Victory;
        GameState.Victory = true;
        GameState.SettingsData.WinCount++;
        IsCameraLocked = true;
        //Banner Ads
        AdMobBanners.Instance.HideAdBanner();
        Music.Instance.PlayMusicTrack(MusicTracks.Victory);        
        MoveCamera(_victoryBoardPos,false);
        SoundManager.PlaySoundWithDelay(VictoryParent.CheerSound, 1.5f);
        VictoryParent.StartEffects();
        Invoke("TriggerWinEvents", 3f);
        InGameUI.SetActive(false);
        MapViewUI.SetActive(false);
        PuzzleView.SetActive(false);
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

    private void TriggerAdImpression()
    {
        /*if(AdMobBanners.Instance.StartBannerAds)
        {
            char[] letters = { 'G', 'N', 'P', 'U', 'X' };                        
            for (int i = 0; i <= 1; i++)
            {
                int random = UnityEngine.Random.Range(0, letters.Length);
                string letter = letters[random].ToString();
                while (_selectedFreeLetter[0] == letter) continue;
                _selectedFreeLetter[i] = letter;
            }            
        }*/
    }

    public void MoveToMapBoard(string letter, bool lerp = false)
    {
        if (string.IsNullOrEmpty(letter)) return;
        CurrentBoard = _selectedBoardType;
        IsCameraLocked = false;
        //Banner Adds
        var banner = AdMobBanners.Instance;
        banner.ShowAdBanner();
        foreach (var l in NodeManager.Instance.NodeList)
        {
            if (l.name.ToUpper() == letter.ToUpper())
            {
                Transform t = l.transform;
                Vector3 camPoint = new Vector3(t.position.x,t.position.y, -10.0f);
                MoveCamera(camPoint, lerp);
                CurrentNode = l;
                CurrentNode.CanSelect = true;
                //Banner ads
                if (banner.StartBannerAds && _selectedFreeLetter.Contains(letter))                
                    Notifications.Instance.InGameNotification(InGameNotificationTypes.FreetoPay);                
                break;
            }
        }

        InGameUI.SetActive(false);
        MapViewUI.SetActive(true);
        PuzzleView.SetActive(false);
    }

    public void UnlockMapBoard()
    {
        if (CurrentBoard != BoardType.FreeMap) return;
        foreach (var node in NodeManager.Instance.NodeList)
        {
            node.CanSelect = true;            
        }
        var treasure = GameObject.FindGameObjectsWithTag("Treasure");
        foreach (var t in treasure)
            t.SetActive(false);
    }

    public void MoveToStartBoard()
    {
        CurrentBoard = _selectedBoardType;
        if (_selectedBoardType == BoardType.FreeMap)
            UnlockMapBoard();
        IsCameraLocked = false;
        Camera.main.transform.position = _mapStartingPos;
        Music.Instance.PlayMusicTrack(MusicTracks.Jazzy);

        InGameUI.SetActive(false);
        MapViewUI.SetActive(true);
        PuzzleView.SetActive(false);
    }

    private IEnumerator DisplayHand()
    {
        HandAnimation.SetActive(true);
        yield return new WaitForSeconds(3f);
        HandAnimation.SetActive(false);
    }   

}