using UnityEngine;

public class Timer : MonoBehaviour {

    private float currentTime;
    private bool timerRunning;

    void Update() {
        if (timerRunning) {
            currentTime += Time.deltaTime;
        }
    }

    public void startTimer() {
        resumeTimer();
    }

    public void resumeTimer() {
        timerRunning = true;
    }

    public void pauseTimer() {
        timerRunning = false;
    }

    public int getTotalTimeElapsed() {
        return (int)currentTime;
    }

    public void resetTimer() {
        currentTime = 0f;
        resumeTimer();
    }

}