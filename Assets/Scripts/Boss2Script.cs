using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Script : Entity
{

    [SerializeField] private TurretBehavior[] turretList;
    [SerializeField] float speed = 10.0f;
    [SerializeField]private GameObject target;
    private GameObject Plant;
    private GameObject Player;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Plant = GameObject.FindWithTag("Plant");
        Player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        RestoreHP();
    }

    // Update is called once per frame
    void Update()
    {
        ///Drop Target, go away from field then beeline for plant
        /*
        if(Plant.activeSelf){
            //target = Plant;
        }
        else target = null;
        */
        ///Otherwise Searech for new target, turrets having a higher priority
        if(target == null){

            switch(Random.Range(1,3)){
                case 1:
                case 2:
                    LookForTurret();
                    break;
                case 3:
                    //Attack Player
                    target = Player;
                    //Ignore for a new target afterwards
                    StartCoroutine(TargetNull(5));
                    break;
            }
        }
        ///Not Working move script
        Vector3.MoveTowards(this.gameObject.transform.position, target.transform.position, speed);

        ///Happens if target is turret and to be repaired, will stay for a while.
        if(target.GetComponent<TurretBehavior>() && Vector3.Distance(target.transform.position, this.transform.position) < 1.0f){
            target.GetComponent<TurretBehavior>().RestoreHP();
            StartCoroutine(TargetNull(8));
        }
    }

    void LookForTurret(){
        
        int x;

        if(AllTurretsUpCheck()) return;
        
        ///Search list for any broken turret then set as target
        for(x = 0 ; x < turretList.Length; x++){
            if(!turretList[x].isAlive) break;
        }
            //Move to x turret and repair
            target = turretList[x].gameObject;
            //turretList[x].RestoreHP();
    }

    bool AllTurretsUpCheck(){
        foreach(TurretBehavior turret in turretList){
            if(!turret.isAlive) return false;
        }
        return true;
    }

    IEnumerator TargetNull(float value){
        yield return new WaitForSeconds(value);
        target = null;
    }
}
