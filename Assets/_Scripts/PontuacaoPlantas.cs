using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontuacaoPlantas : MonoBehaviour {

    private static int pontuacaoAtual;
    private static int qntPlantasCorretas;//qualquer 1 dos 3 grupos
    private static int qntPlantasIncorretas;//todos as plantas erradas
    public static List<string> nomesPlantasErradas = new List<string>();
    public static Dictionary<string,string> nomePlantaE_Grupo = new Dictionary<string,string>();

    //aqui ler arquivo json, fazer isso somente no start do jogo, talvez junto do game maneger, ou algo assim
    //ler arquivo json2 que tem o nome da plantas erradas

    public static void reachedMaxQuantity() {
        GameManager.podePlantar = false;

        DialogPlayerController.instance.showEstatisticas();

    }

    private static void SetValoresPanelEstatisticas() {
        DialogPlayerController.instance.setPontuacao(pontuacaoAtual);
        DialogPlayerController.instance.setPlantasCorretas(qntPlantasCorretas);
        DialogPlayerController.instance.setPlantasIncorretas(qntPlantasIncorretas);
    }
    public static int setValorPlanta(string nomePlanta) {
        int valor = 10;
        //pega as vars da classe e calcula

        return valor;
    }

    public static void atualizaQuantidadePlantasPontuacao(string nomePlanta) {
        //percorre as listas, compara os nomes, pega o grupo a que pertence
        //e ataualiza as vars da classe
    }



    void Start () {
		
        
    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
