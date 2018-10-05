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
            
    void OnMouseDown()
    {
        if (!GameState.DragEnabled) return;
        if (_scene.HandAnimation != null)
            _scene.HandAnimation.SetActive(false);
        _scene.MoveToPuzzleBoard();
        PieceManager.Instance.StartNextPuzzle(Name, this);
    }    

}
