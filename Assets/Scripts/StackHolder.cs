using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StackHolder : MonoBehaviour
{
    public Transform placeStacks;
    public List<GameObject> stackObjs;

    private float yPos = 0;

    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Stack") && GameManager.instance.playerController.ispicked)
        {
            //GameManager.instance.playerController.ispicked = false;
            //GameObject dropedObj = other.gameObject;
            //stackObjs.Add(dropedObj);            
            //dropedObj.transform.SetParent(placeStacks);                       
            //dropedObj.transform.localPosition = new Vector3(0, yPos ,0);
            //yPos += 0.15f;
            //dropedObj.transform.rotation = Quaternion.identity;
            //dropedObj.GetComponent<BoxCollider>().enabled = false;

            //GameManager.instance.stacCount_Txt.text = "Counts : " + stackObjs.Count;

            photonView.RPC("HoldObjs_RPC", RpcTarget.All, other.gameObject.name);
        }
    }

    [PunRPC]
    void HoldObjs_RPC(string objName)
    {
        GameManager.instance.playerController.ispicked = false;
        GameObject dropedObj = GameObject.Find(objName);
        stackObjs.Add(dropedObj);
        dropedObj.transform.SetParent(placeStacks);
        dropedObj.transform.localPosition = new Vector3(0, yPos, 0);
        yPos += 0.15f;
        dropedObj.transform.rotation = Quaternion.identity;
        dropedObj.GetComponent<BoxCollider>().enabled = false;

        GameManager.instance.stacCount_Txt.text = "Counts : " + stackObjs.Count;
    }
}
