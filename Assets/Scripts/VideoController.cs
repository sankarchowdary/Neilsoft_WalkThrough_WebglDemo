using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public static VideoController Instance;
    public string url;
    public VideoPlayer videplayer;
    public GameObject playbutton, pausebutton;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
      
       
    }

    // Update is called once per frame
    public void OnPlay()
    {
        videplayer.url = url;
        videplayer.Play();
        pausebutton.SetActive(true);
        playbutton.SetActive(false);
    }
    public void OnStop()
    {
        videplayer.Stop();
        pausebutton.SetActive(false);
        playbutton.SetActive(true);
    }
}
