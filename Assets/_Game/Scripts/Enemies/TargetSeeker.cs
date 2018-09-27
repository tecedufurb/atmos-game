using UnityEngine;

public class TargetSeeker : MonoBehaviour {

    private static TargetSeeker targetSeekerInstance;

    public Transform enemyEndPointLeft;
    public Transform enemyEndPointRight;
    public PlantsManager plantsManager;

    private void Awake() {
        if (targetSeekerInstance == null)
            targetSeekerInstance = this;
        else if (targetSeekerInstance != this)
            Destroy(gameObject);
    }

    void OnEnable() {
        TrollController.OnGetNextTarget += getNewTarget;
    }

    void OnDisable() {
        TrollController.OnGetNextTarget -= getNewTarget;
    }

    public Transform getNewTarget(bool isOnLeftSideOfRiver) {
        GameObject target = null;
        if (isOnLeftSideOfRiver) {
            target = plantsManager.getPlantOfLeftSide();
            if (target == null) // if there is no plants, send enemy do despawn 
                return enemyEndPointLeft;
            else
                return target.transform;
        }
        else {
            target = plantsManager.getPlantOfRightSide();

            if (target == null) // if there is no plants, send enemy do despawn 
                return enemyEndPointRight;
            else
                return target.transform;
        }
    }

    public Transform getEndPoint(bool isOnLeftSide) {
        if (isOnLeftSide)
            return enemyEndPointLeft;
        return enemyEndPointRight;
    }

    public static TargetSeeker getInstance() {
        return targetSeekerInstance;
    }

}