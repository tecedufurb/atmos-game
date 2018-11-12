using UnityEngine;

public class PlantsSelectorController : MonoBehaviour {
    private bool[] selectedPlants = new bool[10];
    private readonly int maxQntdOfSelectedPlants = 5;
    private int qntdOfSelectedPlants;

    void OnEnable() {
        PlantUiController.OnSelectPlant += updateSelectedPlants;
    }

    void OnDisable() {
        PlantUiController.OnSelectPlant -= updateSelectedPlants;
    }

    public bool updateSelectedPlants(int plant) {
        if (qntdOfSelectedPlants < maxQntdOfSelectedPlants || (selectedPlants[plant] && qntdOfSelectedPlants == maxQntdOfSelectedPlants)) {
            if (selectedPlants[plant] == false)
                qntdOfSelectedPlants++;
            else
                qntdOfSelectedPlants--;
            selectedPlants[plant] = !selectedPlants[plant];
            return true;
        }

        return false;
    }

    public int[] getSelectedPlants() { // return the code of the selected plants
        int selectedPlantsCount = 0;
        for (int i = 0; i < selectedPlants.Length; i++)
            if (selectedPlants[i])
                selectedPlantsCount++;

        if (selectedPlantsCount == 0) // validate if there is at least one plant selected
            return null;

        int resultCount = 0;
        int[] result = new int[selectedPlantsCount];
        for (int i = 0; i < selectedPlants.Length; i++) {
            if (selectedPlants[i]) {
                result[resultCount] = i;
                resultCount++;
            }
        }
        return result;
    }
}