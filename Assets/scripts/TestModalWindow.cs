using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class TestModalWindow : MonoBehaviour {
	private ModalPanel modalPanel;

	private UnityAction myYesAction;
	private UnityAction myNoAction;
	private UnityAction myCancelAction;

	void Awake(){
		modalPanel = ModalPanel.Instance ();

		myYesAction = new UnityAction (TestYesFunction);
		myNoAction = new UnityAction (TestNoFunction);
		myCancelAction = new UnityAction (TestCancelFunction);
	}

	public void TestYNC(){
		modalPanel.Choice ("Would you like to interact with this text box?", myYesAction, myNoAction, myCancelAction);
	}

	//Send to the Modal Panel to set up the Buttons and Functions to call
	void TestYesFunction(){
		Debug.Log ("Yes");
	}

	void TestNoFunction(){
		Debug.Log ("No");
	}

	void TestCancelFunction(){
		Debug.Log ("Cancel");
	}
}
