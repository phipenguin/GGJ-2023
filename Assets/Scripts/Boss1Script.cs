using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum abilityName{ lightSlam, heavySlam, cannon};
public class Boss1Script : Entity
{
    [Header("References")]
    [SerializeField] private GameObject cannonReferenceObject;
    [SerializeField] private float cannonRange1 = -18;
    [SerializeField] private float cannonRange2 = 5;

    [SerializeField] private GameObject SlamReferenceLocation;
    [SerializeField] private GameObject SlammingTail;

    [Header("Attack details")]
    [SerializeField] private float plantTargetRate = 60.0f;
    [SerializeField] private float lightSlamCD = 2.0f;
    [SerializeField] private float heavySlamCD = 7.0f;
    [SerializeField] private float cannonCD = 5.0f;
    [SerializeField] private int maxProjectiles = 4;

    private bool isAttacking;
    [SerializeField]GameObject Player;
    [SerializeField]GameObject Plant;
    Animator animator;
    Animator TailAnimator;
    void Start(){
        animator = GetComponent<Animator>();
        TailAnimator = SlammingTail.GetComponentInChildren<Animator>(); 
        if(Player == null)
        Player = GameObject.FindGameObjectWithTag("Player");
        Plant = GameObject.FindGameObjectWithTag("Plant");
        RestoreHP();
    }

    void Update(){
        //Manual User input for Boss attacks
        /*
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
        */

        if(!isAttacking){
            attack((abilityName)Random.Range(0,3));
        }
        
    }

    void attack(abilityName ability){
        if(!isAttacking){
            isAttacking = true;
            GameObject target;
            if(Random.Range(0,100) < plantTargetRate){
                target = Plant;
            }
            else{
                target = Player;
            }
            switch(ability){

                // ABILITY 1 // ------------------------------
                case abilityName.lightSlam:

                    animator.SetTrigger("ReadyTailSlam");
                    animator.SetBool("TailSlamming", true);
                    SlammingTail.transform.position = new Vector2(Player.transform.position.x, 20.0f);
                    TailAnimator.SetTrigger("TailSlam");
                    StartCoroutine(TailSlamCooldown(lightSlamCD));
                    break;

                // ABILITY 2 // ------------------------------
                case abilityName.heavySlam:
                    SlammingTail.transform.position = new Vector2(SlamReferenceLocation.transform.position.x, 20.0f);
                    animator.SetTrigger("ReadyTailSlam");
                    animator.SetBool("TailSlamming", true);
                    TailAnimator.SetTrigger("HeavySlam");
                    StartCoroutine(TailSlamCooldown(heavySlamCD));

                break; 
                case abilityName.cannon:
                    int cannonCount = Random.Range(1 , maxProjectiles);
                    StartCoroutine(CannonCooldown(cannonCD, cannonCount));
                break; 
            }
        }else {
            print("Currently Attacking, unable to perform ability");
            return;
        }
    }

    IEnumerator TailSlamCooldown(float delayTimer){
        //print("Delay initiated for "+ delayTimer);
        yield return new WaitForSeconds(delayTimer);
        animator.SetBool("TailSlamming", false);
        isAttacking = false;
    }

    IEnumerator CannonCooldown(float delayTimer, int cannonCount){
        int x = 0;
        animator.SetTrigger("cannonThrow");
        while( x < cannonCount){
            x++;
            //print(x + " thrown");
            
            GameObject cannon = Instantiate(cannonReferenceObject, new Vector3(Random.Range(cannonRange1, cannonRange2), Random.Range(20.0f,25.0f) , 0.0f), Quaternion.identity);
            cannon.SetActive(true);
        }
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
