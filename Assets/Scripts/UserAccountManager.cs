using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;

public class UserAccountManager : MonoBehaviour
{
    public static UserAccountManager Instance;
    public static UnityEvent OnsignInSucess = new UnityEvent();
    public static UnityEvent<string> OnsignInFail = new UnityEvent<string>();
    public static UnityEvent<string> OnCreateAccountFailed = new UnityEvent<string>();


    public void Awake()
    {
        Instance = this;
    }
    public void CreateAccount(string username,string emailAddress,string password)
    {
        PlayFabClientAPI.RegisterPlayFabUser(
            new RegisterPlayFabUserRequest()
            {
                Email = emailAddress,
                Password = password,
                Username = username,
                RequireBothUsernameAndEmail = true
            },
            responce =>
            {
                Debug.Log($"Successful Acount Creation: {username},{emailAddress}");
                SignIn(username, password);
                
            },
            error =>
            {

                Debug.Log($"UnSuccessful Acount Creation: {username},{emailAddress}\n {error.ErrorMessage}");
                OnCreateAccountFailed.Invoke(error.ErrorMessage);
            }
    );

    }
    public void SignIn(string username, string password)
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest(){
            Username = username,
                Password = password
                },
       responce =>
       {
           Debug.Log($"Successful Acount Login: {username}");
           OnsignInSucess.Invoke();
       },
            error =>
            {

                Debug.Log($"UnSuccessful Acount Login: {username},\n {error.ErrorMessage}");
                OnsignInFail.Invoke(error.ErrorMessage);
            }
            
        );
    }
}
