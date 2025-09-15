using UnityEngine;

public class EnableDoorBlock : MonoBehaviour
{
    public BoxCollider targetBox;   
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        if (targetBox != null)
            targetBox.enabled = true;
        else
            Debug.LogWarning(name + ": No target BoxCollider assigned!");
    }

}
