using AlphaPuzzle.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class MapObject : MonoBehaviour
{
    public string Name;

    protected virtual void Update()
    {
        
    }

    protected abstract void OnMouseDown();
}
