using UnityEngine;
using System.Collections;

public class Doors : MonoBehaviour {

	Animator animator;
	bool doorOpen;
	BoxCollider2D coll;
	//public Animation anim;

	void Start(){
		doorOpen = false;
		animator = GetComponent<Animator>();
		coll = GetComponent<BoxCollider2D>();

	}

	//to allow players to walk through a door
	//check if there is a collision and the door is open in the player script and then
	//use physics.IgnoreCollision
	//turning off the collider in the door script means that you cannot use the mouse to close doors.

	public void door_position(){
		//opens or closes door
		if (doorOpen) {
			animator.SetTrigger ("close");
			doorOpen = false;
		} else {
			animator.SetTrigger ("open");
			doorOpen = true;
		}
	}


	public static void toggle_doors_on_click(GameObject door){

		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //get mouse position
		Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition); //check if the mouse is over a collider
		
		//Debug.Log("mouse pos "+mousePosition.x+" y "+mousePosition.y+" ");    
		
		
		if(hitCollider){  //if it is get the name of the collider
			//Debug.Log ("Hit "+hitCollider.transform.name);
			string hit = hitCollider.transform.name;
			if(hit.Contains ("door")){ //if the name has door in it
				door = GameObject.Find (hit); //create a door object using the name
				door.GetComponent<Doors>().door_position(); //change the door position
			}
		}
	}

	//Opens the door if the drone tries to move through collision
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && doorOpen == false) {
			door_position ();
		}
	}

	//Closes the door if the drone leaves collision
	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player" && doorOpen == true) {
			door_position ();
		}
	}

}
