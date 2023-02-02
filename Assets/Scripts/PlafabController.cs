using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;
using UnityEngine.UI;


public class PlafabController : MonoBehaviour
{
    public static PlafabController Instance;
    public static PlafabController PFC;
    private string userEmail;
    private string userPassword;
    private string username;
    public string playFabID;
    //public GameObject loginpanel;
    //public GameObject addloginpanel;
    public GameObject UpdateText;
    public InputField age, phonenumber, playerdamage, playerlevel, gamelevel;
    public int Age;
    public int Phonenumber;
    public int playerDamage;
    public int playerLevel;
    public int gameLevel;
   

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        // GetStats();
        phonenumber.text = Phonenumber.ToString();

        age.text = Age.ToString();
        gamelevel.text = gameLevel.ToString();
        playerdamage.text = playerDamage.ToString();
        playerlevel.text = playerLevel.ToString();
    }

    #region PlayerStats

    public void SetStats()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {

            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate {StatisticName = "playerLevel", Value = playerLevel},
            new StatisticUpdate {StatisticName = "gameLevel", Value = gameLevel},
            new StatisticUpdate {StatisticName = "playerDamage", Value = playerDamage},
            new StatisticUpdate {StatisticName = "Age", Value = Age},
            new StatisticUpdate {StatisticName = "Phonenumber", Value = Phonenumber},
            
            }
          
        },
        result => { Debug.Log("User statistics updated"); },
        error => { Debug.LogError(error.GenerateErrorReport()); });
        UpdateText.SetActive(true);
    }
    #endregion PlayerStats

   
    
   public void GetStats()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStats,
            error => Debug.LogError(error.GenerateErrorReport())
    );
          


    }
    void OnGetStats(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics) {
            Debug.Log("Statistic(" + eachStat.StatisticName + "):" + eachStat.Value);
            switch (eachStat.StatisticName)
            {
                case "PlayerLevel":
                    playerLevel = eachStat.Value;
                    break;
                case "Phonenumber":
                    Phonenumber = eachStat.Value;
                    break;
                case "Age":
                    Age = eachStat.Value;
                    break;
                case "playerDamage":
                    playerDamage = eachStat.Value;
                    break;
                case "gameLevel":
                    gameLevel = eachStat.Value;
                    break;
            }
        }
    }







private void OnEnable()
    {
        if(PlafabController.PFC == null)
        {
            PlafabController.PFC = this;
        }
        else
        {
            if (PlafabController.PFC != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    //private void OnLoginSucess(loginResult result)
    //{
    //    Debug.Log("Congratulations, you made your first successful API call!");
    //    PlayerPrefs.SetString("EMAIL", userEmail);
    //    PlayerPrefs.SetString("PASSWORD", userPassword);
    //    loginpanel.SetActive(false);
    //    recoverButton.SetActive(false);
    //}
    //private void OnRegisterFailure(playf)
    //{
    //  Debug.LogError(ColliderErrorState2D.)
    //}
    //public void GetUserEmail(string emailIn)
    //{
    //    userEmail = emailIn;
    //}
    //public void GetUserPassword(string passwordIn)
    //{
    //    userPassword = passwordIn;
    //}
    //public void GetUsername(string usernameIn)
    //{
    //    username = usernameIn;
    //}
    //public void OnClickLogin()
    //{
    //   // var requst = new login
    //}
    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
