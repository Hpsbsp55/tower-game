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
    float angle = 0;
    void Start() {
    }
    void Update() {
        if (!win)
        {
            CheckPedestals();
        }
        RotateItem();
        //string s = "";
        //for (int n = 0; n < 5; n++)
        //{
            //Debug.Log(pedestalsActivated[n]);
            //s += ", " + pedestalsActivated[n];
        //}
        //Debug.Log(s);
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
        if (index == 0 && pedestalsActivated.ToList().All(v => v == true))
        {
            win = true;
            Instantiate(cauldron, Vector3.zero, Quaternion.identity);
        }
    }
    void RotateItem() {
        if(item != null) {
            angle += 90 * Time.deltaTime;
            item.transform.rotation = Quaternion.AngleAxis(angle + 90 * Time.deltaTime,Vector3.up);
            //item.transform.Rotate(item.transform.rotation.eulerAngles.x, 90 * Time.deltaTime, item.transform.rotation.eulerAngles.z, Space.World);
        }
    }
}