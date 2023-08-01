using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactive
{
    private bool opening = false;
    private float t = 0f;
    private Transform box;
    private Transform lid;
    private Bounds bounds;
    private float openSpeed = 30;
    // Start is called before the first frame update
    void Start()
    {
        lid = transform.Find("ChestLid");
        box = transform.Find("ChestBox");
        bounds = box.gameObject.GetComponent<Collider>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        Open();
    }
    public override void Interact() {
        opening = true;
    }
    void Open() {
        //assuming observer is facing positive Z and the front of the chest is facing negative Z
        if(opening && t <= 3f) {
            lid.RotateAround(new Vector3(box.position.x, box.position.y + bounds.size.y / 2, box.position.z + bounds.size.z / 2), transform.right, openSpeed * Time.deltaTime);
            t += Time.deltaTime;
        } else if(opening) {
            opening = !opening;
            openSpeed = -openSpeed;
            t = 0f;
        }
    }
}
