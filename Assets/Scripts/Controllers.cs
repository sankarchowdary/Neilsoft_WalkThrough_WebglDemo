using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controllers : MonoBehaviour
{
    [SerializeField] float RotationSpeed = 5f;
    bool dragging = false;
    Rigidbody rb;
    public GameObject ModelParent,Model,parts;
    public Animator Heartanima;
    public float scale;

    public Renderer rend;
    //ui slider that you want to use to controll the transparency
    public Slider slider;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       // Model.transform.position = new Vector3(-0.058116f, 0.32f, -14.32f);
    }
     void OnMouseDrag()
    {
        dragging = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
    }
    void FixedUpdate()
    {
        if (dragging)
        {
            float x = Input.GetAxis("Mouse X") * RotationSpeed ;
           // float y = Input.GetAxis("Mouse Y") * RotationSpeed ;
            rb.AddTorque(Vector3.down * x);
            // rb.AddTorque(Vector3.right * y);
            // rb.AddForce(Vector3.left * y);
            dragging = false;
        }
    }
    public void OnUpdate(int speed)
    {
      ////  Model.transform.Rotate(0,speed,0);
       // dragging = false;
    }
    public void HeartAnimaClick()
    {
        Heartanima.Play("Heart");
    }
    public void ZoomPlus()
    {
        Model.transform.localScale+= new Vector3(scale, scale, scale);
    }
    public void ZoomMinus()
    {
        Model.transform.localScale-= new Vector3(scale, scale, scale);
    }
    public bool isTPMenabled;
    public void OnToggleClick()
    {
        if (isTPMenabled == false)
        {
            parts.SetActive(false);
            isTPMenabled = true;
        }
        else
        {
            parts.SetActive(true);
            isTPMenabled = false;
        }
    }

    void OnSliderChanged(float value)
    {
        rend.material.color = new Color(
            rend.material.color.r,
            rend.material.color.g,
            rend.material.color.b,
            slider.value
            );
    }
}
