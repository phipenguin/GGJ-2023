using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Cannon : MonoBehaviour
{

    [SerializeField] private float lifetime = 10.0f;
    [SerializeField] private float timepassed = 0f;
    public string objectHitTag;
    public UnityEvent onTagHit;
    public UnityEvent onOtherHit;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(0,0);
    }

    void Update(){
        timepassed += Time.deltaTime;
        if(timepassed>= lifetime){
            Destroy(this);
        }
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision other){
        if (!string.IsNullOrEmpty(objectHitTag) && other.gameObject.CompareTag(objectHitTag)) {
            onTagHit.Invoke();
        } else {
            onOtherHit.Invoke();
        }
    }
    
    public void DestroySelf(){
        Destroy(this);
    }
}