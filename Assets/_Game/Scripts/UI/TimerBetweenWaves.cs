using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerBetweenWaves : MonoBehaviour {

    private Image timer;
    private Coroutine timerCoroutine;
    private float fillAmount;

    #region Events

    public delegate void TimeIsUp();

    public static event TimeIsUp OnTimeIsUp;

    #endregion

    void Start() {
        timer = GetComponent<Image>();
    }

    public void startTimer(int timeBetweenWave) {
        timer.fillAmount = 1f;
        fillAmount = 1f / timeBetweenWave;
        resumeTimer();
    }


    public void resumeTimer() {
        if (gameObject.activeInHierarchy) // only resume timer if is active
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