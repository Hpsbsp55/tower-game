using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfButton : Interactive
{
    [SerializeField] GameObject shelf;
    bool pressed = false;
    public override void Interact()
    {
        StartCoroutine(ButtonPress());
    }

    private IEnumerator ButtonPress()
    {
        if (!pressed)
        {
            pressed = true;
            for (float i = 1; i >= 0; i -= 0.01f)
            {
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(0.767f, 0.86684f, i), transform.position.z);
                yield return new WaitForSeconds(0.005f);
            }
        }
        shelf.GetComponent<ShelfRotation>().OpenShelf();
    }
}
