using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class UISignin : MonoBehaviour
{
    string username, password, emailAddress;
    public GameObject LoginMenu;
    [SerializeField] Text errorText;
    public Text LoginName;
    public static UISignin Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        UserAccountManager.OnsignInFail.AddListener(OnsigninsucessFail);
        UserAccountManager.OnsignInSucess.AddListener(Onsigninsucess);
    }
    private void OnDisable()
    {
        UserAccountManager.OnsignInSucess.RemoveListener(Onsigninsucess);
        UserAccountManager.OnsignInFail.RemoveListener(OnsigninsucessFail);
    }
    void Onsigninsucess()
    {
        NetworkManager.instance.JoinRandomRoom();
        print("sucess");
        errorText.text = null;
       // Stats.SetActive(true);
        LoginMenu.SetActive(false);
      //  Application.LoadLevel("Demo");
       
    }

  
    void OnsigninsucessFail(string error)
    {
        errorText.text = error;
    }
    // Start is called before the first frame update

    public void UpdateUsername(string _username)
    {
        username = _username;
    }
    public void UpdatePassword(string _password)
    {
        password = _password;
    }
    public void Signin()
    {
        print("shankar");
        UserAccountManager.Instance.SignIn(username, password);
    }
}
