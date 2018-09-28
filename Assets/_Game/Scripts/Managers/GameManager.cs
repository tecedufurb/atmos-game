using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Timer timer;
    public Planting planting;
    public PlantsSelectorController selectedPlants;
    public WavesManager waveManager;
    public Text qntOfSeedsText;
    public GameObject score;
    public GameObject[] objectsToDeactivate;

    private readonly int qntOfSeedReplenishedAfterWave = 20;

    [Header("Mission configuration")]
    public string initialQntOfPlants;
    public int missionTimeInSeconds;

    private void OnEnable() {
        InputManager.OnTap += processTap;
        WavesManager.OnWaveEnded += waveEnded;
        Timer.OnTimeIsUp += timerEnded;
        RiparianForestCompletation.OnRiparianForestIsReady += displayScore;
    }

    private void OnDisable() {
        InputManager.OnTap -= processTap;
        WavesManager.OnWaveEnded -= waveEnded;
        Timer.OnTimeIsUp -= timerEnded;
        RiparianForestCompletation.OnRiparianForestIsReady += displayScore;
    }

    void Start() {
        qntOfSeedsText.text = initialQntOfPlants;
        timer.GetComponent<Timer>().startTimer(missionTimeInSeconds);
    }

    private void waveEnded() {
        activatePlantingMode(true);
        qntOfSeedsText.text = (int.Parse(qntOfSeedsText.text) + qntOfSeedReplenishedAfterWave).ToString();
    }

    public void activatePlantingMode(bool option) {
        if (option) {
            for (int i = 0; i < objectsToDeactivate.Length; i++) {
                objectsToDeactivate[i].active = true;
            }

            planting.enabled = true;
        }
        else {
            for (int i = 0; i < objectsToDeactivate.Length; i++) {
                objectsToDeactivate[i].active = false;
            }

            planting.enabled = false;
        }
    }

    public void startWave() {
        waveManager.startNextWave();
        activatePlantingMode(false);
    }

    private void processTap(Vector2 screenPosition) {
        if (planting.enabled)
            plant(screenPosition);
        else
            tapAttack(screenPosition);
    }

    private void plant(Vector2 screenPosition) {
        if (int.Parse(qntOfSeedsText.text) <= 0)
            print("out of seeds"); //show alert of out of seeds

        int[] plantsSelected = selectedPlants.getSelectedPlants();
        if (plantsSelected != null) {
            planting.plant(plantsSelected, screenPosition, int.Parse(qntOfSeedsText.text));
            qntOfSeedsText.text = (int.Parse(qntOfSeedsText.text) - planting.getQntOfPlantsInstantiatedLastTap()).ToString();
        }
    }

    private void tapAttack(Vector2 screenPosition) {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, 500f);
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].transform.CompareTag("Enemy"))
                hits[i].transform.GetComponent<TrollController>().hit();
        }
    }

    private void timerEnded() {
        displayScore();
    }

    private void displayScore() {
        timer.pauseTimer();
        score.SetActive(true);
        score.GetComponent<Score>().showScore();
    }

}

enum GameState {

    Protect,
    Plant,
    DisplayScore

}