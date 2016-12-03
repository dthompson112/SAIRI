using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour {

	private Vector3 dronePosition;
	private bool playerHasControl;
	private float speed;
	private Rigidbody2D droneBody;

	enum QuestState{Initial, None, Frank, Mark, Life_Support_System, Zephyr, Shield, Engine};
	QuestState questState;
	private int[] branches;

	void Start () {

		droneBody = GetComponent<Rigidbody2D> ();
		dronePosition = droneBody.transform.position;
		playerHasControl = false;
		speed = 1.5f;
		questState = QuestState.Initial;
		branches = new int[8];
		for (int i = 0; i < branches.Length; i++) {
			branches [i] = 1;
		}
	}

	void Update () {

		//Gets new position based on mouse position
		if (Input.GetMouseButton (0) && playerHasControl == true) {	

				dronePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				dronePosition.z = droneBody.transform.position.z;

			if (dronePosition.x < -4.3f || dronePosition.x > 3.9f || dronePosition.y < -4.9f || dronePosition.y > 5f) {
				droneBody.MovePosition(Vector3.MoveTowards(droneBody.position, droneBody.position, 0));			
			} 
			else {
				//Moves drone
				droneBody.MovePosition (Vector3.MoveTowards (droneBody.transform.position, dronePosition, speed * Time.deltaTime));
			}
		}
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Frank") {
			questState = QuestState.Frank;
		} else if (other.name == "Mark") {
			questState = QuestState.Mark;
		} else if (other.name == "Life_Support_System") {
			questState = QuestState.Life_Support_System;
		} else if (other.name == "Zephyr") {
			questState = QuestState.Zephyr;
		} else if (other.name == "Shield") {
			questState = QuestState.Shield;
		} else if (other.name == "Engine") {
			questState = QuestState.Engine;
	}
	}
	public void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player" || other.tag == "Item") {
			questState = QuestState.None;
		}
	}
	public void EnableMovement() {
		playerHasControl = true;
	}

	public void DisableMovement() {
		droneBody.velocity = new Vector3 (0, 0, 0);
		playerHasControl = false;
	}

	public void updateBranch(int questState, int branch) {
		branches [questState] = branch;
	}

	public int getBranch(int questState) {
		return branches[questState];
	}

	public void updateQuestState(int questState) {
		this.questState = (QuestState) questState;
	}

	public int getQuestState() {
		return (int) questState;
	}
}