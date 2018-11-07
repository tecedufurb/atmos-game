using UnityEngine;

public class EnemyOfWaveConfiguration {

    private IEnemyConfiguration leftSideEnemy;
    private IEnemyConfiguration rightSideEnemy;
    private float percentageBetweenLeftAndRight;

    public EnemyOfWaveConfiguration(IEnemyConfiguration enemyConfiguration, float percentageBetweenLeftAndRight) {
        this.percentageBetweenLeftAndRight = percentageBetweenLeftAndRight;

        enemyConfiguration.setIsOnLeftSideOfRiver(true);
        leftSideEnemy = enemyConfiguration;

        //TODO use a clone instead
        TrollConfiguration t = new TrollConfiguration(enemyConfiguration.getLife(), enemyConfiguration.getStunTime(),enemyConfiguration.getSpeed()); // hardcoded, use clone
        rightSideEnemy = t;
        rightSideEnemy.setIsOnLeftSideOfRiver(false);
    }

    public IEnemyConfiguration getEnemy() {
        float r = Random.Range(0, 101);
        if (r <= percentageBetweenLeftAndRight*100) // chose a random side of river, according to the percentage
            return leftSideEnemy;
        else {
            return rightSideEnemy;
        }
    }

}