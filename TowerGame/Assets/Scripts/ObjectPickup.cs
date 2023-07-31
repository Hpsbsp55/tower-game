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
    GameObject heldItem;
    bool holdingObj = false;
    int reachDist = 5;
    Ray ray;
    RaycastHit hitInfo;
    Material defaultMat;
    GameObject pickup;

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
        if (inReach && (hitInfo.transform.gameObject.tag == "Pickup" || hitInfo.transform.gameObject.tag == "Interactive"))
        {
            pickup = hitInfo.transform.gameObject;
            pickup.GetComponent<Pickup>().HighlightMat();
        }
        else
        {
            if(pickup != null)
            {
                pickup.GetComponent<Pickup>().DefaultMat();
            }
            pickup = null;
        }

        if (Input.GetKeyDown(KeyCode.E) && inReach && pickup.tag == "Pickup")
        {
            heldItem = hitInfo.transform.gameObject;
            pickup.transform.parent = handLocation;
            pickup.GetComponent<Collider>().isTrigger = true;
            pickup.GetComponent<Rigidbody>().isKinematic = true;
            pickup.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            pickup.transform.localPosition = new Vector3(0, 0, 0);
            holdingObj = true;
        } 
        else if(Input.GetKeyDown(KeyCode.Mouse0) && inReach && pickup.tag == "Interactive")
        {
            pickup.GetComponent<Interactive>().Interact();
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
