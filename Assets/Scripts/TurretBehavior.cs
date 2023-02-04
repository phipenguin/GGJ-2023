using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : Entity
{

    GameObject Player;
    public bool isAlive  = false;

    [SerializeField] private float delayPerShot = 2.0f;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        currHealth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAttacking){
            //Shoot the player if alive. Will not shoot the plant
            StartCoroutine(ShotCooldown());
        }
    }

    IEnumerator ShotCooldown(){
        isAttacking = true;
        yield return new WaitForSeconds(delayPerShot);
        isAttacking = false;
    }

    public override void RestoreHP()
    {
        base.RestoreHP();
        isAlive = true;
    }
}
