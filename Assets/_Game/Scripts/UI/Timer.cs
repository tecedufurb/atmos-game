using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour {

    private Coroutine timerCoroutine;
    private int secondsElapsed;

    public void startTimer() {
        timerCoroutine = StartCoroutine(updateTimer());
    }

    public void resumeTimer() {
        timerCoroutine = StartCoroutine(updateTimer());
    }

    public void pauseTimer() {
        StopCoroutine(timerCoroutine);
    }

    public int getTotalTimeElapsed() {
        return secondsElapsed;
    }

    public void resetTimer() {
        secondsElapsed = 0;
        resumeTimer();
    }

    private IEnumerator updateTimer() {
        while (true) {
            yield return new WaitForSeconds(1);
            secondsElapsed++;
        }
    }

}