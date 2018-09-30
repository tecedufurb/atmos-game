using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerBetweenWaves : MonoBehaviour {

    private Image timer;
    private Coroutine timerCoroutine;

    #region Events

    public delegate void TimeIsUp();

    public static event TimeIsUp OnTimeIsUp;

    #endregion

    void Start() {
        timer = gameObject.GetComponent<Image>();
    }

    public void startTimer(int timeBetweenWave) {
        timer.fillAmount = 1f;
        resumeTimer(timeBetweenWave);
    }


    public void resumeTimer(int timeBetweenWave) {
        float fillAmount = 1f / timeBetweenWave;
        timerCoroutine = StartCoroutine(updateTimer(fillAmount));
    }

    public void pauseTimer() {
        StopCoroutine(timerCoroutine);
    }

    private IEnumerator updateTimer(float fillAmount) {
        while (timer.fillAmount > 0) {
            timer.fillAmount -= fillAmount;
            yield return new WaitForSeconds(1);
        }

        if (OnTimeIsUp != null)
            OnTimeIsUp();
    }

}