using System.Collections;
using UnityEngine;

public class EnemyRecycler : MonoBehaviour {

    private readonly int secondsToRecycleEnemy = 4;
    private readonly int heightToVanishEnemy = -5;

    #region Events

    public delegate void RecycleEnemy();

    public static event RecycleEnemy OnRecycleEnemy;

    #endregion

    private void OnEnable() {
        TrollController.OnTrollIsDead += recycleEnemy;
    }

    private void OnDisable() {
        TrollController.OnTrollIsDead -= recycleEnemy;
    }

    void recycleEnemy(GameObject enemy) {
        StartCoroutine(waitTimeToVanishEnemy(enemy.transform, secondsToRecycleEnemy));
    }

    private IEnumerator waitTimeToVanishEnemy(Transform enemy, int seconds) {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(vanishEnemy(enemy));
    }

    private IEnumerator vanishEnemy(Transform enemy) {
        float currentHeight = enemy.transform.position.y;
        while (currentHeight > heightToVanishEnemy) {
            enemy.transform.Translate(Vector3.down * 1);

            currentHeight = enemy.transform.position.y;
            yield return new WaitForSeconds(0.1f); // move enemy each 0.1 seconds
        }

        sendToPoll(enemy);
    }

    private void sendToPoll(Transform enemy) {
        Destroy(enemy.gameObject);
        // enemy.gameObject  //send to poll..
    }

}