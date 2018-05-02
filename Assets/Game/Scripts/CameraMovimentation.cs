using UnityEngine;
using UnityEngine.Events;
using Lean.Touch;

public class CameraMovimentation : MonoBehaviour{

    [Tooltip("Pull force of the swipe")]
    public int swipeForce = 20;

    [Header("Fov range")]
    public int minimumFov;
    public int maximimFov;
    private Rigidbody rigidbody;
    private Vector3 direction;
    private float zoomAdjustment;
    private Camera camera;
    void Awake(){
        camera = gameObject.GetComponent<Camera>();
        rigidbody = GetComponent<Rigidbody>();
        minimumFov = 50;
        maximimFov = 100;
        getZoomAdjustment(minimumFov,maximimFov);
    }

    private void OnEnable(){
        InputManager.OnSwiped += moveCamera;
        InputManager.OnZoom += zoomCamera;
    }

    private void OnDisable(){
        InputManager.OnSwiped -= moveCamera;
        InputManager.OnZoom -= zoomCamera;
    }
    private void moveCamera(LeanFinger finger, Vector2 delta){
        direction.Set(delta.x, 0, delta.y);
        rigidbody.AddForce(-direction * swipeForce);
    }

    private void getZoomAdjustment(float min, float max){
         // 1 and 0 because the slider goes from 0 to 1
        zoomAdjustment = (max - min) / (1 - 0);
    }
    private void zoomCamera(float value){
        if (camera.orthographic == true)
            camera.orthographicSize = -value * zoomAdjustment +maximimFov;
        else
            camera.fieldOfView = -value * zoomAdjustment + maximimFov;
    }

}
