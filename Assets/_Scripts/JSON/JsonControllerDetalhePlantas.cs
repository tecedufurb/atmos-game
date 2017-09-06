using System;
using UnityEngine;

[Serializable]
public class JsonControllerDetalhePlantas {
    public Planta[] plantas; //array de plantas

    public JsonControllerDetalhePlantas() {

    }
    public static JsonControllerDetalhePlantas transformaJson() //metodo que transforma json em texto
    {
        TextAsset texto = Resources.Load("plantasDetalhes") as TextAsset;
        return JsonUtility.FromJson<JsonControllerDetalhePlantas>(texto.text);
    }
}

[Serializable] //classe que armazena os valores
public class Planta {
    public string nomePopular;
    public string nomeCientifico;
    public string imagem;
    public string informacoes;
}