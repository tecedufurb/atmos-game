using System;
using System.Collections;
using UnityEngine;

public class WavesManager : MonoBehaviour {

    public Difficulties difficulty;
    private ChainOfWaves waves;

    public EnemiesManager enemiesManager;

    private int currentWave;
    private bool hasWaveEnded;

    #region Events

    public delegate void WaveEnded();

    public static event WaveEnded OnWaveEnded;

    #endregion

    void Start() {
        waves = WavesPrefab.getWave(difficulty); //get wave acording to difficulty
    }

    private int[] getQuantityOfEachEnemy() {
        float[] percentageOfEachEnemy = waves.getWave(currentWave).getPercentageOfEachEnemy();

        int[] qntOfEachEnemy = new int[percentageOfEachEnemy.Length];
        int qntOfEnemies = waves.getWave(currentWave).getQntOfEnemies();

        for (int i = 0; i < percentageOfEachEnemy.Length; i++)
            qntOfEachEnemy[i] = (int) (percentageOfEachEnemy[i] * qntOfEnemies);
        return qntOfEachEnemy;
    }

    public void startNextWave() {
        int[] quantityOfEachEnemy = getQuantityOfEachEnemy();

        if (quantityOfEachEnemy.Length == 0)
            throw new Exception("Invalid quanitity of enemies: " + quantityOfEachEnemy);

        for (int i = 0; i < quantityOfEachEnemy.Length; i++)
            if (quantityOfEachEnemy[i] >= 0)
                StartCoroutine(callNextEnemy(quantityOfEachEnemy[i], waves.getWave(currentWave).getRespawnTime()));

        currentWave++;
    }

    private IEnumerator callNextEnemy(int quantity, float rate) {
        while (true) {
            if (quantity > 0) {
                quantity--;
                enemiesManager.spawnTroll((TrollConfiguration) waves.getEnemyConfiguration(0));
                yield return new WaitForSeconds(rate);
            }
            else {
                StartCoroutine(checkIfAllEnemiesAreDead());
                break;
            }
        }
    }

    private IEnumerator checkIfAllEnemiesAreDead(int seconds = 1) {
        while (true) {
            if (enemiesManager.checkIfAllEnemiesAreDead()) {
                if (OnWaveEnded != null)
                    OnWaveEnded();
                break;
            }

            yield return new WaitForSeconds(seconds);
        }
    }
}