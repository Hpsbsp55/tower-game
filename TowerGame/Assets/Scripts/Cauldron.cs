using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Cauldron : Interactive
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rise());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(2);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private IEnumerator Rise()
    {
        for (float i = 0; i <= 1; i += 0.01f)
        {
            transform.position = Vector3.up * Mathf.Lerp(0, 2.75f, i);
            yield return new WaitForSeconds(0.03f);
        }
    }

    public override void Interact()
    {
        throw new System.NotImplementedException();
    }
}
