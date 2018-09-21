using UnityEngine;
using AlphaPuzzle.State;

[RequireComponent(typeof(CircleCollider2D))]
public class Node : MonoBehaviour
{
    public string Name;        
        
    private SceneSelector _scene;    
    
    void Start()
    {        
        _scene = SceneSelector.Instance;
    }
    
    void Update()
    {
        /*if (IsCompleted && _bgRend.sprite != Completed)
        {
            //Add the star wrapper
            _bgRend.sprite = Completed;            
        }

        if (CanSelect && _bgRend.sprite != Selected && !IsCompleted)
        {
            //Kick Off Particle
            _bgRend.sprite = Selected;            
            Particles.FireParticle(NodeManager.Instance.SelectedEffect, transform.position);
            if(CanSelect && Name != "A")
                SoundManager.PlaySound(NodeManager.Instance.LetterComplete);
        } */          
    }
    
    void OnMouseDown()
    {
        if (!GameState.DragEnabled) return;
        if (_scene.HandAnimation != null)
            _scene.HandAnimation.SetActive(false);
        _scene.MoveToPuzzleBoard();
        PieceManager.Instance.StartNextPuzzle(Name, this);
    }    

}
