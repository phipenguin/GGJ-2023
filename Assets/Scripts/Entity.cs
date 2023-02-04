using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [SerializeField] protected float MaxHealth;
    [SerializeField] protected float currHealth;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = MaxHealth;
    }

    virtual public void RestoreHP(){
        this.currHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void DmgTaken(float value){

        currHealth -= value;
        Mathf.Clamp(value, 0, MaxHealth);
        
        /*
        if(currHealth > 0) animator.setTrigger("hurt");
        else if(currHealth <= 0) Die();
        */
    }

    virtual public void Die(){
        this.gameObject.SetActive(false);
    }
}
