using UnityEngine;
using System.Collections;

public class ScrollMap : MonoBehaviour
{
    private Vector2 _scrollPos;
    private Touch _touch;

    void OnGUI()
    {
        _scrollPos = GUI.BeginScrollView(new Rect(110, 50, 130, 150), _scrollPos, new Rect(110, 50, 130, 560), GUIStyle.none,GUIStyle.none);

        for(int i = 0; i < 20; i++)
        {
            GUI.Box(new Rect(110, 50 + i * 28, 100, 25), "xxxx_" + i);
            GUI.EndScrollView();
        }
    }

    void Update()
    {
        if (Input.touchCount <= 0) return;
        _touch = Input.touches[0];
        if(_touch.phase == TouchPhase.Moved)
        {
            _scrollPos.y += _touch.deltaPosition.y;
        }
    }
}
