using AlphaPuzzle.State;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapButtons : MonoBehaviour
{
    void Update()
    {
        GameState.IsOverGameObject = EventSystem.current.IsPointerOverGameObject();        
    }
}
