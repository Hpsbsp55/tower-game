using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactive : MonoBehaviour
{
    public abstract void Interact();
}

public class ObjectPickup : MonoBehaviour
{
    public Camera Camera;
    public Transform handLocation;
    [SerializeField] Material highlight;
    public bool holdingObj = false;
    int reachDist = 5;
    Ray ray;
    RaycastHit hitInfo;
    Material defaultMat;
    GameObject pickup;
    GameObject heldObj;

    private void Update()
    {
        ReachDist();
        InteractCheck();
        if (holdingObj)
        {
            PutDownCheck();
        }
    }

    public void ReachDist()
    {
        ray = Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
    }
    void InteractCheck()
    {
        bool inReach = Physics.Raycast(ray, out hitInfo, reachDist);
        GameObject temp = pickup;
        if (inReach && (hitInfo.transform.gameObject.tag == "Pickup" || hitInfo.transform.gameObject.tag == "Interactive"))
        {
            if (pickup != null && hitInfo.transform.gameObject != pickup)
            {
                foreach (Pickup childPickup in pickup.GetComponentsInChildren<Pickup>())
                {
                    childPickup.DefaultMat();
                }
            }
            pickup = hitInfo.transform.gameObject;
            foreach (Pickup childPickup in pickup.GetComponentsInChildren<Pickup>())
            {
                childPickup.HighlightMat();
            }
        }
        else
            {
            if(pickup != null)
            {
                foreach (Pickup childPickup in pickup.GetComponentsInChildren<Pickup>())
                {
                    childPickup.DefaultMat();
                }
            }
            pickup = null;
        }
        if (pickup != null && !holdingObj && Input.GetKeyDown(KeyCode.E) && inReach && pickup.tag == "Pickup")
        {
            pickup.transform.parent = handLocation;
            pickup.GetComponent<Collider>().isTrigger = true;
            pickup.GetComponent<Rigidbody>().isKinematic = true;
            pickup.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            pickup.transform.localPosition = new Vector3(0, 0, 0);
            heldObj = pickup;
            holdingObj = true;
        } 
        else if(pickup != null && Input.GetKeyDown(KeyCode.Mouse0) && inReach && pickup.tag == "Interactive")
        {
            pickup.GetComponent<Interactive>().Interact();
        }
    }

    void PutDownCheck()
    {
        ray = Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (holdingObj && Input.GetKeyDown(KeyCode.F) && Physics.Raycast(ray, out hit, reachDist) )
        {
            holdingObj = false;
            heldObj.transform.parent = null;
            heldObj.transform.position = hit.point;
            heldObj.transform.up = hit.normal;
            heldObj.transform.localPosition += Vector3.up * 0.5f * heldObj.transform.lossyScale.y;
            heldObj.GetComponent<Collider>().isTrigger = false;
            heldObj.GetComponent<Rigidbody>().isKinematic = false;
        }
        else if(holdingObj && Input.GetKeyDown(KeyCode.F) && !Physics.Raycast(ray, out hit, reachDist))
        {
            holdingObj = false;
            heldObj.transform.parent = null;
            heldObj.transform.position = Camera.transform.position + Camera.transform.forward * reachDist;
            heldObj.GetComponent<Collider>().isTrigger = false;
            heldObj.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
