using UnityEngine;
using UnityEngine.Events;
using Lean.Touch;

public class CameraMovimentation : MonoBehaviour{
    private Rigidbody rigidbody;
    private Camera camera;
    private Vector3 direction;
    
    [Tooltip("Strenght of the swipe"),SerializeField]
    private int pushForce;

    [Header("Zoom Settings"),Tooltip("Ignore fingers with StartedOverGui?")]
    public bool IgnoreGuiFingers = true;
    
    [Tooltip("The current FOV/Size"),SerializeField]
    private float CurrentZoom;

    [Tooltip("The minimum FOV we want to zoom to"),SerializeField]
    private float ZoomMin;

    [Tooltip("The maximum FOV we want to zoom to"),SerializeField]
    private float ZoomMax;

     void Awake(){
        rigidbody = GetComponent<Rigidbody>();
        camera = GetComponent<Camera>();
        CurrentZoom = (camera.orthographic==true) ? camera.orthographicSize : camera.fieldOfView;
        ZoomMin = 40;
        ZoomMax= 100;
        pushForce = 30;
    }
    void Update(){
        
        // Modify the zoom value
        CurrentZoom *= LeanGesture.GetPinchRatio(LeanTouch.GetFingers(IgnoreGuiFingers));
    
        CurrentZoom = Mathf.Clamp(CurrentZoom, ZoomMin, ZoomMax);

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
