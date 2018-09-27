 using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractEnemy : MonoBehaviour {

	protected int life;
	protected int speed;
	protected Animator animator;
	protected NavMeshAgent navMeshAgent;
	protected Transform target;

	public abstract void goToTarget();

	public abstract void hit();

	public abstract void attack();

	public abstract void die();

	protected abstract void resetVariables();

	protected bool AnimatorIsPlaying(){
		return animator.GetCurrentAnimatorStateInfo(0).length >
		       animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}

	protected bool AnimatorIsPlaying(string stateName){
		return AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
	}

	public Transform getCurrentTarget() {
		return target;
	}
}
