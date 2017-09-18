using UnityEngine;

public class DetalhePlantaManeger : MonoBehaviour {

    public GameObject objetoASerControladoNoClick;  // canvas que vai ser ativado e desativado
    public GameObject joystickASerControlado; //joystick visao de cima a ser desativado 

    void OnEnable() {
        if (gameObject.name == "DetalhePlantaPanel") {
            joystickASerControlado.active = false;
            //Debug.Log("estado: " + joystickASerControlado.active);
            objetoASerControladoNoClick.active = true;
        }
    }

    public void onClickClose() //quando clica no botao X vermelho
    {
        joystickASerControlado.active = true;
        objetoASerControladoNoClick.active = false; //desativa o painel
    }

    void OnDisable() { //seta detalhe panel para inicio do scroll
        if (gameObject.name != "CloseButton") { //se nao é o objeto CloseButton
            var t = gameObject.transform.GetChild(0).gameObject;
            t.GetComponent<RectTransform>().offsetMax = new Vector2(0.0f, 0.0f);
            t.GetComponent<RectTransform>().offsetMin = new Vector2(-0.5f, -200f);
        }
    }
}
