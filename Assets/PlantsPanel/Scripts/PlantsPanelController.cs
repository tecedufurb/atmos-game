using UnityEngine;
using UnityEngine.UI;

public class PlantsPanelController : MonoBehaviour {

	[SerializeField] private Image middlePlantButton;
	[SerializeField] private Image rightPlantButton;
	[SerializeField] private Image leftPlantButton;
	[SerializeField] private Text plantNameText;
	[SerializeField] private Text plantDescriptionText;
	
	[SerializeField] private PlantPanelModel[] plantsModels;

	private int plantIndex;
	private int imageIndex;

	private void Start() {
		UpdatePlantsPanel();
	}

	public void NextPlant() {
		if (plantIndex < plantsModels.Length - 1) {
			plantIndex++;
			imageIndex = 0;
		} else {
			plantIndex = 0;
		}
		UpdatePlantsPanel();
	}

	public void PreviousPlant() {
		if (plantIndex > 0) {
			plantIndex--;
			imageIndex = 0;
		} else {
			plantIndex = plantsModels.Length - 1;
		}
		UpdatePlantsPanel();
	}

	public void ChangeFocusToRight() {
		if (imageIndex < plantsModels[plantIndex].images.Length - 1) {
			imageIndex++;
		} else {
			imageIndex = 0;
		}
		UpdatePlantsPanel();
	}
	
	public void ChangeFocusToLeft() {
		if (imageIndex > 0) {
			imageIndex--;
		} else {
			imageIndex = plantsModels[plantIndex].images.Length-1;
		}
		UpdatePlantsPanel();
	}

	private void UpdatePlantsPanel() {
		middlePlantButton.sprite = plantsModels[plantIndex].images[imageIndex];

		if (imageIndex == 0) {
			leftPlantButton.sprite = plantsModels[plantIndex].images[plantsModels[plantIndex].images.Length - 1];
		} else {
			leftPlantButton.sprite = plantsModels[plantIndex].images[imageIndex - 1];
		}

		if (imageIndex == plantsModels[plantIndex].images.Length - 1) {
			rightPlantButton.sprite = plantsModels[plantIndex].images[0];
		} else {
			rightPlantButton.sprite = plantsModels[plantIndex].images[imageIndex + 1];
		}

		plantNameText.text = plantsModels[plantIndex].name;
		plantDescriptionText.text = plantsModels[plantIndex].description;
	}
}