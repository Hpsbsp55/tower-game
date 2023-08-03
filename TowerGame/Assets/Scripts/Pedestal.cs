using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pedestal : MonoBehaviour {
    public static bool[] pedestalsActivated = new bool[5] {false, false, false, false, false};
    [SerializeField] GameObject[] items = new GameObject[5];
    private bool activated = false;
    [SerializeField] int index;
    private GameObject item;
    [SerializeField] GameObject player;
    void Start() {
    }
    void Update() {
        CheckPedestals();
        RotateItem();
    }
    void OnTriggerEnter(Collider other) {
        if(!activated && items.Contains(other.gameObject)) {
            item = other.gameObject;
            item.transform.position = new Vector3(transform.position.x, transform.position.y + transform.Find("Teleport").position.y + 0.5f, transform.position.z);
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.transform.parent = this.transform;
            item.tag = "Untagged";
            player.GetComponent<ObjectPickup>().holdingObj = false;
            activated = true;
            pedestalsActivated[index] = true;
        }
    }
    void CheckPedestals() {
        if(index == 0 && pedestalsActivated == new bool[5] {true, true, true, true, true}) {
            //win
        }
    }
    void RotateItem() {
        if(item != null) {
            item.transform.Rotate(item.transform.rotation.eulerAngles.x, 90 * Time.deltaTime, item.transform.rotation.eulerAngles.z, Space.World);
        }
    }
}