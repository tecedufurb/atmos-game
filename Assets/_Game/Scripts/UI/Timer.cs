using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private Text timerText;

    private int initialTime;
    private float currentTime;
    private bool timerRunning;
    private TextModifier textModifier;


    #region Helpers

    private bool changeColorOfTimer = true;
    private bool changeScaleOfTimer = true;
    private Coroutine coroutineScaleOfText;

    #endregion

    #region Events

    public delegate void TimeIsUpEvent();

    public static event TimeIsUpEvent OnTimeIsUp;

    #endregion

    void Start() {
        timerText = GetComponent<Text>();
        textModifier = GetComponent<TextModifier>();
    }

    void Update() {
        if (timerRunning) {
            currentTime -= Time.deltaTime;
            timerText.text = timeIntToMinutes(currentTime);
            if (currentTime <= 0) {
                StopCoroutine(coroutineScaleOfText);
                if (OnTimeIsUp != null) {
                    OnTimeIsUp();
                    pauseTimer();
                }
            }

            if (changeColorOfTimer && currentTime <= 180) {
                changeColorOfTimer = false;
                textModifier.changeColorOfText(timerText, Color.yellow);
            }

            if (changeScaleOfTimer && currentTime <= 60) {
                changeScaleOfTimer = false;
                textModifier.changeColorOfText(timerText, Color.red);
                coroutineScaleOfText = StartCoroutine(textModifier.changeSizeOfText(timerText, 15));
            }
        }
    }

    public void startTimer(int timeInSeconds) {
        initialTime = timeInSeconds;
        currentTime = timeInSeconds;
        resumeTimer();
    }

    public string timeIntToMinutes(float timeInSeconds) {
        return ((int) timeInSeconds / 60).ToString("00") + ":" + ((int) timeInSeconds % 60).ToString("00");
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