using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantsDiversityValidation : MonoBehaviour {

    public Image currentQuality;
    public Text debugText;

    private int currentDiversityState;
    private bool haveRiparianForestBeenUpdated;

    private readonly int[] qtdOfEachPlantInDiversity = new int [10]; // store qtd of plants of each type

    private List<Color> diversityQualityColors = new List<Color>(new Color[] {terrible, bad, medium, good, excelent});

    #region Colors

    private static readonly Color terrible = new Color32(168, 0, 0, 255);
    private static readonly Color bad = new Color32(254, 108, 3, 255);
    private static readonly Color medium = new Color32(214, 152, 9, 255);
    private static readonly Color good = new Color32(230, 254, 0, 255);
    private static readonly Color excelent = new Color32(0, 168, 36, 255);

    #endregion

    private void OnEnable() {
        PlantsManager.OnPlantAddedToRiparianForest += updatePlantAdded;
        PlantsManager.OnPlantRemovedOfRiparianForest += updatePlantRemoved;
    }

    private void OnDisable() {
        PlantsManager.OnPlantAddedToRiparianForest -= updatePlantAdded;
        PlantsManager.OnPlantRemovedOfRiparianForest -= updatePlantRemoved;
    }

    private void Start() {
        currentQuality.color = diversityQualityColors[0]; //start at lowest score
        StartCoroutine(updateDiversity());
    }

    private IEnumerator updateDiversity() { // update diversity after 0.3 seconds, if a change have been made to riparian area
        while (true) {
            if (haveRiparianForestBeenUpdated) {
                updateDisplay(calculateTotalDiversity());
                haveRiparianForestBeenUpdated = false; // reset
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    private float calculateTotalDiversity() {
        float totalDiversityValue = 0f;

        for (int i = 0; i < qtdOfEachPlantInDiversity.Length; i++) {
            totalDiversityValue += calculatePlantDiversity(qtdOfEachPlantInDiversity[i]);
        }

        currentDiversityState = (int) totalDiversityValue;
        return totalDiversityValue;
    }

    private float calculatePlantDiversity(int qtdOfPlant) {
        if (qtdOfPlant >= 25)
            return 0f;
        else if (qtdOfPlant >= 17)
            return 3f;
        else if (qtdOfPlant >= 12)
            return 5f;
        else if (qtdOfPlant >= 8) // perfect score
            return 10f;
        else if (qtdOfPlant >= 5)
            return 6f;
        else if (qtdOfPlant >= 3)
            return 3f;
        else
            return 0f;
    }

    private void updateDisplay(float scorePercentage) {
        if (scorePercentage > 80)
            currentQuality.color = diversityQualityColors[4];
        else if (scorePercentage > 60)
            currentQuality.color = diversityQualityColors[3];
        else if (scorePercentage > 40)
            currentQuality.color = diversityQualityColors[2];
        else if (scorePercentage > 20)
            currentQuality.color = diversityQualityColors[1];
        else
            currentQuality.color = diversityQualityColors[0];

        debugText.text = ((int) scorePercentage).ToString();
    }

    private void updatePlantAdded(GameObject plant) {
        qtdOfEachPlantInDiversity[plant.GetComponent<PlantController>().getPlantNumber()]++;
        haveRiparianForestBeenUpdated = true;
    }

    private void updatePlantRemoved(GameObject plant) {
        qtdOfEachPlantInDiversity[plant.GetComponent<PlantController>().getPlantNumber()]--;
        haveRiparianForestBeenUpdated = true;
    }

    public int getDiversity() {
        return currentDiversityState;
    }

}