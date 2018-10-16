using UnityEngine;
using AlphaPuzzle.State;

public class Node : MapObject
{       
    private SceneSelector _scene;    
    
    void Start()
    {        
        _scene = SceneSelector.Instance;
    }

    protected override void OnMouseDown()
    {
        if (GameState.IsOverGameObject) return;
        if (!GameState.DragEnabled) return;
        if (_scene.HandAnimation != null)
            _scene.HandAnimation.SetActive(false);        
        _scene.MoveToPuzzleBoard();
        PieceManager.Instance.StartNextPuzzle(Name, this);
    }
   
}
