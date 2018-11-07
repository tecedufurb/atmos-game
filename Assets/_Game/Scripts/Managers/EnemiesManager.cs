using System;
using UnityEngine;

public class EnemiesManager : MonoBehaviour {

    public TrollManager trollManager;

    public void spawnTroll(TrollConfiguration trollConfiguration, int spawnNumber = int.MaxValue) {
        // if spawNumber is omitted spawn on random spawn
        // if sideOfRiver is omitted spawn on random side
        if (trollConfiguration.getIsOnLeftSideOfRiver()) // left side
            trollManager.spawnTrollOnLeftSide(trollConfiguration,spawnNumber);
        else if (!trollConfiguration.getIsOnLeftSideOfRiver()) // right side
            trollManager.spawnTrollOnRightSide(trollConfiguration,spawnNumber);
        else
            throw new Exception("Invalid spawn location: ");
    }

    public bool checkIfAllEnemiesAreDead() {
        return trollManager.checkIfTrollsAreDead();
    }

    public void unspawnAllEnemies() { //TODO CALL THIS
        trollManager.sendAllTrollsToUnspawn();
    }

}