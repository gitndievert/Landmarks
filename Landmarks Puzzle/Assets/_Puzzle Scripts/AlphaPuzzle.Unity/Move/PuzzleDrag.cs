using AlphaPuzzle.State;
using System;
using UnityEngine;

public class PuzzleDrag : MonoBehaviour
{
    public Vector3 FinalPosition;
    public bool CanSelect;
    public bool IsPlaced;
    public float SnapRange = 0.50f;

    [HideInInspector]
    public PuzzlePiece AttachedPiece;

    private float PosX;
    private float PosY;
    private const float ZDistance = 10.0f;
    private Collider2D _dragCollider;

    void Start()
    {
        _dragCollider = transform.GetComponent<Collider2D>();
    }

    void Update()
    {
        PosX = Input.mousePosition.x;
        PosY = Input.mousePosition.y;      
    }

    void OnMouseDrag()
    {
        if (!CanSelect || IsPlaced || !GameState.DragEnabled) return;
        Drag();
        if ((Math.Abs(transform.position.x - FinalPosition.x) < SnapRange) &&
            (Math.Abs(transform.position.y - FinalPosition.y) < SnapRange))
        {            
            IsPlaced = true;
            Particles.FireParticle(PieceManager.Instance.PiecePopEffect,
                new Vector3(transform.position.x, transform.position.y, -0.5f),SoundManager.Instance.PopNoise);            
            _dragCollider.enabled = false;
            transform.position = FinalPosition;
            AttachedPiece.SetSpriteLayer = 2;            
        }
    }

    private void Drag()
    {
        AttachedPiece.SetSpriteLayer = 4;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(PosX, PosY, ZDistance));        
    }

}
