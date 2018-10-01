using System.Collections.Generic;
using UnityEngine;

public class PlantsManager : MonoBehaviour {

    private readonly List<GameObject> plantsLeftSideRiparianForest = new List<GameObject>();
    private readonly List<GameObject> plantsRightSideRiparianForest = new List<GameObject>();
    private readonly List<GameObject> plantsOutsideOfRiparianForest = new List<GameObject>();

    #region Events

    public delegate void
        RiparianForestChanged(int qntOfPlantsInRiparianArea); // called when a plant is added or removed of terrain

    public static event RiparianForestChanged OnRiparianForestChanged;

    #endregion

    private void OnEnable() {
        Planting.OnPlantsAddedOnLastTap += addPlants;
        PlantController.OnPlantIsDead += removePlant;
    }

    private void OnDisable() {
        Planting.OnPlantsAddedOnLastTap -= addPlants;
        PlantController.OnPlantIsDead -= removePlant;
    }

    private void addPlants(GameObject[] plants) {
        for (int i = 0; i < plants.Length; i++) {
            if (plants[i] != null) // check if have plants on array
                addPlant(plants[i]);
        }
    }

    private void addPlant(GameObject plant) {
        PlantController plantController = plant.GetComponent<PlantController>();
        if (plantController.isOutOfRiparianForestArea())
            plantsOutsideOfRiparianForest.Add(plant);
        else if (plantController.isOnLeftSide()) {
            plantsLeftSideRiparianForest.Add(plant);
            notifyChangeOnRiparianForest();
        } else {
            plantsRightSideRiparianForest.Add(plant);
            notifyChangeOnRiparianForest();
        }
    }

    private void removePlant(GameObject plant) {
        PlantController plantController = plant.GetComponent<PlantController>();

        if (plantController.isOutOfRiparianForestArea())
            for (int i = 0; i < plantsOutsideOfRiparianForest.Count; i++) {
                if (plantsOutsideOfRiparianForest[i].name == plant.name) {
                    plantsOutsideOfRiparianForest.RemoveAt(i);
                    return;
                }
            }

        if (plantController.isOnLeftSide())
            for (int i = 0; i < plantsLeftSideRiparianForest.Count; i++) {
                if (plantsLeftSideRiparianForest[i].name == plant.name) {
                    plantsLeftSideRiparianForest.RemoveAt(i);
                    notifyChangeOnRiparianForest();
                    return;
                }
            }

        if (!plantController.isOnLeftSide())
            for (int i = 0; i < plantsRightSideRiparianForest.Count; i++) {
                if (plantsRightSideRiparianForest[i].name == plant.name) {
                    plantsRightSideRiparianForest.RemoveAt(i);
                    notifyChangeOnRiparianForest();
                    return;
                }
            }

        Debug.Log("Plant have not been removed!");
    }

    public GameObject getPlantOfLeftSide() {
        if (plantsLeftSideRiparianForest.Count != 0)
            return plantsLeftSideRiparianForest[Random.Range(0, plantsLeftSideRiparianForest.Count)];
        else if (plantsRightSideRiparianForest.Count != 0) // if there are no more plants in left side, return of right
            return getPlantOfRightSide();
        else
            return null;
    }

    public GameObject getPlantOfRightSide() {
        if (plantsRightSideRiparianForest.Count != 0)
            return plantsRightSideRiparianForest[Random.Range(0, plantsRightSideRiparianForest.Count)];
        else if (plantsLeftSideRiparianForest.Count != 0) // if there are no more plants in right side, return of left
            return getPlantOfLeftSide();
        else
            return null;
    }

    public List<GameObject> getPlantsOfRiparianForest() {
        List<GameObject> result =
            new List<GameObject>(plantsLeftSideRiparianForest.Count + plantsRightSideRiparianForest.Count);
        result.AddRange(plantsLeftSideRiparianForest);
        result.AddRange(plantsRightSideRiparianForest);
        return result;
    }

    private void notifyChangeOnRiparianForest() {
        if (OnRiparianForestChanged != null) // notify that a change has been made in riparian forest
            OnRiparianForestChanged(plantsLeftSideRiparianForest.Count + plantsRightSideRiparianForest.Count);
    }

}