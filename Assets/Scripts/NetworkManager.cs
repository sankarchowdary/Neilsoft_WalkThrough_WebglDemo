using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks , IMatchmakingCallbacks
{
    public static NetworkManager instance;

    //public int noOfPlayers;
    public string myname;
    public InputField name_IF;

    public GameObject PrefabPlayer;
    public GameObject Root;
    public GameObject CurrentPlayer;
    public bool ismaster;
    public List<string> PlayersInRoom;
    public bool isConnecting;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            PhotonNetwork.AutomaticallySyncScene = true;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        Connect();
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
            return;
        Debug.Log("Connecting ....");
        isConnecting = true;

        Disconnect();
        PhotonNetwork.GameVersion = "1.0";
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public void JoinRandomRoom()
    {
        //if (name_IF.text.Length < 2)
        //    return;

        if (PhotonNetwork.IsConnected)
            PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        if (isConnecting)
        {
            PhotonNetwork.NickName = "Player" + Random.Range(0, 10000).ToString();
            myname = PhotonNetwork.NickName;
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed " + returnCode + "  " + message);
        byte maxPlayersPerRoom = 2;
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        isConnecting = false;
    }

    public override void OnJoinedRoom()
    {
        ismaster = PhotonNetwork.IsMasterClient;
        if(ismaster)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.Log("OnPlayerEnteredRoom() " + other.NickName); // not seen if you're the player connecting
        updatePlayerInRoom();
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.Log("OnPlayerLeftRoom() " + other.NickName); // seen when other disconnects

        if (PhotonNetwork.IsMasterClient)
        {
            // if player with turn left the room we select next player to play the game
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
            string nextturnName = PhotonNetwork.CurrentRoom.CustomProperties["nextturn"].ToString();
            if (other.NickName == nextturnName)
            {
                Debug.Log("player with turn left the room");
                for (int i = 0; i < PlayersInRoom.Count; i++)
                    if (PlayersInRoom[i] == other.NickName)
                    {
                        SetNextPlayer();
                    }
            }
        }
        updatePlayerInRoom();
    }

    public void SetNextPlayer(int playerid = -1)
    {
        string str = (playerid == -1) ? NextInList(PlayersInRoom, PhotonNetwork.CurrentRoom.CustomProperties["nextturn"].ToString())
            : PlayersInRoom[playerid];
        Hashtable props = new Hashtable { { "nextturn", str } };
       // PhotonNetwork.CurrentRoom.SetCustomProperties(props);
        Debug.Log(PhotonNetwork.CurrentRoom.ToStringFull());
    }

    public string NextInList(List<string> list, string Current)
    {
        int currentid = 0;
        for (int i = 0; i < list.Count; i++)
            if (list[i] == Current) currentid = i;

        currentid++;
        if (currentid >= list.Count)
            currentid = 0;

        return list[currentid];
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("OnMasterClientSwitched");
        ismaster = PhotonNetwork.IsMasterClient;
        string nextturnName = PhotonNetwork.CurrentRoom.CustomProperties["nextturn"].ToString();
        if (!PlayersInRoom.Contains(nextturnName))
            SetNextPlayer(0);
    }

    public override void OnLeftRoom()
    {

    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void QuitApplication()
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }

    public void updatePlayerInRoom()
    {
        PlayersInRoom.Clear();
        for (int i = 0; i < 200; i++)
        {
            Photon.Realtime.Player TempPlayer;
            if (PhotonNetwork.CurrentRoom.Players.TryGetValue(i, out TempPlayer))
            {
                PlayersInRoom.Add(TempPlayer.NickName);
            }
            if (PlayersInRoom.Count == PhotonNetwork.CurrentRoom.PlayerCount)
                break;
        }
    }
}
