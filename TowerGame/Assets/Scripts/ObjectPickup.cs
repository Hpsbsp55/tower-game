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
    bool holdingObj = false;
    int reachDist = 3;
    Ray ray;
    RaycastHit hitInfo;
    Material defaultMat;
    GameObject pickup;
    GameObject heldObj;
    [SerializeField] List<Transform> children;

    private void Start()
    {
        children = new List<Transform>();
    }
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
            if (pickup.GetComponent<InteractiveObj>().isActive == false)
            {
                pickup.GetComponent<InteractiveObj>().isActive = true;
                foreach (Transform child in pickup.transform)
                {
                    children.Add(child);
                    child.GetComponent<Pickup>().HighlightMat();
                }
                Debug.Log(children.Count);
                if(children.Count == 0)
                    pickup.GetComponent<Pickup>().HighlightMat();
            }
        }
        else
        {
            if(pickup != null && pickup.GetComponent<InteractiveObj>().isActive == true)
            {
                pickup.GetComponent<InteractiveObj>().isActive = false;
                foreach (Transform child in pickup.transform)
                {
                    if(child != pickup.transform)
                    {
                        child.GetComponent<Pickup>().DefaultMat();
                    }
                    //children.Add(child);
                }
                if (children.Count == 0)
                    pickup.GetComponent<Pickup>().DefaultMat();
                children.Clear();
            }
            //pickup = null;
        }

        if (Input.GetKeyDown(KeyCode.E) && inReach && pickup.tag == "Pickup")
        {
            pickup.transform.parent = handLocation;
            pickup.GetComponent<Collider>().isTrigger = true;
            pickup.GetComponent<Rigidbody>().isKinematic = true;
            pickup.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            pickup.transform.localPosition = new Vector3(0, 0, 0);
            heldObj = pickup;
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
        RaycastHit hit;
        if (holdingObj && Input.GetKeyDown(KeyCode.F) && Physics.Raycast(ray, out hit, reachDist) )
        {
            holdingObj = false;
            heldObj.transform.parent = null;
            heldObj.transform.position = hit.point;
            heldObj.transform.up = hit.normal;
            heldObj.transform.localPosition += Vector3.up * 0.5f * pickup.transform.lossyScale.y;
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
