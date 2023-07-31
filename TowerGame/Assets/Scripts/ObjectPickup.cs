using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public Camera Camera;
    public Transform handLocation;
    bool holdingObj = false;
    int reachDist = 4;
    Ray ray;
    RaycastHit hitInfo;

    private void Start()
    {
        ray = Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
    }
    private void Update()
    {
        PickupCheck();
        if (holdingObj)
        {
            PutDownCheck();
        }
    }

    void PickupCheck()
    {
        if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(ray, out hitInfo, reachDist) && hitInfo.transform.gameObject.tag == "Pickup")
        {
            hitInfo.transform.parent = handLocation;
            hitInfo.collider.isTrigger = true;
            hitInfo.rigidbody.isKinematic = true;
            hitInfo.rigidbody.velocity = new Vector3(0, 0, 0);
            hitInfo.transform.localPosition = new Vector3(0, 0, 0);
            holdingObj = true;
        }
    }

    void PutDownCheck()
    {
        //int maxDist = Mathf.Max(reachDist, )
        //RaycastHit hit;
        //if (holdingObj && Input.GetKeyDown(KeyCode.E) && Physics.Raycast(ray, out hit, reachDist))
        //{
        //    holdingObj = false;
        //    hitInfo.transform.position = new Vector3(0, reachDist * Mathf.Cos(Camera.transform.localEulerAngles.y), reachDist * Mathf.Sin(Camera.transform.localEulerAngles.y));
        //    hitInfo.transform.parent = null;
        //    hitInfo.collider.isTrigger = false;
        //    hitInfo.rigidbody.isKinematic = false;
        //}
    }
}
