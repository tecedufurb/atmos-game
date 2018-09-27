using Lean.Touch;
using UnityEngine;

public class CameraMovimentation : MonoBehaviour{
    private Rigidbody rigidbody;
    private Camera camera;
    private Vector3 direction;
    private float CurrentZoom;

    [Tooltip("Strenght of the swipe"),SerializeField]
    private int pushForce;

    [Header("Zoom Settings"),Tooltip("Ignore fingers with StartedOverGui?")]
    public bool IgnoreGuiFingers = true;

    [Tooltip("The minimum FOV we want to zoom to"),SerializeField]
    private float MinZoom;

    [Tooltip("The maximum FOV we want to zoom to"),SerializeField]
    private float MaxZoom;

     void Awake(){
        rigidbody = GetComponent<Rigidbody>();
        camera = GetComponent<Camera>();
        CurrentZoom = (camera.orthographic==true) ? camera.orthographicSize : camera.fieldOfView;
    }
    
    void Update(){
        
        // Modify the zoom value
        CurrentZoom *= LeanGesture.GetPinchRatio(LeanTouch.GetFingers(IgnoreGuiFingers));
    
        CurrentZoom = Mathf.Clamp(CurrentZoom, MinZoom, MaxZoom);

        // Set the new zoom
        zoomCamera(CurrentZoom);
    }

    private void OnEnable(){
        InputManager.OnSwipe += moveCamera;
    }

    private void OnDisable(){
        InputManager.OnSwipe -= moveCamera;
    }

    private void moveCamera(LeanFinger finger, Vector2 delta){
        direction.Set(-delta.x,0,-delta.y);
        rigidbody.AddForce(direction*pushForce);
    }

    private void zoomCamera(float current){
        if (camera != null){
            if (camera.orthographic == true){
                camera.orthographicSize = current;
            }
            else{
                camera.fieldOfView = current;
            }
        }
    }
}
