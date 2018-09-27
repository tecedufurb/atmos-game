using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantsDiversity : MonoBehaviour {

    public Image currentQuality;
    
    private PlantsManager listOfPlants;
    private List<Color> diversityQualityOptions = new List<Color>(new Color[] {terrible,bad,medium,good});

    private int actualState = 0;
    #region Colors

    private static readonly Color terrible = new Color32(168, 0, 0,255);
    private static readonly Color bad = new Color32(254, 108, 3,255);
    private static readonly Color medium = new Color32(230, 254, 0,255);
    private static readonly Color good = new Color32(0, 168, 36,255);

    #endregion


    private void Start() {
        listOfPlants = GetComponent<PlantsManager>();
        currentQuality.color = diversityQualityOptions[0];
    }

    public void updateDiversity() { // TODO criar sistema para calcular diversidade da mata ciliar 
        
        //listOfPlants.getPlantsOfRiparianForest();

    }


    public void moreDiversity() {
        if(actualState==diversityQualityOptions.Count)
            return;
        actualState++;
        currentQuality.color = diversityQualityOptions[actualState];
    }
    
    public void lessDiversity() {
        if(actualState==0)
            return;
        actualState--;
        currentQuality.color = diversityQualityOptions[actualState];
    }

    public int getDiversity() {
        return actualState;
    }

}