using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontuacaoPlantas : MonoBehaviour {


    //adicionr lista com os plantas do terreno

    void Start () {
		
        
    }
	
	// Update is called once per frame
	void Update () {
        if (ControllerPontuacao.isQntAtualPlantasGreateOrEqualQntTotalPlantas()) {//se qntPlantas>=qntTotalPermitida
            GameManager.podePlantar = false;
            //chama evento exibir estatisticas USAR O DialogPlayer
        } 
    }
}
