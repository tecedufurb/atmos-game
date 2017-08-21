using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerUI : MonoBehaviour {

	public void activateCanvas(GameObject canvasOrPanel) {
        canvasOrPanel.SetActive(true);
    }

    public void deactivateCanvas(GameObject canvasOrPanel) {
        canvasOrPanel.SetActive(false);
    }
}
