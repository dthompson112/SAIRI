﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	// starts the game from the start menu
	public void startButton () {
		SceneManager.LoadScene(1);
	}
}
