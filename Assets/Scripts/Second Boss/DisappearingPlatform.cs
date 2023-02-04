using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisappearingPlatform : MonoBehaviour
{
    public UnityEvent onDisappear;
    public UnityEvent onAppear;

    private Collider col;

    public void Appear() {
        col.enabled = true;
        onAppear.Invoke();
    }

    public void Disappear() {
        col.enabled = false;
        onDisappear.Invoke();
    }
}
