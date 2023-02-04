using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Script : Entity
{

    [SerializeField] private TurretBehavior[] turretList;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        RestoreHP();
    }

    // Update is called once per frame
    void Update()
    {
        //if(plant seen) behavior to go out of screen then beeline to plant
        //else
        switch(Random.Range(0,2)){
            case 1:
            case 2:
                lookForTurret();
                break;
            case 3:
                //Attack Player
                break;
        }
    }

    void lookForTurret(){
        
        int x = 0;
        for(x = 0 ; x < turretList.Length; x++){
            if(!turretList[x].isAlive) break;
        }
            //Move to x turret and repair
            //turretList[x].RestoreHP();
    }

    bool AllTurretsUpCheck(){
        foreach(TurretBehavior turret in turretList){
            if(!turret.isAlive) return false;
        }
        return true;
    }
}
