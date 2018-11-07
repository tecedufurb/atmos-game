using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class TrollController : AbstractEnemy {

    private bool isOnLeftSideOfRiver;

    #region Events

    public delegate Transform GetNextTarget(bool isOnLeftSideOfRiver);

    public static event GetNextTarget OnGetNextTarget;

    public delegate void TrollIsDead(GameObject troll);

    public static event TrollIsDead OnTrollIsDead;

    #endregion

    private void OnEnable() {
        PlantController.OnPlantIsDead += checkIfMustChangeTarget;
    }

    private void OnDisable() {
        PlantController.OnPlantIsDead -= checkIfMustChangeTarget;
    }

    public override void initializeEnemyConfiguration(IEnemyConfiguration trollConfiguration) {
        print("init");
        TrollConfiguration trollConfig = trollConfiguration as TrollConfiguration;
        life = trollConfig.getLife();
        speed = trollConfig.getSpeed();
        navMeshAgent.speed = speed;
        defaultStunTime = trollConfig.getStunTime();
        isOnLeftSideOfRiver = trollConfig.getIsOnLeftSideOfRiver();

        isWaiting = false; // set to false so that enemy can attack
        setNextTarget(); // update current target when receive on witch side of river is
        walkToCurrentTarget();

        unspawnPoint = TargetSeeker.getInstance().getEndPoint(isOnLeftSideOfRiver);
    }

    protected override void walkToCurrentTarget() {
        print("walk");
        if (!navMeshAgent.enabled) // check if navMesh is enabled
            return;
        setAttackCollider(false);
        navMeshAgent.SetDestination(target.position);
        enemyAnimator.walk();
    }

    public override void takeDamage() {
        setAttackCollider(false);
        enemyAnimator.takeHit(1);
        life--;
        if (life <= 0)
            die();
        else if (enemyAnimator.checkIfIsAttacking()) {
            setAttackCollider(false);
            getStunned();
        }
    }

    protected override void attack() {
        print("attack");
        setAttackCollider(true);
        transform.LookAt(target.transform);
        enemyAnimator.attack(Random.Range(1, 3)); // 1 or 2

        doNothing(0.5f); // when starts a attack wait half a second before can attack again
    }

    void Update() {
        if (haveReachedDestination()) {
            if (canAttack()) {
                attack();
            }
        }
    }

    private void checkIfMustChangeTarget(GameObject plant) {
            if (target != null && plant.name != target.name)
                return;
        setNextTarget();
        walkToCurrentTarget();
    }

    private void disableColliders() { // disable colliders to troll sink into ground
        GetComponent<BoxCollider>().enabled = false;
        setAttackCollider(false);
        navMeshAgent.enabled = false; // agent have a collider
    }

    public override void die() {
        stopEnemy();
        disableColliders();
        enemyAnimator.die(Random.Range(1, 3));
        if (OnTrollIsDead != null)
            OnTrollIsDead(gameObject);
    }

    private void setNextTarget() {
        print("set next target");
        if (OnGetNextTarget != null)
            target = OnGetNextTarget(isOnLeftSideOfRiver);
    }

    public void sentTrollToUnspawn() {
        target = unspawnPoint;
        walkToCurrentTarget();
    }

}