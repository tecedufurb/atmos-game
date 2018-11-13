using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractEnemy : MonoBehaviour { // TODO add require NavMeshAgent, and EnemyAnimator


    protected int life;
    protected int speed;
    protected NavMeshAgent navMeshAgent;
    protected Transform target;
    protected EnemyAnimator enemyAnimator;
    protected Transform unspawnPoint;
    public Collider attackCollider;

    protected float defaultStunTime;

    protected bool isWaiting = true; // waiting for animation to end, or have just spawned and dont have a target
    protected bool isStunned;

    private Coroutine waitingCoroutine; // used to block troll from attacking when it shouldn't
    private Coroutine stunnedCoroutine;

    void Start() {
        gameObject.SetActive(true);
        enemyAnimator = GetComponent<EnemyAnimator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed =
            0; // when troll is spawned it doesn't have a speed, it receive its speed on initializeTroll

        //enemyAnimator.idle();
    }

    public abstract void initializeEnemyConfiguration(IEnemyConfiguration enemyConfiguration);

    public abstract void takeDamage();

    public abstract void die();

    public abstract void vanish();

    protected abstract void walkToCurrentTarget();

    protected abstract void attack();

    protected void stopEnemy() {
        navMeshAgent.isStopped = true;
    }

    protected bool haveReachedDestination() {
        // Check if we've reached the destination
        if (navMeshAgent.enabled)
            if (!navMeshAgent.pathPending)
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                        return true;
        return false;
    }

    protected bool canAttack() {
        if (enemyAnimator.checkIfIsAttacking())
            return false;
        if (isStunned || isWaiting)
            return false;
        return true;
    }

    protected void setAttackCollider(bool value) {
        attackCollider.enabled = value;
    }

    protected void doNothing(float seconds) {
        isWaiting = true;
        if (waitingCoroutine != null) // check if coroutine isn't running already
            StopCoroutine(waitingCoroutine);
        waitingCoroutine = StartCoroutine(coroutineWaiting(seconds));
    }

    protected void getStunned() {
        isStunned = true;
        if (stunnedCoroutine != null) // check if coroutine isn't running already
            StopCoroutine(stunnedCoroutine);
        stunnedCoroutine = StartCoroutine(coroutineStunned(defaultStunTime));
    }

    private IEnumerator coroutineWaiting(float seconds) {
        yield return new WaitForSeconds(seconds);
        isWaiting = false;
        waitingCoroutine = null;
    }

    private IEnumerator coroutineStunned(float seconds) {
        yield return new WaitForSeconds(seconds);
        isStunned = false;
        stunnedCoroutine = null;
    }

}