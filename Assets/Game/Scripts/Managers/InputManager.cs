using Lean.Touch;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour{

    [Tooltip("Ignore fingers with StartedOverGui?")]
    public bool IgnoreGuiFingers = true;

    #region events

    public delegate void SwipeEvent(LeanFinger finger, Vector2 delta);
    public static event SwipeEvent OnSwiped;

    public delegate void ZoomEvent(float value);
    public static event ZoomEvent OnZoom;

    #endregion

    private void OnEnable(){
        // Hook events
        LeanTouch.OnFingerSwipe += FingerSwipe;
    }

    private void OnDisable(){
        // Unhook events
        LeanTouch.OnFingerSwipe -= FingerSwipe;
    }

    private void FingerSwipe(LeanFinger finger){
        // Ignore this finger?
        if (IgnoreGuiFingers == true && finger.StartedOverGui == true)
            return;
        else
            if (OnSwiped != null)
            OnSwiped(finger, finger.SwipeScreenDelta);
    }

    public void OnCameraZoom(UnityEngine.UI.Scrollbar scrollbar){
        if (OnZoom != null)
            OnZoom(scrollbar.value);
    }

}