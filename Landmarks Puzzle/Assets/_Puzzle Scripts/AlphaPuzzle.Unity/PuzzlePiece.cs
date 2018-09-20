using UnityEngine;


public class PuzzlePiece : PieceBase
{   
    public Vector3 SplitPosition
    {
        get; private set;
    }
    
    [HideInInspector]
    public PuzzleDrag Drag;
    
    protected override void Awake()
    {
        Drag = gameObject.AddComponent<PuzzleDrag>();
        Drag.AttachedPiece = this;
        base.Awake();
    }

    protected override void Start()
    {
        transform.position = transform.parent.position;
        Drag.FinalPosition = transform.position;
        Drag.IsPlaced = false;
        base.Start();              
    }
   
	void Update ()
    {
        #if UNITY_EDITOR
        Debug.DrawLine(transform.position, Drag.FinalPosition, Color.red);
#endif        
        if (Drag.IsPlaced && !Drag.CanSelect)        
            SetPieceInPlace();                
    }
  
    public void SplitPuzzle(float splitTime, Vector3 splitposition)
    {        
        SplitPosition = splitposition;
        SetFinalPosition();
        StopAllCoroutines();      
        StartCoroutine(transform.Lerp(SplitPosition, splitTime));
        Drag.CanSelect = true;
    }

    public void SetFinalPosition()
    {        
        transform.position = Drag.FinalPosition;
    }   

    private void SetPieceInPlace()
    {
        SetFinalPosition();
        SetSpriteLayer = 2;
    }
        
}
