using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    Material defaultMat;
    [SerializeField] Material highlight;
    [SerializeField] GameObject player;
    private void Start()
    {
        defaultMat = GetComponent<MeshRenderer>().material;
    }
    public void DefaultMat()
    {
        gameObject.GetComponent<MeshRenderer>().material = defaultMat;
    }

    public void HighlightMat()
    {
        gameObject.GetComponent<MeshRenderer>().material = highlight;
    }
    /*void OnTriggerEnter(Collider other) {
        if(!player.GetComponent<ObjectPickup>().holdingObj && other.gameObject != player) {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    // }*/
}
