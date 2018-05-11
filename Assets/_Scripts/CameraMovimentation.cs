using UnityEngine;
using UnityEngine.Events;
using Lean.Touch;

public class CameraMovimentation : MonoBehaviour{
     private Rigidbody rb;
     private Vector3 direction;
     void Awake(){
        rb = GetComponent<Rigidbody>();
        Debug.Log(rb.name);
    }

    private void OnEnable(){
        InputManager.OnSwipe += moveCamera;
    }

    private void OnDisable(){
        InputManager.OnSwipe -= moveCamera;
    }
    private void moveCamera(LeanFinger finger, Vector2 delta){
        direction.Set(delta.x,0,delta.y);
        rb.AddForce(direction*20);
    }

}
