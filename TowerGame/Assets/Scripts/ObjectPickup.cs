using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public Camera Camera;
    public Transform handLocation;
    GameObject heldItem;
    bool holdingObj = false;
    int reachDist = 3;
    Ray ray;
    RaycastHit hitInfo;

    private void Update()
    {
        ReachDist();
        PickupCheck();
        if (holdingObj)
        {
            PutDownCheck();
        }
    }

    public void ReachDist()
    {
        ray = Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
    }
    void PickupCheck()
    {
        if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(ray, out hitInfo, reachDist) && hitInfo.transform.gameObject.tag == "Pickup")
        {
            heldItem = hitInfo.transform.gameObject;
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
        ray = Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //int maxDist = Mathf.Max(reachDist, )
        RaycastHit hit;
        if (holdingObj && Input.GetKeyDown(KeyCode.F) && Physics.Raycast(ray, out hit, reachDist))
        {
            holdingObj = false;
            heldItem.transform.parent = null;
            heldItem.transform.position = hit.point;
            heldItem.transform.up = hit.normal;
            heldItem.transform.localPosition += Vector3.up * 0.5f * heldItem.transform.lossyScale.y;
            heldItem.GetComponent<Collider>().isTrigger = false;
            heldItem.GetComponent<Rigidbody>().isKinematic = false;
        }
        else if(holdingObj && Input.GetKeyDown(KeyCode.F) && !Physics.Raycast(ray, out hit, reachDist))
        {
            holdingObj = false;
            heldItem.transform.parent = null;
            heldItem.transform.position = Camera.transform.position + Camera.transform.forward * reachDist;
            heldItem.GetComponent<Collider>().isTrigger = false;
            heldItem.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
