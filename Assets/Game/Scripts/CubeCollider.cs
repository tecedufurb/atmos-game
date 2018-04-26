using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollider : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        RemoverPlanta.instance.resetCubo();
        if (other.gameObject.tag=="PlantaDoMundo") {
            RemoverPlanta.instance.recebePlanta(other.gameObject,true);
        }
        else {
            RemoverPlanta.instance.recebePlanta(other.gameObject,false);
        }
    }
}
