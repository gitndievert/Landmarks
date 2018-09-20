using AlphaPuzzle.State;
using UnityEngine;

public class UIPanel : MonoBehaviour
{   
    /// <summary>
    /// TODO Fix This
    /// </summary>
    protected virtual void OnEnable()
    {
        //GameState.DragEnabled = false;
    }

    protected virtual void OnDisable()
    {
        GameState.DragEnabled = true;
    }
}
