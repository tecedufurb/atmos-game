using UnityEngine;
using UnityEngine.UI;

public class PlantUiController : MonoBehaviour {
    private Image father;
    private bool isSelected;
    private static readonly Color ColorSelected = new Color32(0, 174, 0, 255);
    private static readonly Color ColorWhite = new Color32(255, 255, 255, 255);

    #region Events
    public delegate bool SelectPlant(int plant);
    public static event SelectPlant OnSelectPlant;

    public delegate void SelectMorePlantsThanAllowed();
    public static event SelectMorePlantsThanAllowed OnSelectMorePlantsThanAllowed;
    #endregion


    void Start() {
        father = transform.parent.GetComponentInParent<Image>();
    }

    public void selectPlant() {
        if (OnSelectPlant != null)
            if (!OnSelectPlant(int.Parse(father.name))) // if cant select plant
                if (OnSelectMorePlantsThanAllowed != null) {
                    OnSelectMorePlantsThanAllowed(); // send message to player that cannot select more plants
                    return;
                }

        isSelected = !isSelected;
        if (isSelected)
            father.color = ColorSelected;
        else
            father.color = ColorWhite;
    }
}