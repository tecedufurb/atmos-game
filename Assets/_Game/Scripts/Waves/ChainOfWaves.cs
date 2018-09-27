using System.Collections.Generic;

public class ChainOfWaves {

    private readonly List<Wave> waves;
    private readonly List<IEnemyConfiguration> enemiesCofiguration;

    public ChainOfWaves(List<Wave> waves,List<IEnemyConfiguration> enemiesCofiguration) {
        this.waves = waves;
        this.enemiesCofiguration = enemiesCofiguration;
    }

    public Wave getWave(int position) {
        return waves[position];
    }
    
    public IEnemyConfiguration getEnemyConfiguration(int position) {
        return enemiesCofiguration[position];
    }

}