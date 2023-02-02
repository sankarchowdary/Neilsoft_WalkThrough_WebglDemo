using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickEventss : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) { 
            Debug.Log("Right button pressed.");
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Left button pressed.");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
