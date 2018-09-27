using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Timer timer;
    public PlantsDiversity plantsDiversity;


    #region Score

    [Header("Score")] public Text timeBonus;
    public Text diversityBonus;
    public Text totalScore;
    public Text grade;

    #endregion

    public void showScore() {
        int pointsByTime = (int) (timer.getTimeRemaining() * 1.1f);
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

}