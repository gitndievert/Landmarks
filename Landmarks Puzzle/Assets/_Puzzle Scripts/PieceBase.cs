using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class PieceBase : MonoBehaviour
{
    public const float TouchPadding = 2f;

    public Sprite PieceSprite
    {
        get { return _rend.sprite; }
        set { _rend.sprite = value; }
    }

    public int SetSpriteLayer
    {
        set { _rend.sortingOrder = value; }
    }
    
    private SpriteRenderer _rend;
    private BoxCollider2D _col;
    
    protected virtual void Awake()
    {
        _rend = GetComponent<SpriteRenderer>();
        _col = GetComponent<BoxCollider2D>();
    }

    protected virtual void Start()
    {
        _col.size = new Vector2(_col.size.x + TouchPadding, _col.size.y + TouchPadding);
        SetSpriteLayer = 3;
    }    
    
}
