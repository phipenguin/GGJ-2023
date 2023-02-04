using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public Vector3 relativeFireDirection;
    public float speed;
    public LayerMask mask;
    public string objectHitTag;
    public UnityEvent onTagHit;
    public UnityEvent onOtherHit;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        rb.MovePosition(transform.position + transform.TransformDirection(relativeFireDirection) * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag(objectHitTag)) {
            onTagHit.Invoke();
        } else {
            onOtherHit.Invoke();
        }
    }

    public void DestroyBullet() {
        Destroy(gameObject);
    }
}
