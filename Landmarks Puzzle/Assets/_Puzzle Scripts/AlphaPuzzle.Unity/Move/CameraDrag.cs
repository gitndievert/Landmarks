using AlphaPuzzle.State;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    //Values should be set on Prefab
    public float XMinBounds = 70.05f;
    public float XMaxBounds = 135.02f;
    public float YMinBounds = 22.80f;
    public float YMaxBounds = -83.20f;

    public bool LockCamera
    {
        get { return SceneSelector.Instance.IsCameraLocked; }
        set { SceneSelector.Instance.IsCameraLocked = value; }
    }

    private Vector3 _resetCamera;
    private Vector3 _origin;
    private Vector3 _difference;
    private bool _drag;
    
    void LateUpdate()
    {
        if(LockCamera || !GameState.DragEnabled) return;
        Camera camera = Camera.main;
        if (Input.GetMouseButton(0))
        {
            StopAllCoroutines();
            _difference = (camera.ScreenToWorldPoint(Input.mousePosition)) - camera.transform.position;
            if (!_drag)
            {
                _drag = true;
                _origin = camera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            _drag = false;
        }

        if (_drag)
        {
            camera.transform.position = _origin - _difference;
            var bPos = camera.transform.position;
            if (bPos.x < XMinBounds)
                camera.transform.position = new Vector3(XMinBounds, camera.transform.position.y,-10f);
            if (bPos.x > XMaxBounds)
                camera.transform.position = new Vector3(XMaxBounds, camera.transform.position.y,-10f);
            if (bPos.y > YMinBounds)
                camera.transform.position = new Vector3(camera.transform.position.x, YMinBounds,-10f);
            if (bPos.y < YMaxBounds)
                camera.transform.position = new Vector3(camera.transform.position.x, YMaxBounds,-10f);
        }
        
    }
   
}
