using Lean.Touch;
using UnityEngine;

public class CameraMovimentation : MonoBehaviour {

    private Rigidbody cameraRigidbody;
    private Camera thisCamera;
    private Vector3 direction;
    private float CurrentZoom;

    [Tooltip("Strenght of the swipe"), SerializeField]
    private int pushForce;

    [Header("Zoom Settings"), Tooltip("Ignore fingers with StartedOverGui?")]
    public bool IgnoreGuiFingers = true;

    [Tooltip("The minimum FOV we want to zoom to"), SerializeField]
    private float MinZoom;

    [Tooltip("The maximum FOV we want to zoom to"), SerializeField]
    private float MaxZoom;

    void Awake() {
        cameraRigidbody = GetComponent<Rigidbody>();
        thisCamera = GetComponent<Camera>();
        CurrentZoom = (thisCamera.orthographic == true) ? thisCamera.orthographicSize : thisCamera.fieldOfView;
    }

    void Update() {
        // Modify the zoom value
        CurrentZoom *= LeanGesture.GetPinchRatio(LeanTouch.GetFingers(IgnoreGuiFingers));

        CurrentZoom = Mathf.Clamp(CurrentZoom, MinZoom, MaxZoom);

        // Set the new zoom
        zoomCamera(CurrentZoom);
    }

    private void OnEnable() {
        InputManager.OnSwipe += moveCamera;
    }

    private void OnDisable() {
        InputManager.OnSwipe -= moveCamera;
    }

    private void moveCamera(LeanFinger finger, Vector2 delta) {
        direction.Set(-delta.x, 0, -delta.y);
        cameraRigidbody.AddForce(direction * pushForce);
    }

    private void zoomCamera(float current) {
        if (thisCamera != null) {
            if (thisCamera.orthographic == true) {
                thisCamera.orthographicSize = current;
            } else {
                thisCamera.fieldOfView = current;
            }
        }
    }

}