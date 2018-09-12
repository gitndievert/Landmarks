using AlphaPuzzle.State;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        GameState.DragEnabled = false;
    }

    protected virtual void OnDisable()
    {
        GameState.DragEnabled = true;
    }
}
