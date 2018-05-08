using System;
using UnityEngine;

[Serializable]
public class JsonControllerPlantasCorretas{
    public PlantaCorreta[] plantasCorretas; //array de plantas

    public JsonControllerPlantasCorretas() {

    }
    public static JsonControllerPlantasCorretas transformaJson() //metodo que transforma json em texto
    {
        TextAsset texto = Resources.Load("plantasCorretas") as TextAsset;
        return JsonUtility.FromJson<JsonControllerPlantasCorretas>(texto.text);
    }
}

[Serializable] //classe que armazena os valores
public class PlantaCorreta {
    public string nomePlanta;
    public string grupo;
}