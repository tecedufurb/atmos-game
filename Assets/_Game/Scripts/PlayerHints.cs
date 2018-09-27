using UnityEngine;

public class PlayerHints : MonoBehaviour {

	private void OnEnable(){
		PlantUiController.OnSelectMorePlantsThanAllowed += maximumCapacityOfSelectedPlantsReached;
	}

	private void OnDisable(){

		PlantUiController.OnSelectMorePlantsThanAllowed -= maximumCapacityOfSelectedPlantsReached;
	}


	private void maximumCapacityOfSelectedPlantsReached() {
		print("You can only select up to 5 plants");
	}
}
