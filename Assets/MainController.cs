using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public Animator PanelArrow;
    public GameObject PDFReader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClickArrowUp()
    {
       PanelArrow.Play("PanelAnimaUp");
    }
    public void OnClickArrowDown()
    {
        PanelArrow.Play("PanelAnimaDown");
    }
    // Update is called once per frame
   public void PdfClose()
    {
        PDFReader.transform.localScale = new Vector3(0,0,0);
        
    }
   public void PdfOpen()
    {

        PDFReader.transform.localScale = new Vector3(1f,1f,1f);
    }




}
