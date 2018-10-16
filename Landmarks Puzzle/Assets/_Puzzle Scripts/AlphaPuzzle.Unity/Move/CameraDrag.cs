using AlphaPuzzle.State;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraDrag : MonoBehaviour
{
    //Values should be set on Prefab
    public float XMinBounds = 70.05f;
    public float XMaxBounds = 135.02f;
    public float YMinBounds = 22.80f;
    public float YMaxBounds = -83.20f;
    [Space(10)]
    [Range(0, 25f)]
    public float ButtonSpeed = 10f;    

    private Vector3 _resetCamera;
    private Vector3 _origin;
    private Vector3 _difference;
    private bool _drag;
    private Camera _camera;
    private bool _buttonDrag;   

    void Start()
    {
        _camera = Camera.main;
        _buttonDrag = false;
        Input.multiTouchEnabled = false;
    }

    void Update()
    {        
        if (SceneSelector.Instance.IsCameraLocked || !GameState.DragEnabled) return;        
        if (Input.GetMouseButton(0))
        {
            //StopAllCoroutines();            
            _difference = (_camera.ScreenToWorldPoint(Input.mousePosition)) - _camera.transform.position;
            if (!_drag)
            {
                _drag = true;
                _origin = _camera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            _drag = false;
        }

        if (_drag)
        {
            _camera.transform.position = _origin - _difference;            
            Bounds();           
        }
        
    }  

    public void OnClickDown_MoveCamera(string direction)
    {        
        GameState.DragEnabled = false;        
        switch (direction)
        {
            case "top":
                transform.Translate(0, ButtonSpeed, 0);
                break;
            case "bottom":
                transform.Translate(0, -ButtonSpeed, 0);
                break;
            case "left":
                transform.Translate(-ButtonSpeed, 0 , 0);
                break;
            case "right":
                transform.Translate(ButtonSpeed, 0, 0);
                break;
        }

        Bounds();        
    }
        
    public void OnClick_ReleaseCamera()
    {
        GameState.DragEnabled = true;
    }    
    
    private void Bounds()
    {
        var pos = _camera.transform.position;
        if (pos.x < XMinBounds)
            _camera.transform.position = new Vector3(XMinBounds, _camera.transform.position.y, -10f);
        if (pos.x > XMaxBounds)
            _camera.transform.position = new Vector3(XMaxBounds, _camera.transform.position.y, -10f);
        if (pos.y > YMinBounds)
            _camera.transform.position = new Vector3(_camera.transform.position.x, YMinBounds, -10f);
        if (pos.y < YMaxBounds)
            _camera.transform.position = new Vector3(_camera.transform.position.x, YMaxBounds, -10f);
    }
   
}
