using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICreateAccount : MonoBehaviour
{
    [SerializeField] Text errorText;
    string username, password, emailAddress;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        UserAccountManager.OnCreateAccountFailed.AddListener(Creataccountfailed);
        UserAccountManager.OnsignInSucess.AddListener(OnsignSucess);
    }
    private void OnDisable()
    {
        UserAccountManager.OnCreateAccountFailed.RemoveListener(Creataccountfailed);
        UserAccountManager.OnsignInSucess.RemoveListener(OnsignSucess);
    }
    void Creataccountfailed(string error)
    {
        errorText.text = error;
    }
    void OnsignSucess()
    {
        print("signsucess");
        errorText.text = null;
    }
    void OnsignFail(string error)
    {
        errorText.text = error;
    }

    public void UpdatePassword(string _password)
    {
        password = _password;
    }
    public void UpdateEmailaddress(string _emailadress)
    {
        emailAddress = _emailadress;
    }
    public void UpdateUsername(string _username)
    {
        username = _username;
    }
    public void createAccount()
    {
        UserAccountManager.Instance.CreateAccount(username,emailAddress, password);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
