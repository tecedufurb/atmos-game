using System;
using UnityEngine;
using UnityEngine.UI;

public class RiparianForestValidation : MonoBehaviour {

    public Text riparianForestDisplay;

    private int[] boxesLeftSide; // each box have the qtd of plants that are inside of it
    private int[] boxesRightSide; // each box have the qtd of plants that are inside of it

    private int qntOfPlantsOnEachSideOfRiver;
    private float sizeOfEachBox;

    [Header("Riparian forest validation config")]
    public int qntOfPlantsOnEachBox;

    public int qntOfBoxesOnEachSide;

    public float coordinateStartOfBoxes;
    public float coordinateEndOfBoxes;

    #region Events

    public delegate void RiparianForestIsComplete();

    public static event RiparianForestIsComplete OnRiparianForestIsComplete;

    #endregion

    private void OnEnable() {
        PlantsManager.OnPlantAddedToRiparianForest += updatePlantAdded;
        PlantsManager.OnPlantRemovedOfRiparianForest += updatePlantRemoved;
    }

    private void OnDisable() {
        PlantsManager.OnPlantAddedToRiparianForest -= updatePlantAdded;
        PlantsManager.OnPlantRemovedOfRiparianForest -= updatePlantRemoved;
    }

    void Start() {
        createBoxes();
    }

    private void createBoxes() {
        qntOfPlantsOnEachSideOfRiver = qntOfPlantsOnEachBox * qntOfBoxesOnEachSide;

        sizeOfEachBox = (Mathf.Abs(coordinateStartOfBoxes) + Mathf.Abs(coordinateEndOfBoxes)) / qntOfBoxesOnEachSide;
        boxesLeftSide = new int[qntOfBoxesOnEachSide];
        boxesRightSide = new int[qntOfBoxesOnEachSide];
    }

    private void updatePlantAdded(GameObject plant) { // called when a plants is added to riparian area

        int box = checkOnWhichBoxPlantIs(plant.transform);

        if (box == int.MaxValue) // if is not on riparian area
            return;

        if (plant.GetComponent<PlantController>().isOnLeftSide())
            boxesLeftSide[box]++;
        else
            boxesRightSide[box]++;
        updateDisplay();
    }

    private void updatePlantRemoved(GameObject plant) { // called when a plants is removed of riparian area

        int box = checkOnWhichBoxPlantIs(plant.transform);

        if (box == int.MaxValue) // if is not on riparian area
            return;

        if (plant.GetComponent<PlantController>().isOnLeftSide())
            boxesLeftSide[box]--;
        else
            boxesRightSide[box]--;
        updateDisplay();
    }

    private void updateDisplay() {
        int totalQtdPlantsLeftSide = 0;
        int totalQtdPlantsRightSide = 0;

        for (int i = 0; i < boxesLeftSide.Length; i++) {
            if (boxesLeftSide[i] > qntOfPlantsOnEachBox) // allow on each box only max qnt allowed  
                totalQtdPlantsLeftSide += qntOfPlantsOnEachBox;
            else
                totalQtdPlantsLeftSide += boxesLeftSide[i];
        }

        for (int i = 0; i < boxesRightSide.Length; i++) {
            if (boxesRightSide[i] > qntOfPlantsOnEachBox) // allow on each box only max qnt allowed  
                totalQtdPlantsRightSide += qntOfPlantsOnEachBox;
            else
                totalQtdPlantsRightSide += boxesRightSide[i];
        }

        float percentageLeftSide = (float) (totalQtdPlantsLeftSide * 50) / qntOfPlantsOnEachSideOfRiver;
        float percentageRightSide = (float) (totalQtdPlantsRightSide * 50) / qntOfPlantsOnEachSideOfRiver;

        int percentageOfBothSides = (int) (percentageLeftSide + percentageRightSide);

        riparianForestDisplay.text = percentageOfBothSides.ToString("00") + "%"; // update display

        if (percentageOfBothSides >= 100) {
            riparianForestIsReady();
        }
    }

    private int checkOnWhichBoxPlantIs(Transform plant) {
        if (!checkIfPlantIsOnRiparianForestArea(plant)) // if is not on riparian area use a value as escape
            return Int32.MaxValue;

        float plantZ_Position = plant.position.z;

        int box = 0;
        float helper = coordinateStartOfBoxes;

        while (plantZ_Position > helper) {
            helper += sizeOfEachBox;
            box++;
        }

        box--;
        return box;
    }

    private bool checkIfPlantIsOnRiparianForestArea(Transform plant) {
        if (plant.position.z > coordinateEndOfBoxes || plant.position.z < coordinateStartOfBoxes)
            return false;
        else
            return true;
    }

    private void riparianForestIsReady() {
        if (OnRiparianForestIsComplete != null)
            OnRiparianForestIsComplete();
    }

}