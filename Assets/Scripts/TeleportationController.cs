using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationController : MonoBehaviour
{
    public static TeleportationController Instance;
    [SerializeField] GameObject TP01;
    [SerializeField] GameObject TP02;
    [SerializeField] GameObject TP03;
    [SerializeField] GameObject TP04;
    public GameObject MainPlayer;
    public Animator DoorAnimation;
    Vector3 TP01Location;
    Vector3 TP02Location;
    Vector3 TP03Location;
    Vector3 TP04Location;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        TP01Location = TP01.transform.position;
        TP02Location = TP01.transform.position;
        TP03Location = TP01.transform.position;
        TP04Location = TP04.transform.position;
        Invoke("attachplayer", 2f);
    }
    public  void attachplayer()
    {
        MainPlayer = GameObject.Find("FPSController(Clone)");
    }   
   
    public void TeleportOurPlayer(Vector3 tplocation)
    {
      //  MainPlayer.transform.position = tplocation;
        Debug.Log(tplocation + "TELEPORT!!!");
    }
    // Update is called once per frame
    public void Player1()
    {
        TeleportOurPlayer(TP01Location);
        MainPlayer.transform.localPosition = new Vector3(TP01.transform.localPosition.x, TP01.transform.localPosition.y, TP01.transform.localPosition.z);
        MainPlayer.transform.eulerAngles = new Vector3(TP01.transform.localEulerAngles.x, 0, TP01.transform.localEulerAngles.z);
        
    }
    public void Player2()
    {
        
        MainPlayer.transform.localPosition = new Vector3(TP02.transform.localPosition.x, TP02.transform.localPosition.y, TP02.transform.localPosition.z);
        MainPlayer.transform.eulerAngles = new Vector3(TP02.transform.localEulerAngles.x, -90, TP02.transform.localEulerAngles.z);
    }
    public void Player3()
    {
        
        MainPlayer.transform.localPosition = new Vector3(TP03.transform.localPosition.x, TP03.transform.localPosition.y, TP03.transform.localPosition.z);
        MainPlayer.transform.eulerAngles = new Vector3(TP03.transform.localEulerAngles.x, 180, TP03.transform.localEulerAngles.z);
    }
    public void Player4()
    {

        MainPlayer.transform.localPosition = new Vector3(TP04.transform.localPosition.x, TP04.transform.localPosition.y, TP04.transform.localPosition.z);
        MainPlayer.transform.eulerAngles = new Vector3(TP04.transform.localEulerAngles.x, 180, TP04.transform.localEulerAngles.z);
    }
   public void AnimationDoorClick()
    {

        DoorAnimation.Play("DoorAnima");
    }
}
