using System;
using UnityEngine;

[Serializable]
public class JsonControllerPlantasErradas {
    public PlantaIncorreta[] plantasIncorretas; //array de plantas incorretas

    public JsonControllerPlantasErradas() {

    }
    public static JsonControllerPlantasErradas transformaJson() //metodo que transforma json em texto
    {
        TextAsset texto = Resources.Load("plantasIncorretas") as TextAsset;
        return JsonUtility.FromJson<JsonControllerPlantasErradas>(texto.text);

    }
}

[Serializable] //classe que armazena os valores
public class PlantaIncorreta {

    public string nomePlanta;

}
