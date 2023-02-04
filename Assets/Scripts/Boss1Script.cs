using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum abilityName{ lightSlam, heavySlam, cannon};
public class Boss1Script : Entity
{
    [Header("Slam Attributes")]
    [SerializeField] private GameObject ReferenceLocation;
    [SerializeField] private GameObject SlammingTail;
    [SerializeField] private int slamRepeatChance = 3;

    private bool isAttacking;
    [SerializeField]GameObject Player;
    Animator animator;
    void Start(){
        animator = GetComponent<Animator>();
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
                    //SetReferenceLocation(SlamReferenceLocation);
                    ReferenceLocation.transform.position = new Vector2(Player.transform.position.x, ReferenceLocation.transform.position.y);
                    //SlammingTail.transform.position = new Vector2(ReferenceLocation.transform.position.x, ReferenceLocation.transform.position.y);
                    SlammingTail.transform.position = new Vector2(Player.transform.position.x, SlammingTail.transform.position.y);
                    animator.SetTrigger("TailSlam");


                    if(Random.Range (0,10) <= slamRepeatChance){
                        //SetReferenceLocation(SlamReferenceLocation);
                        ReferenceLocation.transform.position = new Vector2(Player.transform.position.x, ReferenceLocation.transform.position.y);  
                        SlammingTail.transform.position = new Vector2(ReferenceLocation.transform.position.x, ReferenceLocation.transform.position.y);
                        animator.SetTrigger("TailSlam");
                        print("Another Slam");
                    }
                        StartCoroutine(AttackCooldown(3));
                        animator.SetBool("TailSlamming", false);

                    break;

                // ABILITY 2 // ------------------------------
                case abilityName.heavySlam:
                    
                    animator.SetTrigger("ReadyTailSlam");
                    animator.SetBool("TailSlamming", true);
                    animator.SetTrigger("HeavySlam");

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
