using System.Collections.Generic;
using UnityEngine;

public class PlantsSingleton : MonoBehaviour {

    private static PlantsSingleton mInstance;
    private List<InformacoesBotaoIntroductionScene> m_SelectedPlants;

    public static PlantsSingleton Instance {
        get {
            return mInstance;
        }

        set {
            mInstance = value;
        }
    }

    public List<InformacoesBotaoIntroductionScene> SelectedPlants {
        get {
            return m_SelectedPlants;
        }

        set {
            m_SelectedPlants = value;
        }
    }

    void Awake() {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}