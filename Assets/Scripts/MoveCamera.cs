using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    //public float distanceX;
    //public float distanceY;

    void Update()
    {
        transform.position =
        cameraPosition.position;
        //+ cameraPosition.forward * (-1) * distanceX
        //+ cameraPosition.up * distanceY
    }
}
