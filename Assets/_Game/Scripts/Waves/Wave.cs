public class Wave {

    private readonly short qntOfEnemies;
    private readonly float[] percentageOfEachEnemy;
    private readonly float respawnTime;

    public Wave(short qntOfEnemies, float[] percentageOfEachEnemy, float respawnTime) {
        this.qntOfEnemies = qntOfEnemies;
        this.percentageOfEachEnemy = percentageOfEachEnemy;
        this.respawnTime = respawnTime;
    }

    public short getQntOfEnemies() {
        return qntOfEnemies;
    }

    public float[] getPercentageOfEachEnemy() {
        return percentageOfEachEnemy;
    }

    public float getRespawnTime() {
        return respawnTime;
    }

}