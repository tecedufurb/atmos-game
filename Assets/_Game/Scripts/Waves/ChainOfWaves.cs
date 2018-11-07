using System.Collections.Generic;

public class ChainOfWaves {

    private readonly List<Wave> waves;
    private readonly List<EnemyOfWaveConfiguration> enemiesCofiguration;

    public ChainOfWaves(List<Wave> waves,List<EnemyOfWaveConfiguration> enemiesCofiguration) {
        this.waves = waves;
        this.enemiesCofiguration = enemiesCofiguration;
    }

    public Wave getWave(int position) {
        return waves[position];
    }
    
    public IEnemyConfiguration getEnemyConfiguration(int position) {
        return enemiesCofiguration[position].getEnemy();
    }

}