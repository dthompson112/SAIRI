using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	//used for primary functionality of the game
	
	GameObject door;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//if the left mouse button has been pressed
		if(Input.GetMouseButtonDown(0)){
			Doors.toggle_doors_on_click(door); 
		}
	}
}

