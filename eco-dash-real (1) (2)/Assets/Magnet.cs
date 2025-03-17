using System.Collections;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public GameObject coinDetectorObj; // Can be assigned manually in the Inspector

    void Start()
    {
       coinDetectorObj = GameObject.FindGameObjectWithTag("Coin Detector");
       //coinDetectorObj.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Player" && coinDetectorObj != null)
        {
            StartCoroutine(ActivateCoin());
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    IEnumerator ActivateCoin()
    {
        coinDetectorObj.SetActive(true);
        yield return new WaitForSeconds(10f);
        coinDetectorObj.SetActive(false);

    }
}
