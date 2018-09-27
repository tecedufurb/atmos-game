using Lean.Touch;
using UnityEngine;

public class InputManager : MonoBehaviour{

    #region Events
    
    public delegate void SwipeEvent(LeanFinger finger, Vector2 delta);
    public static event SwipeEvent OnSwipe;
    
    public delegate void TapEvent(Vector2 screenPosition);
    public static event TapEvent OnTap;

    #endregion

    private void OnEnable(){
        // Hook events
        LeanFingerSwipe.OnSwipe += FingerSwipe;
        LeanFingerTap.OnTap += FingerTap;
    }

    private void OnDisable(){
        // Unhook events
        LeanFingerSwipe.OnSwipe -= FingerSwipe;
        LeanFingerTap.OnTap -= FingerTap;
    }

    private void FingerSwipe(LeanFinger finger,Vector2 delta){
        if (OnSwipe != null)
            OnSwipe(finger,delta);
    }

    private void FingerTap(LeanFinger finger){
        if (OnTap != null)
            OnTap(finger.ScreenPosition);
    }

}
