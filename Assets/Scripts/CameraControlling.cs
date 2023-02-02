using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
    // Start is called before the first frame update



namespace MultiWebcam
{
    public class CameraControlling : MonoBehaviour
    {
        [SerializeField] private RawImage _display;

        private WebCamDevice[] devices;
        private WebCamTexture _texture;
        private int _currentCameraIndex = 0;

        private void Awake()
        {
            StartCoroutine(Start());
        }

        private IEnumerator Start()
        {
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
            if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                Debug.Log("webcam found");
                devices = WebCamTexture.devices;
                for (int cameraIndex = 0; cameraIndex < devices.Length; ++cameraIndex)
                {
                    Debug.Log("devices[cameraIndex].name: ");
                    Debug.Log(devices[cameraIndex].name);
                    Debug.Log("devices[cameraIndex].isFrontFacing");
                    Debug.Log(devices[cameraIndex].isFrontFacing);
                }
            }
            else
            {
                Debug.Log("no webcams found");
            }
        }

        public void SwapCamera()
        {
            if (WebCamTexture.devices.Length > 0)
            {
                _currentCameraIndex++;
                _currentCameraIndex %= WebCamTexture.devices.Length;

                if (_texture != null)
                {
                    StopCamera();
                    StartCamera();
                }
            }
        }

        public void StartCamera()
        {
            Debug.LogError($"USER PERMISSION {Application.HasUserAuthorization(UserAuthorization.WebCam)}");
            Debug.LogError($"DEVICES AMOUNT {WebCamTexture.devices.Length}");

            if (WebCamTexture.devices.Length > 0)
            {
                WebCamDevice device = WebCamTexture.devices[_currentCameraIndex];
                _texture = new WebCamTexture(device.name);
                _display.texture = _texture;

                _texture.Play();
                Debug.LogError($"START PLAYING!");
            }
        }

        public void StopCamera()
        {
            if (_texture != null)
            {
                _texture.Stop();
                _display.texture = null;
                _texture = null;
            }
        }
    }
}