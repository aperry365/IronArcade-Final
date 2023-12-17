using UnityEngine;
using System.Collections;

public class ResetPins : MonoBehaviour {
	
	
	public Vector3 _Position;
	public Quaternion _Rotation;
	
	
	// initialise pin
	void OnEnable () {
		_Position = gameObject.transform.position;
		_Rotation = gameObject.transform.rotation;
	}


	


	public void ResetPin(object _ball)
	{
		
		gameObject.transform.position  = _Position;
		gameObject.transform.rotation = _Rotation;
		gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		
	}
}
