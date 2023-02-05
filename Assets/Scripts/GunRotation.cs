using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public void GunLeft()
    {
        transform.eulerAngles = new Vector3(0, 0, 180.0f);
    }

    public void GunRight()
    {
        transform.eulerAngles = Vector3.zero;
    }

    public void GunUp()
    {
        transform.eulerAngles = new Vector3(0, 0, 90.0f);
    }
}
