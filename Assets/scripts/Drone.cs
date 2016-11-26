using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour {

	private Vector3 dronePosition;
	private bool isSelected = false;
	public float speed = 1.5f;

	void Start () {

		dronePosition = transform.position;	
	}

	void Update () {

		//Toggles whether the drone is selected or not
		if (Input.GetKeyDown("d") && isSelected == false) {
			isSelected = true;
		}
		else if (Input.GetKeyDown("d") && isSelected == true) {
			isSelected = false;
		}

		//Gets new position based on mouse position
		if (Input.GetMouseButton (0) && isSelected == true) {			
			dronePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			dronePosition.z = transform.position.z;
		}

		//Moves drone
		transform.position = Vector3.MoveTowards (transform.position, dronePosition, speed * Time.deltaTime);
	}
}