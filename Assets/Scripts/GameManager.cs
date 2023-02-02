using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;

    public GameObject playerPrefab;
    public PlayerController1 playerController;
    public Transform ParentObject;

    public Text stacCount_Txt;
    
    private void Awake()
    {
      //  GameObject playerObj = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 1, 0), Quaternion.identity);
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            //DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 1, 0), Quaternion.identity);
        //  playerController = playerObj.GetComponent<PlayerController>();
       // playerObj.transform.SetParent(ParentObject);
      



    }
}
