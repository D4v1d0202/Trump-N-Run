using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingWallTrigger : MonoBehaviour
{
    bool isInDaWall = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "PlayerObj")
        {
            isInDaWall = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "PlayerObj")
        {
            isInDaWall = false;
        }
    }

    public bool GetIsInDaWall(){
        return isInDaWall;
    }
}
