using System.Collections.Generic;
using UnityEngine;

public class PlantsSingleton : MonoBehaviour {

    private static PlantsSingleton mInstance;
    private List<ButtonInformations> m_SelectedPlants = new List<ButtonInformations>();

    public static PlantsSingleton Instance {
        get {
            if (mInstance == null)
                mInstance = FindObjectOfType<PlantsSingleton>();

            return mInstance;
        }
    }

    public List<ButtonInformations> SelectedPlants {
        get {
            return m_SelectedPlants;
        }

        set {
            m_SelectedPlants = value;
        }
    }

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}