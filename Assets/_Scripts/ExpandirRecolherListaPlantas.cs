using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandirRecolherListaPlantas : MonoBehaviour {

    private bool recolhido = false; //estado atual da lista

    public void onClick() {
        if (recolhido) { //se ja esta recolhido, expande lista
            recolhido = false;
            gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(-50, -50);
            gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(50, 50);
        }
        else { //se nao esta recolhido, recolhe a lista 
            recolhido = true;
            gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(96f, -50);
            gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(196, 50);
        }
    }
}
