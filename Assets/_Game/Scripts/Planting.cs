using UnityEngine;

public class Planting : MonoBehaviour {

    public Transform fatherOfPlants;
    public GameObject[] plants = new GameObject[10];

    private readonly int quantityOfPlantsByTap = 5;
    private readonly int raycastDistance = 500;
    private readonly int radiusOfAreaToPlant = 20;
    private readonly int maxPlantingAttempts = 10;
    private int qntOfPlantsCreated = 0; // used to name plant
    private int qntOfPlantsInstantiated = 0; // changes at every tap 

    private readonly string[] haveToCollide = {"Terrain"}; //add ConservationArea here
    private readonly string canCollide = "Ignore";
    private readonly string cameraTag = "MainCamera";
    private readonly string riparianForestAreaLeftSide = "RiparianForestAreaLeftSide";
    private readonly string riparianForestAreaRightSide = "RiparianForestAreaRightSide";

    #region PlatingHelpers

    private RaycastHit[] hitsInRaycast;
    private Ray firstRay;
    private int plantingAttempts;
    private int plantsCountHelper;
    private GameObject[] plantsInstantiatedOnLastTap;

    #endregion

    #region Events

    public delegate void PlantsAddedOnLastTap(GameObject[] plants);

    public static event PlantsAddedOnLastTap OnPlantsAddedOnLastTap;

    #endregion


    public void plant(int[] selectedPlants, Vector2 screenPosition, int qntOfSeedsAvailable) {
        //Debug.DrawRay(firstRay.origin, firstRay.direction * 1000, Color.yellow, 40);

        int[] plantsToInstantiate = getPlantsToInstantiate(selectedPlants);
        firstRay = Camera.main.ScreenPointToRay(screenPosition);

        // resets
        plantsInstantiatedOnLastTap = new GameObject[5];
        plantingAttempts = 0;
        plantsCountHelper = 0;
        qntOfPlantsInstantiated = 0;
        while (plantsCountHelper < plantsToInstantiate.Length && plantingAttempts <= maxPlantingAttempts &&
               plantsCountHelper < quantityOfPlantsByTap && qntOfSeedsAvailable > 0) {
            if (plant(plantsToInstantiate[plantsCountHelper], newRandomRay(), plantsCountHelper)) {
                qntOfSeedsAvailable--;
            } else
                plantingAttempts++;
        }

        if (OnPlantsAddedOnLastTap != null)
            OnPlantsAddedOnLastTap(plantsInstantiatedOnLastTap);
    }

    private bool plant(int plant, Ray ray, int countHelper) {
        hitsInRaycast = Physics.RaycastAll(ray, raycastDistance);
        if (validateRay()) {
            plantsInstantiatedOnLastTap[countHelper] = instantiatePlant(plant, getHitWithTerrain().point);
            return true;
        }

        return false;
    }

    private GameObject instantiatePlant(int plantNumber, Vector3 position) {
        GameObject plantObject =
            Instantiate(plants[plantNumber], position, plants[plantNumber].transform.rotation, fatherOfPlants); // instantiate plant
        plantObject.name = "Plant" + qntOfPlantsCreated;
        plantObject.GetComponent<PlantController>().initializePlant(getSideOfRiparianForest(),plantNumber);
        qntOfPlantsCreated++;
        plantsCountHelper++;
        qntOfPlantsInstantiated++;
        return plantObject;
    }

    private char getSideOfRiparianForest() {
        foreach (RaycastHit hit in hitsInRaycast) { // check if have collided riparian area
            if (hit.transform.CompareTag(riparianForestAreaLeftSide))
                return 'l'; // l == left

            if (hit.transform.CompareTag(riparianForestAreaRightSide))
                return 'r'; // d == right
        }

        return '0'; // is not on riparian area
    }

