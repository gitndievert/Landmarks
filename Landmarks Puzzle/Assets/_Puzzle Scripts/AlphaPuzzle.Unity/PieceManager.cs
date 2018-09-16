using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using AlphaPuzzle.State;
using TMPro;

public class PieceManager : BoardManager<PieceManager>
{
    public float SplitTimerSec = 0.25f;
      
    public float SecToNextPiece
    {
        get {  return 2.0f; }
    }
        
    public Vector3[] PresetSplitPositions;
    public GameObject AssociatedText;
    
    //Particles
    public GameObject PiecePopEffect;
    public GameObject[] PuzzleWinEffects;

    public Vector3 PieceParentPos
    {
        get
        {
            if (_pieceParent == null) return Vector3.zero;
            return _pieceParent.transform.position;
        }
    }

    private PuzzlePiece[] _associatedPieces;
    //private GameObject _letterPrefab;
    private PieceParent _pieceParent;
    private bool _waitForPuzzle;
    private bool _pieceSplitComplete;
    private Animation _pieceAnim;
    private TextMeshProUGUI _aText;
    private GameObject _animal;
        

    protected override void PAwake()
    {
        _aText = AssociatedText.GetComponent<TextMeshProUGUI>();
    }

    protected override void PDestroy()
    {

    }

    void Update()
    {
        //Do Stuff for the WIN events!
        //Then start next Puzzle
        if (PiecePrefab != null)
        {
            if (CheckForWin() && !_waitForPuzzle && !GameState.Victory)
            {
                _waitForPuzzle = true;
                SoundManager.PlaySound(SoundManager.Instance.Clapping,2);
                var letterQueue = new[]
                {
                    SoundManager.Instance.GetRandomMotivation/*,
                    _pieceParent.AudioLetter,
                    _pieceParent.AudioLetterSound,
                    _pieceParent.AudioLetterExample*/
                };

                float totalSec = 0f,soundDelay = 0.8f;
                foreach (var letter in letterQueue)
                    totalSec += letter.length;
                totalSec += 3.0f;
                StartCoroutine(AddLetterAssociation(soundDelay,totalSec));
                SoundManager.QueueSounds(letterQueue, soundDelay);            
            }                     
        }       
    }
    
    public void SelectPuzzle(string letter)
    {
        if(PieceCollection == null) return;
        if (PiecePrefab != null)
        {
            Destroy(PiecePrefab.gameObject);
            _pieceParent = null;
            _aText.text = "";
            if (_animal != null)
                Destroy(_animal);
        }
        foreach(var obj in PieceCollection.transform)
        {
            var go = ((Transform)obj).gameObject;
            if (go.name.ToUpper() == letter.ToUpper())
            {
                PiecePrefab = Instantiate(go);
                PiecePrefab.transform.parent = transform;                           
                _pieceParent = PiecePrefab.GetComponent<PieceParent>();
                _pieceAnim = PiecePrefab.GetComponent<Animation>();
            }
        }     
           
        StartPuzzle();
    }

    public override bool CheckForWin()
    {
        foreach (var piece in _associatedPieces)
        {
            if (!piece.Drag.IsPlaced) return false;           
        }

        return true;
    }
  
    public void StartNextPuzzle()
    {
        if (_pieceParent == null)
            throw new Exception("No piece parent assigned to puzzle piece parent container");
        //var node = NodeManager.Instance.PullMapLetter(_pieceParent.AlphaName);
        
        
        //TODO: Replace with back to map logic
        //SelectPuzzle(_pieceParent.NextAlpha);
    }

    public void StartNextPuzzle(string letter)
    {
        SelectPuzzle(letter);
    }

    private void StartPuzzle()
    {        
        _associatedPieces = PiecePrefab.GetComponentsInChildren<PuzzlePiece>();        
        SoundManager.PlaySound(_pieceParent.AudioDescription, 2);
        Invoke("StartScramble", 1.0f);
        _waitForPuzzle = false;
    }

    //Invoked Method
    private void StartScramble()
    {        
        var randPositions = RandomizePositions();
        if (randPositions.Length < _associatedPieces.Length)
            throw new IndexOutOfRangeException("Pieces outnumber split position points");
        SoundManager.PlaySound(SoundManager.Instance.ScatterNoise, 2);
        for (int i = 0; i < _associatedPieces.Length; i++)
            _associatedPieces[i].SplitPuzzle(SplitTimerSec, randPositions[i]);
    }

    private Vector3[] RandomizePositions()
    {
        var r = new System.Random();
        return PresetSplitPositions.OrderBy(x => r.Next()).ToArray();        
    }  

    /*private GameObject CreateAnimal()
    {
        var animal = Instantiate(_pieceParent.AssociatedAnimal);
        animal.transform.parent = transform;
        animal.GetComponent<Renderer>().sortingOrder = 5;
        return animal;
    }*/
    
    private IEnumerator AddLetterAssociation(float timerDelay, float moveToMapSec)
    {
        Particles.DestructDelay = 10.0f;
        foreach (var effect in PresetSplitPositions)
        {
            Particles.FireRandomParticle(PuzzleWinEffects, effect);
        }

        Particles.FireParticle(PuzzleWinEffects[0], _pieceParent.transform.position);
        _pieceAnim.Play();
        //_animal = CreateAnimal();
        /*Animal Animations go here*/
        yield return new WaitForSeconds(timerDelay);
        //_aText.text = _pieceParent.AssociatedWordValue;        
        StartCoroutine(MoveToNextMapLetter(moveToMapSec));                
    }
    
    private IEnumerator MoveToNextMapLetter(float startDelaySec)
    {
        yield return new WaitForSeconds(startDelaySec);        
        /*if (_pieceParent.NextAlpha == "END" && GameState.LoadBoard == BoardType.Adventure)
        {
            SceneSelector.Instance.MoveToVictoryBoard();
            yield break;                
        }*/
        //SceneSelector.Instance.MoveToMapBoard(_pieceParent.AlphaName);
        yield return new WaitForSeconds(1f);
        /*if (GameState.LoadBoard != BoardType.FreeMap)
        {            
            SceneSelector.Instance.MoveToMapBoard(_pieceParent.NextAlpha, true);
        } */       
        yield return null;
    }      
     
}
