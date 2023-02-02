using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractionRayCaster))]
public class CursorController : MonoBehaviour {
    public bool Openover,closeover;
    public static CursorController Instance;
    // Use this for initialization
    void Start () {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
       
        Instance = this;
        
    }

    // Update is called once per frame
    void Update () {
        //Unlock Cursor
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetButtonDown("Fire1") && Cursor.visible == true)
        {
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
            //CameraController.Instance.camMoved = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            CameraController.Instance.camMoved = true;
            //print("cursor");
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;

        }
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            CameraController.Instance.camMoved = false;

            //print("cursor");
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;

        }

    }

    void ChangeCursor()
    {
        throw new NotImplementedException();
    }

    void HideCursor()
    {
        Cursor.visible = false;
    }

    public void ChangeCursorIcon(Texture2D texture)
    {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
