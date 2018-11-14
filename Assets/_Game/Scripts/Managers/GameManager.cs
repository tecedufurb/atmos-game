using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Planting planting;
    public PlantsSelectorController selectedPlants;
    public WavesManager waveManager;
    public Text qntOfSeedsText;
    public TimerBetweenWaves timerBetweenWaves;
    public Score score;
    public Helper helper;
    public GameObject[] objectsToDeactivate;

    public static bool isGamePaused;

    private Timer timer;

    [Header("Mission configuration")]
    public string initialQntOfPlants;

    public int expectedMissionDurationInSeconds;
    public int seedsReplenishedAfterWave;
    public int timeBetweenWaves;

    private void OnEnable() {
        InputManager.OnPlantTap += plant;
        WavesManager.OnWaveEnded += waveEnded;
        TimerBetweenWaves.OnTimeIsUp += startWave;
        RiparianForestValidation.OnRiparianForestIsComplete += displayScore;
        SeedController.OnSeedCollected += increaseQuantityOfSeeds;
    }

    private void OnDisable() {
        InputManager.OnPlantTap -= plant;
        WavesManager.OnWaveEnded -= waveEnded;
        TimerBetweenWaves.OnTimeIsUp -= startWave;
        RiparianForestValidation.OnRiparianForestIsComplete -= displayScore;
        SeedController.OnSeedCollected -= increaseQuantityOfSeeds;
    }

    void Start() {
        qntOfSeedsText.text = initialQntOfPlants;
        timer = GetComponent<Timer>();
        timer.startTimer();
        timerBetweenWaves.startTimer(timeBetweenWaves);
    }

    private void waveEnded() {
        Invoke("activatePlantingMode", 2); // wait 2 second to player don't plant in wrong place accidentally
        qntOfSeedsText.text = (int.Parse(qntOfSeedsText.text) + seedsReplenishedAfterWave).ToString();
    }

    public void activatePlantingMode(bool option) {
        if (option) {
            for (int i = 0; i < objectsToDeactivate.Length; i++) {
                objectsToDeactivate[i].SetActive(true);
            }

            allowPlanting(true);
            timerBetweenWaves.startTimer(timeBetweenWaves);
        } else {
            for (int i = 0; i < objectsToDeactivate.Length; i++) {
                objectsToDeactivate[i].SetActive(false);
            }

            allowPlanting(false);
        }
    }

    private void activatePlantingMode() { // used to add delay before enable planting mode
        activatePlantingMode(true);
    }

    public void startWave() {
        waveManager.startNextWave();
        activatePlantingMode(false);
    }

    public void allowPlanting(bool state) {
        planting.enabled = state;
    }

    private void plant(Vector2 screenPosition) {
        if (planting.enabled) {
            if (int.Parse(qntOfSeedsText.text) <= 0)
                print("out of seeds"); // TODO show alert of out of seeds

            int[] plantsSelected = selectedPlants.getSelectedPlants();
            if (plantsSelected != null) {
                planting.plant(plantsSelected, screenPosition, int.Parse(qntOfSeedsText.text));
                qntOfSeedsText.text =
                    (int.Parse(qntOfSeedsText.text) - planting.getQntOfPlantsInstantiatedLastTap()).ToString();
            }
        }
    }

    private void displayScore() {
        RiparianForestValidation.OnRiparianForestIsComplete -=
            displayScore; // when display score can unsubscribe, so that displayScore isn't called more than once
        timer.pauseTimer();
        score.calculateScore(expectedMissionDurationInSeconds);
        score.showScore();
    }

    public void pauseGame() {
        isGamePaused = true;
        Time.timeScale = 0;
    }

    public void resumeGame() {
        isGamePaused = false;
        Time.timeScale = 1;
    }

    private void increaseQuantityOfSeeds() {
        qntOfSeedsText.text = (int.Parse(qntOfSeedsText.text) + 1).ToString();
    }

}

enum GameState {

    Protect,
    Plant,
    DisplayScore

}