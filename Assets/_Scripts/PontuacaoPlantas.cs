using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontuacaoPlantas : MonoBehaviour {

    static int pontuacaoAtual;
    int qntPlantasCorretas;
    int qntPlantasIncorretas;
    //adicionr lista com os plantas do terreno

    public static void reachedMaxQuantity() {
        GameManager.podePlantar = false;
        DialogPlayerController.instance.setPontuacao(pontuacaoAtual);
        DialogPlayerController.instance.showEstatisticas();

    }

    void Start () {
		
        
    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
