using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Transform gunOrigin;
    public float timeBetweenShots;
    public GameObject bullet;
    public UnityEvent onAimUp;
    public UnityEvent onAimLeft;
    public UnityEvent onAimRight;
    public UnityEvent onShoot;

    public PickupAbility pickedPlant;
    private Vector2 aimDirection = Vector2.right;
    private bool canShoot = true;


    void Start(){
        pickedPlant = GameObject.FindObjectOfType<PickupAbility>();
    }
    public void OnMove(InputValue value) {
        aimDirection = value.Get<Vector2>();

        if (aimDirection.y > 0.5f) {
            onAimUp.Invoke();
        } else if (aimDirection.x > 0.5f) {
            onAimRight.Invoke();
        } else if (aimDirection.x < -0.5f) {
            onAimLeft.Invoke();
        }
    }

    IEnumerator shootCooldown() {
        canShoot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    public void OnRangeAtk() {
        if (canShoot && pickedPlant.canPlant) {
            Instantiate(bullet, transform.position, transform.rotation);
            StartCoroutine(shootCooldown());
        }
    }

    public void SetRotation(float angle) {
        gunOrigin.Rotate(new Vector3(0, 0, angle), Space.World);
    }
}
