using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExibirAreaApp : MonoBehaviour {

    public GameObject areaApp1;
    public GameObject areaApp2;
    public GameObject botaoConfiguracoes;
    public GameObject setaVoltar;
	
	public void exibirApp() {
        areaApp1.GetComponent<MeshRenderer>().enabled = true;
        areaApp2.GetComponent<MeshRenderer>().enabled = true;
        botaoConfiguracoes.SetActive(false);
        setaVoltar.SetActive(true);
    }
}
