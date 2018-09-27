using System;
using UnityEngine;

public class EnemiesManager : MonoBehaviour {

    public TrollManager trollManager;

    public void spawnTroll(TrollConfiguration trollConfiguration,char sideOfRiver='0', int spawnNumber = int.MaxValue) {
        // if spawNumber is omitted spawn on random spawn
        // if sideOfRiver is omitted spawn on random side
        if (sideOfRiver == 'l') // left side
            trollManager.spawnTrollOnLeftSide(trollConfiguration,spawnNumber);
        else if (sideOfRiver == 'r') // right side
            trollManager.spawnTrollOnRightSide(trollConfiguration,spawnNumber);
        else if (sideOfRiver == '0') // random
            trollManager.spawnTrollOnRandomSide(trollConfiguration,spawnNumber);
        else
            throw new Exception("Invalid spawn location: " + sideOfRiver);
    }

    public bool checkIfAllEnemiesAreDead() {
        return trollManager.checkIfTrollsAreDead();
    }

    public void unspawnAllEnemies() { //TODO CALL THIS
        trollManager.sendAllTrollsToUnspawn();
    }

}