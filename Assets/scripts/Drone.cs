﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Drone : MonoBehaviour {

	private Vector3 dronePosition;
	private bool playerHasControl;
	private float speed;
	private Rigidbody2D droneBody;

	enum QuestState{Initial, None, Frank, Mark, Life_Support_System, Zephyr, Shield, Engine, Turret, Shultz, Bar, Junkbox, Toolbox, Damage, Cornelius, Footlocker, Cornlocker};
	QuestState questState;
	private int[] branches;
	private Text TextName;
	public ModalWindow mw;

	void Start () {

		droneBody = GetComponent<Rigidbody2D> ();
		dronePosition = droneBody.transform.position;
		playerHasControl = false;
		speed = 30f;
		questState = QuestState.Initial;
		branches = new int[17];
		for (int i = 0; i < branches.Length; i++) {
			branches [i] = 1;
		}
		TextName = GameObject.Find("TextName").GetComponent<UnityEngine.UI.Text>();
		mw = GameObject.Find("Canvas").GetComponent<ModalWindow>();
		mw.DecisionTree();
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

		//button controls

		if (Input.GetKey("escape"))
			SceneManager.LoadScene(0);

		if (Input.GetKeyDown("i"))
			mw.DecisionTree();

		if (Input.GetKeyDown("1"))
			mw.myYesAction();

		if (Input.GetKeyDown("2"))
			mw.myCancelAction();

		if (Input.GetKeyDown("3"))
			mw.myNoAction();

	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Frank") {
			questState = QuestState.Frank;
			TextName.text = other.name;
		} else if (other.name == "Mark") {
			questState = QuestState.Mark;
			TextName.text = other.name;
		} else if (other.name == "Life_Support_System") {
			questState = QuestState.Life_Support_System;
			TextName.text = "Life Support";
		} else if (other.name == "Zephyr") {
			questState = QuestState.Zephyr;
			TextName.text = other.name;
		} else if (other.name == "Shield") {
			questState = QuestState.Shield;
			TextName.text = other.name;
		} else if (other.name == "Engine") {
			questState = QuestState.Engine;
			TextName.text = other.name;
		} else if (other.name == "Turret") {
			questState = QuestState.Turret;
			TextName.text = other.name;
		} else if (other.name == "Shultz") {
			questState = QuestState.Shultz;
			TextName.text = other.name;
		} else if (other.name == "Bar") {
			questState = QuestState.Bar;
			TextName.text = other.name;
		} else if (other.name == "Crate 1" || other.name == "Crate 2" || other.name == "Crate 3") {
			questState = QuestState.Junkbox;
			TextName.text = other.name;
		} else if (other.name == "Crate 4") {
			questState = QuestState.Toolbox;
			TextName.text = other.name;
		} else if (other.name == "Damage") {
			questState = QuestState.Damage;
			TextName.text = other.name;
		} else if (other.name == "Locker 4") {
			questState = QuestState.Cornlocker;
			TextName.text = other.name;
		} else if (other.name == "Cornelius") {
			questState = QuestState.Cornelius;
			TextName.text = other.name;
		} else if (other.name == "Locker 1" || other.name == "Locker 2" || other.name == "Locker 3" || other.name == "Locker 5" || other.name == "Locker 6") {
			questState = QuestState.Footlocker;
			TextName.text = other.name;
		} else if (other.name == "Dpad") {
			questState = QuestState.Initial;
			TextName.text = "Objective Menu";
		}
	}
	public void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player" || other.tag == "Item") {
			questState = QuestState.None;
			TextName.text = "Nothing";
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
