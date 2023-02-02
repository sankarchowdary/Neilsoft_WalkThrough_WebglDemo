using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Uimanager : MonoBehaviour
{
    public static Uimanager instance;
    //public GameObject[] SpawnPoints;
    public Text PingText;
    public List<Button> OnConnectEnable;
    public Image Isconnectiong;
    private Vector3 ConnectingImageSpeed=new Vector3(0,0,-1);

    void Start()
    {
        instance = this;
        foreach (var item in OnConnectEnable)
            item.interactable = false;
        StartCoroutine(GetPing());
        Isconnectiong.gameObject.SetActive(false);
    }

    void Update()
    {        
        Isconnectiong.transform.Rotate(ConnectingImageSpeed);
    }

    IEnumerator GetPing()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if (PhotonNetwork.IsConnected)
                PingText.text = PhotonNetwork.GetPing().ToString();
            else
                PingText.text = "--";
        }
    }

    public void ConnectedTomasterServer()
    {
        foreach (var item in OnConnectEnable)
            item.interactable = true;

        Isconnectiong.gameObject.SetActive(false);
    }

    public void DisConnectedTomasterServer()
    {
        foreach (var item in OnConnectEnable)
            item.interactable = false;
        //Isconnectiong.gameObject.SetActive(true);
    }

    public void OnConnecting()
    {
        Isconnectiong.gameObject.SetActive(true);
    }
}