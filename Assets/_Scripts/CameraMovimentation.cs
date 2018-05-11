using UnityEngine;
using UnityEngine.Events;
using Lean.Touch;

public class CameraMovimentation : MonoBehaviour{
     private Rigidbody rb;
     private Vector3 direction;
    [Tooltip("Strenght of the swipe"),SerializeField]
     private int pushForce;

     void Awake(){
         pushForce = 50;
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable(){
        InputManager.OnSwipe += moveCamera;
    }

    private void OnDisable(){
        InputManager.OnSwipe -= moveCamera;
    }
    private void moveCamera(LeanFinger finger, Vector2 delta){
        direction.Set(-delta.x,0,-delta.y);
        rb.AddForce(direction*pushForce);
    }

}
