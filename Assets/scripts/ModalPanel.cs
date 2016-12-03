using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanel: MonoBehaviour {

	public Text question;
	public Image iconImage;
	public Button yesButton;
	public Button noButton;
	public Button cancelButton;
	public GameObject modalPanelObject;

	private static ModalPanel modalPanel;



	public static ModalPanel Instance(){
		if (!modalPanel) {
			modalPanel = FindObjectOfType (typeof(ModalPanel)) as ModalPanel;
			if (!modalPanel)
				Debug.LogError ("There needs to be one active ModalPanel script on a GameObject in your scene.");
		}
		return modalPanel;
	}


	public void Choice(string question, UnityAction yesEvent, UnityAction noEvent){
		modalPanelObject.SetActive (true); //makes the panel apear
		yesButton.onClick.RemoveAllListeners(); //prevent previouse events from affecting current clicks
		yesButton.onClick.AddListener(yesEvent); //add yes event to onclick
		yesButton.onClick.AddListener (ClosePanel); //add close panel event to onclick//calls ClosePanel fuction

		noButton.onClick.RemoveAllListeners();
		noButton.onClick.AddListener (noEvent);
		noButton.onClick.AddListener (ClosePanel);

		this.question.text = question;
		this.iconImage.gameObject.SetActive (false);
		yesButton.gameObject.SetActive (true);
		noButton.gameObject.SetActive (true);
		cancelButton.gameObject.SetActive (false);

	}

	// Yes/No/Cancel
	//unity action is a pointer funtion
	public void Choice(string question, UnityAction yesEvent, UnityAction noEvent, UnityAction cancelEvent){
		modalPanelObject.SetActive (true); //makes the panel apear
		yesButton.onClick.RemoveAllListeners(); //prevent previouse events from affecting current clicks
		yesButton.onClick.AddListener(yesEvent); //add yes event to onclick
		yesButton.onClick.AddListener (ClosePanel); //add close panel event to onclick//calls ClosePanel fuction

		noButton.onClick.RemoveAllListeners();
		noButton.onClick.AddListener (noEvent);
		noButton.onClick.AddListener (ClosePanel);

		cancelButton.onClick.RemoveAllListeners ();
		cancelButton.onClick.AddListener (cancelEvent);
		cancelButton.onClick.AddListener (ClosePanel);

		this.question.text = question;
		this.iconImage.gameObject.SetActive (false);
		yesButton.gameObject.SetActive (true);
		noButton.gameObject.SetActive (true);
		cancelButton.gameObject.SetActive (true);


	}

	void ClosePanel(){
		modalPanelObject.SetActive (false);
	}
}
