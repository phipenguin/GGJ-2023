using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum abilityName{ lightSlam, heavySlam, cannon};
public class Boss1Script : Entity
{
    [Header("Slam Attributes")]
    [SerializeField] private GameObject SlamReferenceLocation;
    [SerializeField] private GameObject SlammingTail;

    private bool isAttacking;
    [SerializeField]GameObject Player;
    Animator animator;
    Animator TailAnimator;
    void Start(){
        animator = GetComponent<Animator>();
        TailAnimator = SlammingTail.GetComponentInChildren<Animator>(); 
        if(Player == null)
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            attack(abilityName.lightSlam);
            //print("Performing Ability 1");
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            attack(abilityName.heavySlam);
            //print("Performing Ability 2");
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            attack(abilityName.cannon);
            print("Performing Ability 3");
        }
    }

    void attack(abilityName ability){
        if(!isAttacking){
            isAttacking = true;
            switch(ability){

                // ABILITY 1 // ------------------------------
                case abilityName.lightSlam:

                    animator.SetTrigger("ReadyTailSlam");
                    animator.SetBool("TailSlamming", true);
                    SlammingTail.transform.position = new Vector2(Player.transform.position.x, 20.0f);
                    print(SlammingTail.transform.position);
                    TailAnimator.SetTrigger("TailSlam");
                    StartCoroutine(AttackCooldown(3));
                    animator.SetBool("TailSlamming", false);

                    break;

                // ABILITY 2 // ------------------------------
                case abilityName.heavySlam:
                    SlammingTail.transform.position = new Vector2(SlamReferenceLocation.transform.position.x, 20.0f);
                    animator.SetTrigger("ReadyTailSlam");
                    animator.SetBool("TailSlamming", true);
                    TailAnimator.SetTrigger("HeavySlam");

                    StartCoroutine(AttackCooldown(5));
                    animator.SetBool("TailSlamming", false);

                break; 
                case abilityName.cannon:
                break; 
            }
        }else {
            print("Currently Attacking, unable to perform ability");
            return;
        }
    }

    IEnumerator AttackCooldown(float delayTimer){
        print("Delay initiated for "+ delayTimer);
        yield return new WaitForSeconds(delayTimer);
        isAttacking = false;
    }

    IEnumerator DelayedAttack(string trigger, float delayTimer){
        yield return new WaitForSeconds(delayTimer);
        animator.SetTrigger(trigger);
    }

    void SetReferenceLocation(GameObject reference){
        reference.transform.position = new Vector2(Player.transform.position.x, reference.transform.position.y);
        }
}
