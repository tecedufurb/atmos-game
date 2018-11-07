using System.Collections.Generic;

public class WavesPrefab {

    private static readonly List<ChainOfWaves> wavesEasy = new List<ChainOfWaves>();
    private static readonly List<ChainOfWaves> wavesMedium = new List<ChainOfWaves>();
    private static readonly List<ChainOfWaves> wavesHard = new List<ChainOfWaves>();

    private static readonly TrollConfiguration trollConfigurationEasy = new TrollConfiguration(2, 0.5f, 30);

    private static void easyWaveModel1() {
        Wave wave1 = new Wave(5, new float[] {1}, 3f);
        Wave wave2 = new Wave(10, new float[] {1}, 2f);
        Wave wave3 = new Wave(15, new float[] {1}, 1.5f);
        Wave wave4 = new Wave(20, new float[] {1}, 1f);
        Wave wave5 = new Wave(20, new float[] {1}, 1.3f);
        Wave wave6 = new Wave(20, new float[] {1}, 1.3f);

        
        ChainOfWaves easyWave1 =
            new ChainOfWaves(new List<Wave>(new Wave[] {wave1, wave2, wave3, wave4,wave5,wave6}),
                new List<EnemyOfWaveConfiguration>(new EnemyOfWaveConfiguration[] { new EnemyOfWaveConfiguration(trollConfigurationEasy, 0.5f)}));
        wavesEasy.Add(easyWave1);
    }

    static WavesPrefab() { // initialize waves prefab
        easyWaveModel1();
    }

    public static ChainOfWaves getWave(Difficulties difficulty) {
        if (difficulty == Difficulties.Easy)
            return wavesEasy[0];
        if (difficulty == Difficulties.Medium)
            return wavesMedium[0];
        if (difficulty == Difficulties.Hard)
            return wavesHard[0];
        throw new System.Exception("Difficulty not found: " + difficulty.ToString());
    }

}

public enum Difficulties {

    Easy,
    Medium,
    Hard

}
