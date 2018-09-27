public class TrollConfiguration : IEnemyConfiguration {

    // used to define the configuration of the troll

    private readonly short life;
    private readonly float stunTime;
    private readonly short speed;

    public TrollConfiguration(short life, float stunTime, short speed) {
        this.life = life;
        this.stunTime = stunTime;
        this.speed = speed;
    }

    public short getLife() {
        return life;
    }

    public float getStunTime() {
        return stunTime;
    }

    public short getSpeed() {
        return speed;
    }

}