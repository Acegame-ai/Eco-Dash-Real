using UnityEngine;

public class TrashMove : MonoBehaviour
{
    Trash trashScript;

    void Start()
    {
        trashScript = GetComponent<Trash>();
        enabled = false; // Disabled by default
    }

    void Update()
    {
        if (trashScript.playerTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, trashScript.playerTransform.position, trashScript.moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin Detector")
        {
            Destroy(gameObject);
        }
    }
}
