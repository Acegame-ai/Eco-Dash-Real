using System.Collections;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 17f;
    
    TrashMove trashMoveScript;

    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        trashMoveScript = gameObject.GetComponent<TrashMove>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin Detector")
        {
            trashMoveScript.enabled = true;
        }
    }
}
