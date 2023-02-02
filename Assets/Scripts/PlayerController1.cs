using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerController1 : MonoBehaviourPun, IPunObservable
{
    public float _speed = 3;
    public float _rotationSpeed = 3;
    public float minX, maxX;
    public float minZ, maxZ;

    public GameObject pickUpObj;
    //public FixedJoystick joystick;

    public bool ispicked;

    private CharacterController _controller;
    private Rigidbody rigidbody;
    private Vector3 vec;

    private PhotonView photonView;

    void Start()
    {
#if UNITY_EDITOR
        //gameObject.AddComponent<CharacterController>();
        //_controller = GetComponent<CharacterController>();
#endif
//#if UNITY_ANDROID
        //gameObject.AddComponent<Rigidbody>();
        //rigidbody = GetComponent<Rigidbody>();
//#endif
     //   joystick = GameObject.FindWithTag("JoyStick").GetComponent<FixedJoystick>();

        photonView = GetComponent<PhotonView>();
        if(!photonView.IsMine)
        {
            //GameManager.instance.joyStcikController.player = this.transform;
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    void Update()
    {
#if UNITY_ANDROID
        //rigidbody.velocity = new Vector3(joystick.Horizontal * _speed, rigidbody.velocity.y, joystick.Vertical * _speed);
        //if (joystick.Horizontal != 0f || joystick.Vertical != 0f)
        //{
        //    transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
        //}
        //else
        //{

        //}
#endif
#if UNITY_EDITOR
        //if (transform != null)
        //{
        //    transform.Rotate(0, Input.GetAxis("Horizontal") * _rotationSpeed, 0);
        //    var forward = transform.TransformDirection(Vector3.forward);
        //    float curSpeed = _speed * Input.GetAxis("Vertical");
        //    _controller.SimpleMove(forward * curSpeed);
        //}
#endif
        vec = transform.position;
        transform.position = new Vector3(Mathf.Clamp(vec.x, minX, maxX), vec.y, Mathf.Clamp(vec.z, minZ, maxZ));
    }

    //Device Mode
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Stack") && !ispicked)
    //    {
    //        ispicked = true;
    //        GameObject pickedObj = collision.gameObject;
    //        pickedObj.transform.SetParent(pickUpObj.transform);
    //        pickedObj.transform.position = pickUpObj.transform.position;
    //        pickedObj.transform.localRotation = Quaternion.identity;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stack") && !ispicked)
        {
            //obj = other.gameObject;
            //ispicked = true;
            //GameObject pickedObj = obj;
            //pickedObj.transform.SetParent(pickUpObj.transform);
            //pickedObj.transform.position = pickUpObj.transform.position;
            //pickedObj.transform.localRotation = Quaternion.identity;


            photonView.RPC("PickUpStackObj_RPC", RpcTarget.All, other.gameObject.name);
        }
    }
    
    [PunRPC]
    void PickUpStackObj_RPC(string objName)
    {
        GameObject obj = GameObject.Find(objName);
        ispicked = true;
        GameObject pickedObj = obj;
        pickedObj.transform.SetParent(pickUpObj.transform);
        pickedObj.transform.position = pickUpObj.transform.position;
        pickedObj.transform.localRotation = Quaternion.identity;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(ispicked);
        }
        else
        {
            ispicked = (bool)stream.ReceiveNext();
        }
    }

    // Editor Mode
    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{        
    //    if (hit.gameObject.CompareTag("Stack") && !ispicked)
    //    {
    //        ispicked = true;
    //        Debug.LogError("collision name = " + hit.gameObject.name);
    //        GameObject pickedObj = hit.gameObject;
    //        pickedObj.transform.SetParent(pickUpObj.transform);
    //        pickedObj.transform.position = pickUpObj.transform.position;
    //        pickedObj.transform.localRotation = Quaternion.identity;    
    //    }
    //}

}