    private bool validateRay() {
        int collisionCounter = 0;
        bool collisionHelper;
        foreach (string tag in haveToCollide) {
            collisionHelper = false;
            foreach (RaycastHit hit in hitsInRaycast) { // validate if have collided with what should collide
                if (hit.transform.CompareTag(tag)) {
                    collisionCounter++;
                    collisionHelper = true;
                }
            }

            if (!collisionHelper)
                return false;
        }

        if (collisionCounter != haveToCollide.Length) // validate if have collided with everything in haveToCollide
            return false;

        foreach (RaycastHit hit in hitsInRaycast) { // validate if have not collided with other objects
            collisionHelper = false;

            foreach (string tag in haveToCollide)
                if (hit.transform.CompareTag(tag))
                    collisionHelper = true;

            if (!hit.transform.CompareTag(canCollide) && !hit.transform.CompareTag(cameraTag) && !collisionHelper &&
                !(hit.transform.CompareTag(riparianForestAreaLeftSide) ||
                  hit.transform.CompareTag(riparianForestAreaRightSide))) {
                return false;
            }
        }

        return true;
    }

    private Ray newRandomRay() { // create a new ray based on the ray of the click and the radius defined
        Ray newRay = new Ray(new Vector3(
                Random.Range(firstRay.origin.x - radiusOfAreaToPlant, firstRay.origin.x + radiusOfAreaToPlant),
                firstRay.origin.y,
                Random.Range(firstRay.origin.z - radiusOfAreaToPlant, firstRay.origin.z + radiusOfAreaToPlant)),
            firstRay.direction);
        //print("new ray "+newRay);
        //Debug.DrawRay(newRay.origin, newRay.direction * 1000, Color.red, 40);
        return newRay;
    }

    private RaycastHit getHitWithTerrain() {
        foreach (RaycastHit hit in hitsInRaycast) {
            if (hit.transform.CompareTag("Terrain")) {
                return hit;
            }
        }

        foreach (RaycastHit hit in hitsInRaycast) {
            print("Hit: " + hit.transform.tag);
        }

        throw new System.Exception("Did not collide with terrain! run to the hills!");
    }

    private int[] getPlantsToInstantiate(int[] selectedPlants) {
        int[] plantsToInstantiate = new int[5];

        if (selectedPlants.Length == 1) {
            for (int i = 0; i < plantsToInstantiate.Length; i++) {
                plantsToInstantiate[i] = selectedPlants[0];
            }
        } else if (selectedPlants.Length <= 2) {
            plantsToInstantiate[0] = selectedPlants[0];
            plantsToInstantiate[1] = selectedPlants[0];
            plantsToInstantiate[2] = selectedPlants[1];
            plantsToInstantiate[3] = selectedPlants[1];
            plantsToInstantiate[4] = selectedPlants[Random.Range(0, 2)]; // random between 0 and 1
        } else if (selectedPlants.Length <= 3) {
            plantsToInstantiate[0] = selectedPlants[0];
            plantsToInstantiate[1] = selectedPlants[1];
            plantsToInstantiate[2] = selectedPlants[2];
            plantsToInstantiate[3] = selectedPlants[Random.Range(0, 3)]; // random between 0 and 2
            plantsToInstantiate[4] = selectedPlants[Random.Range(0, 3)]; // random between 0 and 2
        } else if (selectedPlants.Length <= 4) {
            plantsToInstantiate[0] = selectedPlants[0];
            plantsToInstantiate[1] = selectedPlants[1];
            plantsToInstantiate[2] = selectedPlants[2];
            plantsToInstantiate[3] = selectedPlants[3];
            plantsToInstantiate[4] = selectedPlants[Random.Range(0, 4)]; // random between 0 and 3
        } else if (selectedPlants.Length <= 5) {
            return selectedPlants;
        }

        return plantsToInstantiate;
    }

    public int getQntOfPlantsInstantiatedLastTap() {
        return qntOfPlantsInstantiated;
    }

    private void printHits() {
        foreach (RaycastHit hit in hitsInRaycast)
            print("name: " + hit.collider.name + " tag: " + hit.collider.tag);
    }

}