using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollManager : MonoBehaviour {

    public GameObject trollPrefab;
    public Transform fatherOfTrolls;
    
    public Transform[] spawnLocationsOnLeftSide;
    public Transform[] spawnLocationsOnRightSide;

    private List<GameObject> trolls = new List<GameObject>();

    private int qntdOfTrollsCreated = 0; // used to name trolls

    private void OnEnable() {
        TrollController.OnTrollIsDead += removeTroll;
    }

    private void OnDisable() {
        TrollController.OnTrollIsDead -= removeTroll;
    }

    public void spawnTrollOnLeftSide(TrollConfiguration trollConfiguration, int spawnNumber = int.MaxValue) {
        if (spawnNumber == int.MaxValue) { // if parameter is omitted spawn on random spawn
            GameObject troll = Instantiate(trollPrefab,
                spawnLocationsOnLeftSide[Random.Range(0, spawnLocationsOnLeftSide.Length)].position,
                new Quaternion(), fatherOfTrolls);
            StartCoroutine(initializeTroll(troll, trollConfiguration));
            troll.name = "Troll" + qntdOfTrollsCreated;
            trolls.Add(troll);
        }
        else {
            GameObject troll = Instantiate(trollPrefab,
                spawnLocationsOnLeftSide[spawnNumber].position,
                new Quaternion(), fatherOfTrolls);
            StartCoroutine(initializeTroll(troll, trollConfiguration));
            troll.name = "Troll" + qntdOfTrollsCreated;
            trolls.Add(troll);
        }

        qntdOfTrollsCreated++;
    }

    public void spawnTrollOnRightSide(TrollConfiguration trollConfiguration, int spawnNumber = int.MaxValue) {
        if (spawnNumber == int.MaxValue) { // if parameter is omitted spawn on random spawn
            GameObject troll = Instantiate(trollPrefab,
                spawnLocationsOnRightSide[Random.Range(0, spawnLocationsOnRightSide.Length)].position,
                new Quaternion(), fatherOfTrolls);
            StartCoroutine(initializeTroll(troll, trollConfiguration));
            troll.name = "Troll" + qntdOfTrollsCreated;
            trolls.Add(troll);
        }
        else {
            GameObject troll = Instantiate(trollPrefab,
                spawnLocationsOnRightSide[spawnNumber].position,
                new Quaternion(), fatherOfTrolls);
            StartCoroutine(initializeTroll(troll, trollConfiguration));
            troll.name = "Troll" + qntdOfTrollsCreated;
            trolls.Add(troll);
        }

        qntdOfTrollsCreated++;
    }

    public void spawnTrollOnRandomSide(TrollConfiguration trollConfiguration, int spawnNumber = int.MaxValue) {
        if (Random.Range(0, 2) == 0)
            spawnTrollOnLeftSide(trollConfiguration, spawnNumber);
        else
            spawnTrollOnRightSide(trollConfiguration, spawnNumber);
    }

    private void removeTroll(GameObject troll) {
        for (int i = 0; i < trolls.Count; i++)
            if (trolls[i].name == troll.name) {
                trolls.RemoveAt(i);
                return;
            }

        print("Enemy have not been removed!");
    }

    public bool checkIfTrollsAreDead() {
        return trolls.Count == 0;
    }

    public void sendAllTrollsToUnspawn() {
        foreach (GameObject troll in trolls) {
            troll.GetComponent<TrollController>().sentTrollToUnspawn();
        }
    }
    
    private IEnumerator initializeTroll(GameObject troll,TrollConfiguration trollConfiguration) { // wait 0.5 because start haven't played yet
        yield return new WaitForSeconds(0.5f);
        troll.GetComponent<TrollController>().initializeEnemyConfiguration(trollConfiguration);
    }

}