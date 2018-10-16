using AlphaPuzzle.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPiece : MapObject
{
    public AudioClip OnTapSound;
    public bool AnimateOnTap = false;

    private Animation _anim;

    private void Awake()
    {
        _anim = transform.GetComponent<Animation>();        
    }


    protected override void OnMouseDown()
    {
        if (GameState.IsOverGameObject) return;
        if (!GameState.DragEnabled) return;
        if (OnTapSound != null)
            SoundManager.PlaySound(OnTapSound);
        if(AnimateOnTap && !_anim.isPlaying)
        {
            //_anim.Play()
        }

    }
}
