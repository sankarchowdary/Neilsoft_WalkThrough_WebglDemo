using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;

public class player : MonoBehaviourPunCallbacks, IPunObservable, IPunInstantiateMagicCallback
{
    public string playername;
    PhotonView pv;
    public Text UIPlayername;
    public Text PlayerLastAnswer;
    public InputField Uiinput;
    public Button SendButton;
    public Image TurnLight;
    public bool lightState;
    public List<string> PlayersInRoom;
    
    void Start()
    {
        pv = GetComponent<PhotonView>();
        this.gameObject.transform.SetParent(
        GameObject.FindObjectOfType<Canvas>().transform);
        playername = PhotonNetwork.NickName;

        UIPlayername.text = pv.Owner.NickName;

        gameObject.name += pv.IsMine ? "(local)" : "";
        if (pv.IsMine)
        {
            //this.gameObject.GetComponent<RectTransform>().anchoredPosition
            //    = FindObjectOfType<Uimanager>().SpawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1].
            //    GetComponent<RectTransform>().anchoredPosition;
        }

        lightState = false;
        if (PhotonNetwork.IsMasterClient && pv.IsMine)
        {
            NetworkManager.instance.SetNextPlayer(0);
        }
        Interactable(false);

        TurnLight.color = lightState ? Color.green : Color.red;

        //
        Debug.Log(PhotonNetwork.CurrentRoom.ToStringFull());
        for (int i = 0; i < PhotonNetwork.CurrentRoom.MaxPlayers; i++)
        {
            Photon.Realtime.Player TempPlayer;
            if (PhotonNetwork.CurrentRoom.Players.TryGetValue(i, out TempPlayer))
            {
                PlayersInRoom.Add(TempPlayer.NickName);
            }
        }
    }

    public void Interactable(bool state)
    {
        Uiinput.interactable = state;
        SendButton.interactable = state;
    }

    public void SendAndNextTurn()
    {
        PlayerLastAnswer.text = Uiinput.text;
        pv.RPC("RPCSendAndNextTurn", RpcTarget.All);
    }

    [PunRPC]
    void RPCSendAndNextTurn()
    {
        Debug.Log("RPCSendAndNextTurn 0", gameObject);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("RPCSendAndNextTurn on" + this.gameObject.name, gameObject);
            NetworkManager.instance.SetNextPlayer();
        }
    }


    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        string nextplayerTurnNickName = (string)propertiesThatChanged["nextturn"];
        Debug.Log("OnRoomPropertiesUpdate nextplayerTurnNickName=" +
            nextplayerTurnNickName + "   objName=" + this.gameObject.name, gameObject);
        Debug.Log(PhotonNetwork.CurrentRoom.ToStringFull());
        lightState = (pv.Owner.NickName == nextplayerTurnNickName);
        TurnLight.color = lightState ? Color.green : Color.red;

        if (!pv.IsMine) return;
        if (pv.Owner.NickName == nextplayerTurnNickName)
        {
            Debug.Log("Myturn");
            Interactable(true);
        }
        else
        {
            Interactable(false);
        }

        if ((nextplayerTurnNickName == null) && pv.IsMine && PhotonNetwork.IsMasterClient)
        {
            Interactable(true);
        }
    }
    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(PlayerLastAnswer.text);
            stream.SendNext(lightState);
        }
        else
        {
            PlayerLastAnswer.text = (string)stream.ReceiveNext();
            lightState = (bool)stream.ReceiveNext();
            TurnLight.color = lightState ? Color.green : Color.red;
        }
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
        {
            gameObject.name = pv.Owner.NickName;
        }
    }

    #endregion
}
