using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour
{

	public Transform target;
	public Transform cam;
	public Vector3 offset = Vector3.zero;
	static public float cameraRotSide;
	static public float cameraRotUp;
	public float cameraRotSideCur;
	public float cameraRotUpCur;
	static public float distance;
	public bool reset_pos ;
	public GameObject hlp;
	public float pinchSpeed;
	private Touch touch;
	private float lastDist = 0;
	private  float curDist = 0;
	static public float initial_dist;
	static  public float c;
	public float zoomStart = 15;
	public float zoomMin;
	public float zoomMax;
	public float cameraRotSideStart;
	public float cameraRotUpStart;
	public float CurentZoom;
	public static bool orbtBool;
//	static public bool zoom_activation;
	
	void Start ()
	{
		pinchSpeed = 2f;
		cameraRotSide = cameraRotSideStart;
		cameraRotUp = cameraRotUpStart;
		cameraRotUpCur = transform.localEulerAngles.x;	
		cameraRotSideCur = transform.localEulerAngles.y;
		distance = zoomStart;	
//		cam.camera.orthographicSize=zoomStart;
		reset_pos = false;
//		initial_dist = distance;
//		zoom_activation = false;
		orbtBool=true;
	}

	
	void Update ()
	{
		if (orbtBool){
			reset_pos = false;
		CurentZoom = cam.localPosition.z;
		if (Application.platform != RuntimePlatform.Android) {
//			if(Input.GetKeyUp(KeyCode.LeftShift)) {
			if (Input.GetMouseButton (0)) {
				//if(!Try_another.rot){
				cameraRotSide += Input.GetAxis ("Mouse X") * 9;
				cameraRotUp -= Input.GetAxis ("Mouse Y") * 9;
//					if(cameraRotUp>80){
//						cameraRotUp =80;
//					}
//					if(cameraRotUp<-80){
//						cameraRotUp=-80;
//					}
			}
//			}	
			if (Input.GetKey (KeyCode.UpArrow)) {
				cameraRotUp -= 0.5f;
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				cameraRotUp += 0.5f;
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				cameraRotSide -= 0.5f;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				cameraRotSide += 0.5f;
			}
		} else {
			if ((Input.touchCount == 1) && (Input.GetTouch (0).phase == TouchPhase.Moved)) {
				cameraRotSide += Input.GetAxis ("Mouse X") * 9;
				cameraRotUp -= Input.GetAxis ("Mouse Y") * 9;	
			}
		}
//		reset_pos = false;
		
		if (Application.platform != RuntimePlatform.Android) {
			//			if (Input.GetMouseButton(0)) {
			//				distance += Input.GetAxis("Mouse Y")*1;
			//			}

			if (Input.GetKey (KeyCode.KeypadPlus)) {
				distance -= 0.1f;
//					cam.camera.orthographicSize -=0.1f;
			}
			if (Input.GetKey (KeyCode.KeypadMinus)) {
				distance += 0.1f;
//					cam.camera.orthographicSize +=0.1f;
			}
//			if (zoom_activation == true) {
			distance *= (1 - 1 * Input.GetAxis ("Mouse ScrollWheel"));
//				cam.camera.orthographicSize *= (1 - 1 * Input.GetAxis ("Mouse ScrollWheel"));
//			}
		} else {
			if (Input.touchCount > 1 && (Input.GetTouch (0).phase == TouchPhase.Moved || Input.GetTouch (1).phase == TouchPhase.Moved)) {
				var touch1 = Input.GetTouch (0);
				var touch2 = Input.GetTouch (1);
				curDist = Vector2.Distance (touch1.position, touch2.position);
				if (curDist > lastDist) {
					distance -= Vector2.Distance (touch1.deltaPosition, touch2.deltaPosition) * pinchSpeed / 10;
				} else {
					distance += Vector2.Distance (touch1.deltaPosition, touch2.deltaPosition) * pinchSpeed / 10;
				}
//				if (zoom_activation == true) {
				lastDist = curDist;
//				}
			}
		}
		
		//if(Controls.z){

//		distance *= (1-1*Input.GetAxis("Mouse ScrollWheel"));
//		
//		if (Input.GetKey (KeyCode.UpArrow)){
//			distance +=  0.1f;
//		}
//		if(Input.GetKey (KeyCode.DownArrow)){
//			distance -=  0.1f;
//		}
		//}
		
		//	if(Buttons_Selection.Pan_b){
//				reset_pos = false;	
		if (Application.platform != RuntimePlatform.Android) {
			if (Input.GetMouseButton (0) && Input.GetKey (KeyCode.LeftShift)) {
				float x = -Input.GetAxis ("Mouse X") * 0.8f;
				float y = -Input.GetAxis ("Mouse Y") * 0.8f;
				cam.transform.localPosition = new Vector3 (cam.transform.localPosition.x + x, cam.transform.localPosition.y + y, cam.transform.localPosition.z);
//						cam.localPosition.y -= Input.GetAxis("Mouse Y")*0.8;

			} else {
			}

			if (Input.GetKey (KeyCode.W)) {
				cam.transform.localPosition = new Vector3 (cam.transform.localPosition.x, cam.transform.localPosition.y - 0.03f, cam.transform.localPosition.z);
			}
			if (Input.GetKey (KeyCode.S)) {
				cam.transform.localPosition = new Vector3 (cam.transform.localPosition.x, cam.transform.localPosition.y + 0.03f, cam.transform.localPosition.z);
			}
			if (Input.GetKey (KeyCode.A)) {
				cam.transform.localPosition = new Vector3 (cam.transform.localPosition.x + 0.03f, cam.transform.localPosition.y, cam.transform.localPosition.z);
			}
			if (Input.GetKey (KeyCode.D)) {
				cam.transform.localPosition = new Vector3 (cam.transform.localPosition.x - 0.03f, cam.transform.localPosition.y, cam.transform.localPosition.z);
			}
		} else {
			if ((Input.touchCount == 1) && (Input.GetTouch (0).phase == TouchPhase.Moved)) {
				if (Input.GetMouseButton (0)) {
					float x = -Input.GetAxis ("Mouse X") * 0.8f;
					float y = -Input.GetAxis ("Mouse Y") * 0.8f;
					cam.transform.localPosition = new Vector3 (cam.transform.localPosition.x + x, cam.transform.localPosition.y + y, cam.transform.localPosition.z);
//							cam.localPosition.x -= Input.GetAxis("Mouse X")*0.8;
//							cam.localPosition.y -= Input.GetAxis("Mouse Y")*0.8;
				}
			}
		}
		//	}
		//	if(Controls.home){
		//		reset_pos = true;
		//		if(reset_pos){
		//			cam.localPosition.x = Mathf.Lerp(cam.localPosition.x,0,0.1);
		//			cam.localPosition.y = Mathf.Lerp(cam.localPosition.y,0,0.1);
		//		}
		//	
		//	}
		
		
		//	if(hlp.active == false){
		if (distance <= zoomMin) {
			distance = zoomMin;
		} else if (distance >= zoomMax) {
			distance = zoomMax;
		}
//		if(Buttons_Selection.Explode_b){
//			zoomMin =39;
//		}
//		else{
//			zoomMin =16;
//		}
		//	}else{
		//distance =3;
		//	}	
		if (cam.GetComponent<Camera> ().orthographicSize <= zoomMin) {
			cam.GetComponent<Camera> ().orthographicSize = zoomMin;
		} else if (cam.GetComponent<Camera> ().orthographicSize >= zoomMax) {
			cam.GetComponent<Camera> ().orthographicSize = zoomMax;
						
		}
		//	if(Try_another.rot){
		//		if(cameraRotSide<-20){
		//			cameraRotSide = -20;	
		//		}
		//		if(cameraRotSide>20){
		//			cameraRotSide = 20;
		//		}
		//		cameraRotUp = 0;
		//	}
		//	
		
		
		cameraRotSideCur = Mathf.LerpAngle (cameraRotSideCur, cameraRotSide, Time.deltaTime * 5);
		cameraRotUpCur = Mathf.Lerp (cameraRotUpCur, cameraRotUp, Time.deltaTime * 5);

		Vector3 targetPoint = target.position;
		transform.position = Vector3.Lerp (transform.position, targetPoint + offset, Time.deltaTime);
		transform.rotation = Quaternion.Euler (cameraRotUpCur, cameraRotSideCur, 0);
		float dist = Mathf.Lerp (-cam.transform.localPosition.z, distance, Time.deltaTime * 5);
		cam.localPosition = new Vector3 (cam.localPosition.x, cam.localPosition.y, -dist);
		c = -dist;
	}
		
	}
}
