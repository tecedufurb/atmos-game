using UnityEngine;
using UnityEngine.UI;

public class PlantsDetails : MonoBehaviour {

    public GameObject objetoASerControladoNoClick;  // canvas que vai ser ativado e desativado
    public GameObject joystickASerControlado; //joystick visao de cima a ser desativado 
    public static bool botaoDetalhePlanta = false; //estado do botao Ver Detalhes Planta
    public GameObject buttonVerDetalhesPlanta; //botao VerDetalhesPlanta

    void OnEnable() {
        if (gameObject.name == "DetalhePlantaPanel") {
            joystickASerControlado.SetActive(false);
            objetoASerControladoNoClick.SetActive(true);
            botaoDetalhePlanta = false;
        }
    }

    //quando clica no botoa X vermelho
    public void onClickClose() {
        joystickASerControlado.SetActive(true);
        objetoASerControladoNoClick.SetActive(false); //desativa o painel
        botaoDetalhePlanta = false;
        buttonVerDetalhesPlanta.GetComponent<Image>().color = new Color32(255, 255, 255, 255); //restaura a cor do botao ao normal
    }

    public void onClickBotaoDetalhePlanta() {
        if (botaoDetalhePlanta) {//se ja esta ativo 
            botaoDetalhePlanta = false; //seta o botao como desativo/falso
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        } else {//seta o botao (colocar a cor azul)
            botaoDetalhePlanta = true; //seta o botao como ativo
            gameObject.GetComponent<Image>().color = new Color32(50, 97, 143, 255);
        }
    }
}
