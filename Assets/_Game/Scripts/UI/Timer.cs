using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private Text timerText;

    private readonly int time = 300; // five minutes
    private float currentTime;
    private bool timerRunning;

    #region Events

    public delegate void TimeIsUpEvent();

    public static event TimeIsUpEvent OnTimeIsUp;

    #endregion

    void Start() {
        timerText = GetComponent<Text>();
        currentTime = time;
    }

    void Update() { //TODO deixar cor do tempo vermelho quando o tempo estiver acabando
        if (timerRunning) {
            currentTime -= Time.deltaTime;
            timerText.text = ((int) currentTime / 60).ToString("00") + ":" + ((int) currentTime % 60).ToString("00");
            if (currentTime <= 0) {
                if (OnTimeIsUp != null) {
                    OnTimeIsUp();
                    pauseTimer();
                    resetTimer();
                }
            }
        }
    }

    public void resumeTimer() {
        timerRunning = true;
    }

    public void pauseTimer() {
        timerRunning = false;
    }

    public void resetTimer() {
        currentTime = time;
    }

    public float getTimeRemaining() {
        return currentTime;
    }

}