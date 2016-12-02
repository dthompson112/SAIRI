using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour {

	private Vector3 dronePosition;
	private bool isSelected = false;
	public float speed = 1.5f;
	private Rigidbody2D droneBody;

	void Start () {

		droneBody = GetComponent<Rigidbody2D> ();
		dronePosition = droneBody.transform.position;
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
			dronePosition.z = droneBody.transform.position.z;
		}

		//Moves drone
		droneBody.MovePosition(Vector3.MoveTowards(droneBody.transform.position, dronePosition, speed * Time.deltaTime));
	}
}