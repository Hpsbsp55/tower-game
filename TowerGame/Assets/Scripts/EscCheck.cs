using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EscCheck : MonoBehaviour
{
    public GameObject canvas;
    private static EscCheck instance;
    public static EscCheck Instance => instance;
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //SceneManager.LoadScene(0);
            canvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
