using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Timer timer;
    public PlantsDiversityValidation plantsDiversity;

    #region Score

    [Header("Score")]
    public Text timeBonus;

    public Text diversityBonus;
    public Text totalScore;
    public Text grade;

    #endregion

    public void calculateScore(int expectedMissionDurationInSeconds) {

        int pointsByTime = 0;
        if (timer.getTotalTimeElapsed() < expectedMissionDurationInSeconds) // if have take fewer time than expected
            pointsByTime = (int) (Mathf.Abs(timer.getTotalTimeElapsed() - expectedMissionDurationInSeconds) * 1.3f); // get bonus points of time
        if (pointsByTime > 0 && pointsByTime < 50) // set the minimum bonus to 50
            pointsByTime = 50;

        timeBonus.text = "Bonus de tempo:    " + pointsByTime;

        int pointsByDiversity = plantsDiversity.getDiversity() * 60;
        diversityBonus.text = "    Diversidade:    " + pointsByDiversity;

        int total = pointsByTime + pointsByDiversity;
        totalScore.text = "Total: " + total + " pts";

        if (total >= 312)
            grade.text = "9.5/10";
        else if (total >= 234)
            grade.text = "8/10";
        else if (total >= 156)
            grade.text = "7.5/10";
        else
            grade.text = "6/10";
    }

    public void showScore() {
        gameObject.SetActive(true);
    }

}