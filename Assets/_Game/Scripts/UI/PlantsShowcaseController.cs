using System;
using UnityEngine;
using UnityEngine.UI;

public class PlantsShowcaseController : MonoBehaviour {

    public Image currentPlantImage;
    public Image firstPlantImage;
    public Image secondPlantImage;
    public Image thirdPlantImage;
    public Image fourthPlantImage;
    public Text plantName;
    public Text plantScientificName;
    public Text plantDescription;

    public PlantsShowcaseModel[] plantsModels;

    private int currentPlant;

    [Serializable]
    public class PlantsShowcaseModel {

        public string name;
        public string scientificName;
        public string description;
        public Sprite[] images;

    }

    public void nextPlant() {
        if (currentPlant >= plantsModels.Length) // is is on last plant, go back to first
            currentPlant = 0;
        else
            currentPlant++;
        updatePlantSelected();
    }

    public void previousPlant() {
        if (currentPlant == 0) // is is on first plant, go to last
            currentPlant = plantsModels.Length;
        else
            currentPlant--;
        updatePlantSelected();
    }

    public void changeCurrentImage(int image) {
        currentPlantImage.sprite = plantsModels[currentPlant].images[image];
    }

    private void updatePlantSelected() {
        currentPlantImage.sprite = plantsModels[currentPlant].images[0];
        firstPlantImage.sprite = plantsModels[currentPlant].images[0];
        secondPlantImage.sprite = plantsModels[currentPlant].images[1];
        thirdPlantImage.sprite = plantsModels[currentPlant].images[2];
        fourthPlantImage.sprite = plantsModels[currentPlant].images[3];
        plantName.text = plantsModels[currentPlant].name;
        plantScientificName.text = plantsModels[currentPlant].scientificName;
        plantDescription.text = plantsModels[currentPlant].description;
    }

    void Start() {
        updatePlantSelected();
    }

}