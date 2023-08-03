using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pedestal : MonoBehaviour {
    public static bool[] pedestalsActivated = new bool[5] {false, false, false, false, false};
    [SerializeField] GameObject[] items = new GameObject[5];
    [SerializeField] bool activated = false;
    [SerializeField] int index;
    private GameObject item;
    [SerializeField] GameObject player;
    [SerializeField] GameObject cauldron;
    bool win = false;
    void Start() {
    }
    void Update() {
        if (!win)
        {
            CheckPedestals();
        }
        RotateItem();
        //for (int n = 0; n < 5; n++)
        //{
        //    Debug.Log(pedestalsActivated[n]);
        //}
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
            Debug.Log(pedestalsActivated);
        }
    }
    void CheckPedestals() {
        if (index == 0 && pedestalsActivated.ToList().All(v => v == true))
        {
            Debug.Log("Finished");
            win = true;
            Instantiate(cauldron, Vector3.zero, Quaternion.identity);
        }
    }
    void RotateItem() {
        if(item != null) {
            item.transform.Rotate(item.transform.rotation.eulerAngles.x, 60 * Time.deltaTime, item.transform.rotation.eulerAngles.z);
        }
    }
}