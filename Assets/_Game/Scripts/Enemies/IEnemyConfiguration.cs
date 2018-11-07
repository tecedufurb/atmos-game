public class IEnemyConfiguration {

    // used as base by all enemies
    
    protected bool isOnLeftSideOfRiver;
    private readonly short life;
    private readonly float stunTime;
    private readonly short speed;

    public IEnemyConfiguration(short life, float stunTime, short speed) {
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

    public bool getIsOnLeftSideOfRiver() {
        return isOnLeftSideOfRiver;
    }
    
    public void setIsOnLeftSideOfRiver(bool isOnLeftSideOfRiver) {
        this.isOnLeftSideOfRiver = isOnLeftSideOfRiver;
    }
}
