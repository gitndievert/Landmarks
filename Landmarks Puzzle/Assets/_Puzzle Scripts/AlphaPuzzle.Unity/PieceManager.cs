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
    public Node SelectedNode;

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
        PiecePrefab = null;
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
            if (CheckForWin() && !_waitForPuzzle)
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
    
    public void SelectPuzzle(string landmark)
    {
        if(PieceCollection == null) return;
        foreach(var obj in PieceCollection.transform)
        {
            var go = ((Transform)obj).gameObject;
            if (go.name.ToUpper() == landmark.ToUpper())
            {
                Destroy(PiecePrefab);
                PiecePrefab = Instantiate(go);
                PiecePrefab.transform.parent = transform;                           
                _pieceParent = PiecePrefab.GetComponent<PieceParent>();
                //_pieceAnim = PiecePrefab.GetComponent<Animation>();
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

    public void StartNextPuzzle(string name, Node node)
    {        
        SelectedNode = node;
        SelectPuzzle(name);
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
        //_pieceAnim.Play();
        //_animal = CreateAnimal();
        /*Animal Animations go here*/
        yield return new WaitForSeconds(timerDelay);
        //_aText.text = _pieceParent.AssociatedWordValue;        
        StartCoroutine(MoveBackToLandmark(moveToMapSec));                
    }
    
    private IEnumerator MoveBackToLandmark(float startDelaySec)
    {
        yield return new WaitForSeconds(startDelaySec);
        var tPos = SelectedNode.transform.position;        
        SceneSelector.Instance.MoveToMapBoard(new Vector3(tPos.x, tPos.y, -10f));
        yield return null;
    }      
     
}
