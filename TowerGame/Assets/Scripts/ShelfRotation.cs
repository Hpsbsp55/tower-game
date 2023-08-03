using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfRotation : MonoBehaviour
{
    public void OpenShelf()
    {
        StartCoroutine(ShelfOpen());
    }

    private IEnumerator ShelfOpen()
    {
        for (int i = 0; i < 70; i++)
        {
            transform.rotation = Quaternion.AngleAxis(-i, Vector3.up);
            yield return new WaitForSeconds(.01f);
        }
    }
}
