using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class TrollController : AbstractEnemy {

    private Transform unspawnPoint;
    public Collider clubCollider;

    private bool isAttacking;
    private int lastAttackType;
    private bool isOnLeftSideOfRiver;
    private bool isChasing = true;

    private float stunTime;
    private float currentStunTime;
    private bool isStunned;

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

    public void initializeTroll(TrollConfiguration trollConfiguration, bool sideOfRiver) {
        life = trollConfiguration.getLife();
        speed = trollConfiguration.getSpeed();
        stunTime = trollConfiguration.getStunTime();
        isOnLeftSideOfRiver = sideOfRiver;
        unspawnPoint = TargetSeeker.getInstance().getEndPoint(isOnLeftSideOfRiver);
    }

    void Start() {
        gameObject.SetActive(true);
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        enableClubCollider(false);
        setNextTarget();
        goToTarget();
    }

    public override void goToTarget() {
        if (!navMeshAgent.enabled) // check if navMesh is enabled
            return;
        navMeshAgent.SetDestination(target.position);
        animator.SetBool("Move", true);
        isAttacking = false;
    }

    public override void hit() {
        animator.SetTrigger("Hit1");
        life--;
        if (life <= 0)
            die();
        else if (isAttacking) {
            isAttacking = false;
            isStunned = true;
            currentStunTime = 0;
        }
    }

    public override void attack() {
        if (CheckIfIsAttacking()) //if already is attacking, do nothing
            return;
        enableClubCollider(true);
        transform.LookAt(target.transform);
        isAttacking = true;
        lastAttackType = Random.Range(1, 3);
        animator.SetTrigger("Attack" + lastAttackType);
    }

    private bool checkIfAttackHaveEnded() {
        if (lastAttackType == 1) { // verify if animation have ended based on last attack type
            if (!AnimatorIsPlaying("strike")) {
                isAttacking = false;
                enableClubCollider(false);
                return true;
            }
        }
        else {
            if (!AnimatorIsPlaying("powerstrike")) {
                isAttacking = false;
                enableClubCollider(false);
                return true;
            }
        }

        return false;
    }

    private bool CheckIfIsAttacking() {
        if (AnimatorIsPlaying("strike") || AnimatorIsPlaying("powerstrike"))
            return true;
        return false;
    }

    void Update() {
        if (isChasing)
            // Check if we've reached the destination
            if (!navMeshAgent.pathPending) {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f) {
                        if (!isAttacking && !isStunned)
                            attack();
                        else {
                            if (checkIfAttackHaveEnded())
                                if (isStunned)
                                    updateIsStunned();
                                else {
                                    setNextTarget();
                                    goToTarget();
                                }
                        }
                    }
                }
            }
    }

    private void updateIsStunned() {
        currentStunTime += Time.deltaTime;
        if (currentStunTime > stunTime) {
            currentStunTime = 0;
            isStunned = false;
        }
    }

    private void checkIfMustChangeTarget(GameObject plant) {
        if (plant.name != target.name)
            return;
        setNextTarget();
        goToTarget();
    }

    protected override void resetVariables() {
        target = null;
        isAttacking = false;
        lastAttackType = 0;
        currentStunTime = 0;
        isStunned = false;
    }

    private void stopChasing() {
        navMeshAgent.isStopped = true;
        isChasing = false;
    }

    private void disableColliders() { // disable colliders to troll sink into ground
        GetComponent<BoxCollider>().enabled = false;
        enableClubCollider(false);
        navMeshAgent.enabled = false;
    }

    public override void die() {
        stopChasing();
        disableColliders();
        animator.SetTrigger("Die" + Random.Range(1, 3));
        //resetVariables();
        if (OnTrollIsDead != null)
            OnTrollIsDead(gameObject);
    }

    private void setNextTarget() {
        if (OnGetNextTarget != null)
            target = OnGetNextTarget(isOnLeftSideOfRiver);
    }

    public void sentTrollToUnspawn() {
        target = unspawnPoint;
        goToTarget();
    }

    private void enableClubCollider(Boolean value) {
        clubCollider.enabled = value;
    }

}