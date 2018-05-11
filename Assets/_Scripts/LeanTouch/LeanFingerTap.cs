using UnityEngine;
using UnityEngine.Events;

namespace Lean.Touch{
		public class LeanFingerTap : MonoBehaviour{

        [Tooltip("If the finger is over the GUI, ignore it?")]
        public bool IgnoreIfOverGui;

        [Tooltip("If the finger started over the GUI, ignore it?")]
        public bool IgnoreIfStartedOverGui;

        [Tooltip("How many times must this finger tap before OnFingerTap gets called? (0 = every time)")]
        public int RequiredTapCount = 0;

        [Tooltip("How many times repeating must this finger tap before OnFingerTap gets called? (e.g. 2 = 2, 4, 6, 8, etc) (0 = every time)")]
        public int RequiredTapInterval;

        public delegate void TapEvent(LeanFinger finger);
        public static event TapEvent OnTap;

        private void OnEnable(){
            // Hook events
            LeanTouch.OnFingerTap += OnFingerTap;
        }

        private void OnDisable(){
            // Unhook events
            LeanTouch.OnFingerTap -= OnFingerTap;
        }

        private void OnFingerTap(LeanFinger finger){
            
            if (IgnoreIfOverGui == true && finger.IsOverGui == true)
                return;

            if (IgnoreIfStartedOverGui == true && finger.StartedOverGui == true)
                return;

            if (RequiredTapCount > 0 && finger.TapCount != RequiredTapCount)
                return;

            if (RequiredTapInterval > 0 && (finger.TapCount % RequiredTapInterval) != 0)
                return;

            // Call event
            if (OnTap != null)
                OnTap(finger);
        }
    }
}