using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetalhePlantaManeger : MonoBehaviour {

    public GameObject objetoASerControladoNoClick;  // canvas que vai ser ativado e desativado
    public GameObject joystickASerControlado; //joystick visao de cima a ser desativado 
    public static bool botaoDetalhePlanta = false; //estado do botao Ver Detalhes Planta
    public GameObject buttonVerDetalhesPlanta; //botao VerDetalhesPlanta

    void OnEnable() {
        if (gameObject.name == "DetalhePlantaPanel")
        {
            //Debug.Log("desativar joy");
            joystickASerControlado.active = false;
            //Debug.Log("estado: " + joystickASerControlado.active);
            objetoASerControladoNoClick.active = true;
            botaoDetalhePlanta = false;
        }
    }

    public void onClickClose() //quando clica no botoa X vermelho
    {
        //Debug.Log("onclickclose");
        joystickASerControlado.active = true;
        objetoASerControladoNoClick.active = false; //desativa o painel
        botaoDetalhePlanta = false;
        buttonVerDetalhesPlanta.GetComponent<Image>().color = new Color32(255, 255, 255, 255); //restaura a cor do botao ao normal
    }

    public void onClickBotaoDetalhePlanta() {
        //Debug.Log("onclickbutton");
        if (botaoDetalhePlanta)//se ja esta ativo 
        {
            botaoDetalhePlanta = false; //seta o botao como desativo/falso
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        } else //seta o botao (colocar a cor azul)
          {
            botaoDetalhePlanta = true; //seta o botao como ativo
            gameObject.GetComponent<Image>().color = new Color32(50, 97, 143, 255);
        }
    }
}
