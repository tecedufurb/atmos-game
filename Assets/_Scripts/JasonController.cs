using UnityEngine;
using System;

[Serializable]
public class JasonController {

    public Planta[] plantas; //array de plantas

    public JasonController()
    {
        
    }
    public static JasonController transformaJson() //metodo que transforma json em texto
    {
        TextAsset texto = Resources.Load("plantas") as TextAsset;
        return JsonUtility.FromJson<JasonController>(texto.text);//OU texto.text

    }
}

[Serializable] //classe que armazena os valores
public class Planta
{
    public string nomePopular;
    public string nomeCientifico;
    public string imagem;
    public string informacoes;

}
