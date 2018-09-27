using UnityEngine;
using UnityEngine.Events;

namespace Lean.Touch{
	public class LeanFingerSwipe : MonoBehaviour{

		[Tooltip("Ignore fingers with StartedOverGui?")]
		public bool IgnoreGuiFingers = true;

        public delegate void SwipeEvent(LeanFinger finger, Vector2 delta);
        public static event SwipeEvent OnSwipe;

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
                if (OnSwipe != null)
                	OnSwipe(finger, finger.SwipeScreenDelta);
		}
	}
}