using UnityEngine;
using UnityEngine.UI;

public class RiparianForestCompletation : MonoBehaviour {

    public Text completationIndicator;
    public PlantsDiversity plantsDiversity; // TODO remove this from here, and implement plantDiversity
    private int nextValueObjective = 22;

    private int currentStatus = 00;

    private readonly int qntOfPlantsToCompletation = 65;

    #region Events

    public delegate void RiparianForestIsReady();

    public static event RiparianForestIsReady OnRiparianForestIsReady;

    #endregion

    private void OnEnable() {
        PlantsManager.OnRiparianForestChanged += updateIndicator;
    }

    private void OnDisable() {
        PlantsManager.OnRiparianForestChanged -= updateIndicator;
    }


    private void updateIndicator(int qntOfPlantsInTerrain) {
        // every time a plant is added to terrain or removed, the indicator updates

        int completation = qntOfPlantsInTerrain * 100 / qntOfPlantsToCompletation;
        if (completation > nextValueObjective) {
            plantsDiversity.moreDiversity();
            nextValueObjective *= 2;
        }

        if (completation >= 100) {
            completationIndicator.text = "100%";
            if (OnRiparianForestIsReady != null)
                OnRiparianForestIsReady();
            return;
        }

        completationIndicator.text = completation.ToString("00") + "%";
    }

}