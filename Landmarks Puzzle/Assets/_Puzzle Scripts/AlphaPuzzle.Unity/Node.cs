using UnityEngine;
using AlphaPuzzle.State;

public class Node : MonoBehaviour
{
    public string Name;    
    public bool CanSelect = false;
        
    private SceneSelector _scene;

    void Awake()
    {
        Name = transform.name;        
    }

    void Start()
    {
        /*if (Name == "A")        
            CanSelect = true;*/
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
        if (!CanSelect || !GameState.DragEnabled) return;
        if (_scene.HandAnimation != null)
            _scene.HandAnimation.SetActive(false);
        _scene.MoveToPuzzleBoard();
        PieceManager.Instance.StartNextPuzzle(Name);
    }    

}
