using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Planting planting;
    public PlantsSelectorController selectedPlants;
    public WavesManager waveManager;
    public Text qntOfSeedsText;
    public TimerBetweenWaves timerBetweenWaves;
    public Score score;
    public GameObject[] objectsToDeactivate;

    private Timer timer;

    [Header("Mission configuration")]
    public string initialQntOfPlants;
    public int expectedMissionDurationInSeconds;
    public int seedsReplenishedAfterWave;
    public int timeBetweenWaves;
    
    private void OnEnable() {
        InputManager.OnTap += processTap;
        WavesManager.OnWaveEnded += waveEnded;
        TimerBetweenWaves.OnTimeIsUp += startWave;
        RiparianForestCompletation.OnRiparianForestIsReady += displayScore;
    }

    private void OnDisable() {
        InputManager.OnTap -= processTap;
        WavesManager.OnWaveEnded -= waveEnded;
        TimerBetweenWaves.OnTimeIsUp -= startWave;
        RiparianForestCompletation.OnRiparianForestIsReady -= displayScore;
    }

    void Start() {
        qntOfSeedsText.text = initialQntOfPlants;
        timer = GetComponent<Timer>();
        timer.startTimer();
        timerBetweenWaves.startTimer(timeBetweenWaves);
    }

    private void waveEnded() {
        activatePlantingMode(true);
        qntOfSeedsText.text = (int.Parse(qntOfSeedsText.text) + seedsReplenishedAfterWave).ToString();
    }

    public void activatePlantingMode(bool option) {
        if (option) {
            for (int i = 0; i < objectsToDeactivate.Length; i++) {
                objectsToDeactivate[i].SetActive(true);
            }
            timerBetweenWaves.startTimer(timeBetweenWaves);
            allowPlanting(true);
        }
        else {
            for (int i = 0; i < objectsToDeactivate.Length; i++) {
                objectsToDeactivate[i].SetActive(false);
            }
            allowPlanting(false);
        }
    }

    public void startWave() {
        waveManager.startNextWave();
        activatePlantingMode(false);
    }

    public void allowPlanting(bool state) {
        planting.enabled = state;
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

    private void displayScore() {
        RiparianForestCompletation.OnRiparianForestIsReady -= displayScore; // when display score can unsubscribe, so that displayScore isn't called more than once
        timer.pauseTimer();
        score.calculateScore(expectedMissionDurationInSeconds);
        score.showScore();
    }

}

enum GameState {

    Protect,
    Plant,
    DisplayScore

}