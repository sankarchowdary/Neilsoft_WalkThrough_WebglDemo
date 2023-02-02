using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RaycastController : MonoBehaviour
{
    public bool OnOff, Hover;

    // public GameObject Textbutton,Textbox;
    public GameObject playbutton, pausebutton;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void ToStopButton()
    {
        playbutton.SetActive(false);
        pausebutton.SetActive(false);
    }
    void Update()
    {
       
       

        if (Input.GetMouseButtonDown(0))
                {
                    print("left");
                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                    {

                        Transform objecthit = hit.transform;
                        if (hit.transform.gameObject.tag == "VideoPlayer")
                        {
                            if (OnOff == true)
                            {
                                //Change Material
                                print("Sucess");
                                VideoController.Instance.OnPlay();
                                OnOff = false;
                                // Invoke("ToStopButton", 2f);
                            }
                            else
                            {
                                VideoController.Instance.OnStop();
                                OnOff = true;
                                //  Invoke("ToStopButton", 2f);
                            }
                        }
                        else if (hit.transform.gameObject.tag == "Stats")
                        {
                            PlafabController.Instance.GetStats();
                            PlafabController.Instance.SetStats();


                        }
                        else if (hit.transform.gameObject.tag == "Player1")
                        {
                            //Textbutton.SetActive(true);
                            //Textbox.SetActive(false);
                            TeleportationController.Instance.Player1();
                            Debug.Log("player1");

                        }
                        else if (hit.transform.gameObject.tag == "Player2")
                        {

                            TeleportationController.Instance.Player2();
                            Debug.Log("player2");
                        }
                        else if (hit.transform.gameObject.tag == "Player3")
                        {
                            Debug.Log("player3");

                            TeleportationController.Instance.Player3();
                        }
                        else if (hit.transform.gameObject.tag == "Player4")
                        {
                            Debug.Log("player4");

                            TeleportationController.Instance.Player4();
                        }
                        else if (hit.transform.gameObject.tag == "MainGate")
                        {
                         Debug.Log("MainGate");

                        TeleportationController.Instance.AnimationDoorClick();
                        }

                       

                else
                        {
                            print("Fail");

                        }
                    }
                }
            }

        }
  