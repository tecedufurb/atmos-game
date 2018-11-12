using Lean.Touch;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static int raycastDistance = 500;

    #region Events

    public delegate void SwipeEvent(LeanFinger finger, Vector2 delta);

    public static event SwipeEvent OnSwipe;

    public delegate void PlantEvent(Vector2 screenPosition);

    public static event PlantEvent OnPlantTap;

    #endregion

    private void OnEnable() {
        // Hook events
        LeanFingerSwipe.OnSwipe += FingerSwipe;
        LeanFingerTap.OnTap += FingerTap;
    }

    private void OnDisable() {
        // Unhook events
        LeanFingerSwipe.OnSwipe -= FingerSwipe;
        LeanFingerTap.OnTap -= FingerTap;
    }

    private void FingerSwipe(LeanFinger finger, Vector2 delta) {
        if (OnSwipe != null)
            OnSwipe(finger, delta);
    }

    private void FingerTap(LeanFinger finger) {
        if(canPlant(shotRay(createRay(finger.ScreenPosition))))
            if (OnPlantTap != null)
                OnPlantTap(finger.ScreenPosition);
    }


    private Ray createRay(Vector2 tap) {
        return Camera.main.ScreenPointToRay(tap);
    }

    private RaycastHit[] shotRay(Ray ray) {
        return Physics.RaycastAll(ray, raycastDistance);
    }

    private bool canPlant(RaycastHit[] hits) {
        bool
            allowPlanting =
                true; // only allow planting if haven't done any other action like collecting seed or hitting troll
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].transform.CompareTag("Enemy")) {
                allowPlanting = false;
                hits[i].transform.GetComponent<TrollController>().takeDamage();
            }

            if (hits[i].transform.CompareTag("Seed")) {
                allowPlanting = false;
                hits[i].transform.GetComponent<SeedController>().collectSeed();
            }
        }

        return allowPlanting;
    }

}