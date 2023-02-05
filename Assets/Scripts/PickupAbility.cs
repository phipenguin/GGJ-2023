using System.Collections;
using System.Collections.Generic;
using Micosmo.SensorToolkit;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class PickupAbility : MonoBehaviour
{
    public string objectTag;
    public Vector3 relativePutdownPosition;
    public UnityEvent onPickup;
    public UnityEvent onPutdown;
    private GameObject objectHeld;
    private GameObject objectTarget;
    private bool canPlant = false;

    void Awake() {
        Assert.IsFalse(string.IsNullOrEmpty(objectTag));
    }

    public void OnPlant() { 
        if (canPlant) {
            if (objectTarget) {
                objectHeld = objectTarget;
                objectTarget = null;
                objectHeld.SetActive(false);
                onPickup.Invoke();
            } else if (objectHeld) {
                objectHeld.transform.position = transform.position + relativePutdownPosition;
                objectHeld.SetActive(true);
                objectHeld = null;
                onPutdown.Invoke();
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(objectTag)) {
            objectTarget = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag(objectTag)) {
            objectTarget = null;
        }
    }

    public void setCanPlant(bool value) {
        canPlant = value;
    }
}
