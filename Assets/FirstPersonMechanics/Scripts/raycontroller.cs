using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycontroller : MonoBehaviour
{
   
    public float range = 50;
    public Material mat1,mat2,mat3;
    public GameObject materialobject,parenton;


    void Start()
    {
       
    }

    void Update()
    {
        Vector3 direction = Vector3.forward;
        Ray theray = new Ray(transform.position, transform.TransformDirection(direction * range));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * range));
        if (Physics.Raycast(theray, out RaycastHit hit, range))
        {

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.tag == "stop")
                {
                    parenton.SetActive(true);
                }

                if (hit.collider.tag == "blue")
                {
                    materialobject.GetComponent<Renderer>().material = mat1;
                }
                if (hit.collider.tag == "pink")
                {
                    materialobject.GetComponent<Renderer>().material = mat2;
                }
                if (hit.collider.tag == "red")
                {
                    materialobject.GetComponent<Renderer>().material = mat3;
                }

            }
            //// 
            // Move this object to the position clicked by the mouse.
        }
    }
}
