using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public Camera Camera;
    public Transform handLocation;


    private void Update()
    {
        Ray ray = Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hitInfo;
        if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(ray, out hitInfo, 4) && hitInfo.transform.gameObject.tag == "Pickup")
        {
            hitInfo.transform.parent = handLocation;
            hitInfo.collider.isTrigger = true;
            hitInfo.rigidbody.isKinematic = true;
            hitInfo.rigidbody.velocity = new Vector3(0, 0, 0);
            hitInfo.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

}
