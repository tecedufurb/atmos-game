using UnityEngine;

public class EnemyEndPoint : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("EnemyAttack")) {
            other.gameObject.GetComponentInParent<TrollController>().die();
        }
    }
}