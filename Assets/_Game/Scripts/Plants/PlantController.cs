using UnityEngine;

public class PlantController : MonoBehaviour {

    private char sideOfRiver;
    private int plantNumber; // used to calculate diversity

    #region Events

    public delegate void PlantIsDead(GameObject plant);

    public static event PlantIsDead OnPlantIsDead;

    #endregion

    public void initializePlant(char sideOfRiver, int plantNumber) { // l==left ; r==right 0==outside of riparian forest
        this.sideOfRiver = sideOfRiver;
        this.plantNumber = plantNumber;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("EnemyAttack")) {
            if (OnPlantIsDead != null)
                OnPlantIsDead(gameObject);
            Destroy(gameObject);
        }
    }

    public bool isOutOfRiparianForestArea() {
        if (sideOfRiver == 'l' || sideOfRiver == 'r')
            return false;
        return true;
    }

    public bool isOnLeftSide() {
        if (sideOfRiver == 'l')
            return true;
        return false;
    }

    public int getPlantNumber() {
        return plantNumber;
    }

}