using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageScript : MonoBehaviour
{

    GameObject Player;
    GameObject Plant;

    void Start(){
        Player = GameObject.FindWithTag("Player");
        Plant = GameObject.FindWithTag("Plant");

    }
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Player")){
            Player.GetComponent<PickupAbility>().OnPlant();
        }else if(collision.gameObject.CompareTag("Plant")){
            Plant.GetComponent<Health>().plantDamaged();
        }
    }

}
