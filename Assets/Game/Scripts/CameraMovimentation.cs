using UnityEngine;
using UnityEngine.Events;
using Lean.Touch;

public class CameraMovimentation : MonoBehaviour
{

    [Tooltip("Pull force of the swipe")]
    public int pullForce = 20;
     private Rigidbody rb;
     private Vector3 direction;
     void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable(){
        InputManager.OnSwiped += moveCamera;
    }

    private void OnDisable(){
        InputManager.OnSwiped -= moveCamera;
    }
    private void moveCamera(LeanFinger finger, Vector2 delta)
    {
        direction.Set(delta.x,0,delta.y);
        rb.AddForce(-direction*pullForce);
    }

}
