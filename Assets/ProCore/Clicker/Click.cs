using UnityEngine;
using System.Collections;

public class Click : MonoBehaviour {

	float timer = 0;
	
	[SerializeField]
	AnimationCurve curve;

	public GameObject CameraObject;
	public GameObject Waypoint1;
	public GameObject Waypoint2;

	public bool firstPoint = true;

	private GameObject TargetObject;
	public Vector3 FromPosition;
	public Quaternion FromRotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (clicked) {

			var localScale = transform.localScale;
			var scale = curve.Evaluate(timer);
			localScale.x = scale;
			localScale.y = scale;
			localScale.z = scale;
			transform.localScale = localScale;
			
			// Increase the timer by the time since last frame
			timer += Time.deltaTime;

			CameraObject.transform.localPosition = Vector3.Slerp(FromPosition, TargetObject.transform.localPosition, timer);
			CameraObject.transform.localRotation = Quaternion.Slerp(FromRotation, TargetObject.transform.localRotation, timer);

			if (timer > 1f) { 
				clicked = false;
				CameraObject.transform.localRotation = TargetObject.transform.localRotation;
			}
		}
	}

	bool clicked = false;

	void OnMouseDown () {
		// change the target of the LookAtTarget script to be this gameobject.
		if (firstPoint) {
			TargetObject = Waypoint2;
		}
		else {
			TargetObject = Waypoint1;
		}
		firstPoint = !firstPoint;
		FromPosition = CameraObject.transform.localPosition;
		FromRotation = CameraObject.transform.localRotation;

		//CameraObject.transform.localPosition = TargetObject.transform.localPosition;
		//CameraObject.transform.localRotation = TargetObject.transform.localRotation;

		Debug.Log("CLICKED!");
		timer = 0f;
		clicked = true;
	}
}
