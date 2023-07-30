using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public Camera Camera;
    public Transform handLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        Ray ray = Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hitInfo;
        if (Input.GetKeyDown(KeyCode.E) && other.tag == "Pickup" && Physics.Raycast(ray, out hitInfo))
        {
            other.transform.parent = handLocation;
            other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            other.attachedRigidbody.velocity = new Vector3(0, 0, 0);
            other.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
