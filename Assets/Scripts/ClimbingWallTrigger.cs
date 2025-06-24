using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingWallTrigger : MonoBehaviour
{
    bool isInDaWall = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isInDaWall = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isInDaWall = false;
        }
    }

    public bool GetIsInDaWall(){
        return isInDaWall;
    }
}
