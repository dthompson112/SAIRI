using UnityEngine;
using System.Collections;


public class Loader : MonoBehaviour {

	//Loads the game manager object
	public GameObject gameManager;

	void start(){
		Debug.Log ("Game manager started");
		if (GameManager.instance == null) {
			Instantiate (gameManager); //instatiate the game manager which runs the Game.cs script
		}
	}
}
