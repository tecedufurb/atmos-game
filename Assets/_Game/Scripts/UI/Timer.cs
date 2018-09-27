using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private Text timerText;

    private int initialTime;
    private float currentTime;
    private bool timerRunning;

    #region Events

    public delegate void TimeIsUpEvent();

    public static event TimeIsUpEvent OnTimeIsUp;

    #endregion

    void Start() {
        timerText = GetComponent<Text>();
    }

    void Update() { //TODO deixar cor do tempo vermelho quando o tempo estiver acabando
        if (timerRunning) {
            currentTime -= Time.deltaTime;
            timerText.text = timeIntToMinutes(currentTime);
            if (currentTime <= 0) {
                if (OnTimeIsUp != null) {
                    OnTimeIsUp();
                    pauseTimer();
                }
            }
        }
    }

    public void startTimer(int timeInSeconds) {
        initialTime = timeInSeconds;
        currentTime = timeInSeconds;
        resumeTimer();
    }

    public string timeIntToMinutes(float timeInSeconds) {
        return ((int) currentTime / 60).ToString("00") + ":" + ((int) currentTime % 60).ToString("00");
    }

    public void resumeTimer() {
        timerRunning = true;
    }

    public void pauseTimer() {
        timerRunning = false;
    }

    public float getTimeRemaining() {
        return currentTime;
    }

    public void resetTimer() {
        currentTime = initialTime;
        resumeTimer();
    }

}