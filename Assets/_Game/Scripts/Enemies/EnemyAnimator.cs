using UnityEngine;

public class EnemyAnimator : MonoBehaviour {  // TODO add require Animator

    public Animator animator;

    public void walk() {
        animator.SetBool("Move", true);
    }
    
    public void idle() {
        animator.SetTrigger("Idle");
    }

    public bool animatorIsPlaying() {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public bool animatorIsPlaying(string stateName) {
        return animatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    public bool checkIfIsAttacking() {
        if (animatorIsPlaying("strike") || animatorIsPlaying("powerstrike"))
            return true;
        return false;
    }

    public void attack(int attackNumber, string attackName = "Attack") {
        animator.SetTrigger(attackName + attackNumber);
    }
    
    public void takeHit(int hitNumber, string hitName = "Hit") {
        animator.SetTrigger(hitName + hitNumber);
    }
    
    public void die(int deathNumber, string deathName = "Die") {
        animator.SetTrigger(deathName + deathNumber);
    }
}