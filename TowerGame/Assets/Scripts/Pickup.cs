using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    Material defaultMat;
    [SerializeField] Material highlight;
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
}
