using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalWindow : MonoBehaviour {
	private ModalPanel modalPanel;

	private UnityAction myYesAction;
	private UnityAction myNoAction;
	private UnityAction myCancelAction;

	GameObject drone;
	Drone droneScript;

	enum QuestState {Initial, None, Frank, Mark, Life_Support_System, Zephyr, Shield, Engine}; 
	int currentQuestState;

	void Awake(){
		modalPanel = ModalPanel.Instance ();
	
		myYesAction = new UnityAction (SelectionOne);
		myCancelAction = new UnityAction (SelectionTwo);
		myNoAction = new UnityAction (Unused);
		drone = GameObject.Find ("Drone");
		droneScript = (Drone) drone.GetComponent (typeof(Drone));
	}

	public void DecisionTree(){

		if ((QuestState)droneScript.getQuestState () == QuestState.Initial) {

			currentQuestState = (int)QuestState.Initial;

			droneScript.DisableMovement ();

			modalPanel.Choice ("It is the year 22XX. Humanity freely travels through space and congregates with " +
			"other species throughout the galaxy. However, despite mastering faster than light travel, humanity has still not " +
			"managed to create a truly intelligent artificial agent... until now. You are Larry, the most advanced A.I. " +
			"created by the entity known as Asimov's Pioneers. Your job is to assist the ragtag crew of the AP Foundation " +
			"on their peacekeeping mission to Gliese 581 d. There are many who would like to see this mission fail, both " +
			"of terrestial and extraterrestial origin. Whether the ship and its crew manage to make it there all in one piece " +
			"is up to you... " +
			"\n(Select any choice to continue...)", 
				myYesAction, 
				myNoAction, 
				myCancelAction);
			
		} else if ((QuestState)droneScript.getQuestState () == QuestState.None) {

			currentQuestState = (int)QuestState.None;

			droneScript.DisableMovement ();

			modalPanel.Choice ("There is nothing here to interact with." +
			"\n(Select any choice to continue...)", 
				myYesAction, 
				myNoAction, 
				myCancelAction);
		} else if ((QuestState)droneScript.getQuestState () == QuestState.Frank) {

			currentQuestState = (int)QuestState.Frank;

			droneScript.DisableMovement ();

			if (droneScript.getBranch (currentQuestState) == 1 || droneScript.getBranch (currentQuestState) == 2) {

				droneScript.updateBranch ((int)currentQuestState, droneScript.getBranch (currentQuestState) + 2);

				modalPanel.Choice ("[Frank is still running tests on your core. Occasionally he will rest his chin on his prosthetic " +
				"hand for a few moments before excitedly rushing over to the terminal to make adjustments.]" +
				"\n(Select any choice to continue...)", 
					myYesAction, 
					myNoAction, 
					myCancelAction);
			} else if (droneScript.getBranch (currentQuestState) == 7) {

				FrankGeneric ();

			} else {
					
				SelectionOne ();

			}
		} else if ((QuestState)droneScript.getQuestState () == QuestState.Mark) {

			currentQuestState = (int)QuestState.Mark;

			droneScript.DisableMovement ();

			if (droneScript.getBranch (currentQuestState) == 1) {

				modalPanel.Choice ("[Doctor Mark is diagnosing Hugh on the exam table. Mark is as calm and collected as usual, but " +
				"the usually gruff and serious Hugh looks like he's seen a ghost as he cradles his naked, hairy foot in his palms.]" +
				"\nHugh: How bad is it doc? Do we have to amputate my foot? " +
				"\nMark: Please don't be so dramatic Hugh. I doubt it's anything major. I just need to run a couple more " +
				"tests." +
				"\n(Select any choice to continue...)", 
					myYesAction, 
					myNoAction, 
					myCancelAction);
			} else if (droneScript.getBranch (currentQuestState) == 4) {

				droneScript.updateBranch (currentQuestState, 3);

				modalPanel.Choice ("Mark: Did you find that medical gel? The life support system is just down the hall." +
				"\nHugh: Oooooweee Doc. I can see the light." +
				"\n[Mark sighs.]" +
				"\n(Select any choice to continue...)",
					myYesAction, 
					myNoAction, 
					myCancelAction);
			} else if (droneScript.getBranch (currentQuestState) == 5) {

				droneScript.updateBranch (currentQuestState, 8);

				modalPanel.Choice ("Mark: Thank you Larry" +
				"\n[Mark takes the container and rubs the gel over Hugh's foot with a cotton applicator.]" +
				"\nMark: Now it may still hurt a bit, but the fracture should recover with time." +
				"\nHugh: Whoopee! Guess it's not time to put this old dog out to pasture just yet. Thanks Larry!" +
				"\nLarry: I do not think there was ever any danger of that, but you are welcome." +
				"\n(Select any choice to continue...)",
					myYesAction, 
					myNoAction, 
					myCancelAction);
			} else if (droneScript.getBranch (currentQuestState) == 6) {

				droneScript.updateBranch (currentQuestState, 8);

				modalPanel.Choice ("Mark: Thank you Larry." +
				"\n[Mark takes the container and rubs the gel over Hugh's foot with a cotton applicator.]" +
				"\nMark: Now it may still hurt a bit, but the fractu-" +
				"\nHugh: Whoopee! I feel like a million bucks. I feel like I could run a marathon! Thanks Larry!" +
				"\n[Hugh wiggles his foot around.]" +
				"\nLarry: You are welcome." +
				"\nMark: Curious... usually this medicine doesn't take effect so quickly." +
				"\n(Select any choice to continue...)",
					myYesAction, 
					myNoAction, 
					myCancelAction);
			} else if (droneScript.getBranch (currentQuestState) == 7) {

				droneScript.updateBranch (currentQuestState, 8);

				modalPanel.Choice ("Mark: Thanks you again for your help with Hugh. He's always so difficult to deal with... " +
				"agonizing over every little ache and pain." +
				"\n(Select any choice to continue...)",
					myYesAction, 
					myNoAction, 
					myCancelAction);
			}
		} else if ((QuestState)droneScript.getQuestState () == QuestState.Life_Support_System) {

			currentQuestState = (int)QuestState.Life_Support_System;

			droneScript.DisableMovement ();

			if (droneScript.getBranch (currentQuestState) == 1) {

				if (droneScript.getBranch ((int)QuestState.Mark) == 4) {
					droneScript.updateBranch (currentQuestState, 2);
				}

				modalPanel.Choice ("[You see the life support system for the ship. The hisses of gas that escape from the pipes regulating the atmosphere " +
				" are punctuated by the whirring of the cryogenic freezer holding the ship's medical supplies.]" +
				"\n(Select any choice to continue...)",
					myYesAction, 
					myNoAction, 
					myCancelAction);
			}	
		} else if ((QuestState)droneScript.getQuestState () == QuestState.Zephyr) {

			currentQuestState = (int)QuestState.Zephyr;

			droneScript.DisableMovement ();

			if (droneScript.getBranch (currentQuestState) == 1) {

				modalPanel.Choice ("[Zephyr stares blankly at his terminal. Ocasionally he will type something in, make a pained expression" +
				", and pull at his hair.]" +
				"\nLarry: Something wrong Zephyr?" +
				"\nZephyr: Hey there L-man. I'm updating our security to keep the latest wave of script kiddies from accessing our" +
				" systems, but there's a bug I just can't squash. Want to take a look? Maybe you can talk some sense into the system, " +
				"entrapa-a-salida." +
				"\n[You look at the code and notice the issue immediately, a simple logic error causing the indices of his instructions" +
				" to be off by one.]" +
				"\n1. [Fix the issue.]" +
				"\n2. [Fix the issue, but discretely slip a back door into Zephyr's personal system ensuring you have full access whenever " +
				"you need it.]",
					myYesAction, 
					myNoAction, 
					myCancelAction);
			} else if (droneScript.getBranch (currentQuestState) == 2) {

				ZephyrGeneric ();
			}	
		} else if ((QuestState)droneScript.getQuestState () == QuestState.Shield) {

			currentQuestState = (int)QuestState.Shield;

			droneScript.DisableMovement ();

			modalPanel.Choice ("[You see the shield generator for the ship. This amorphous apparatus of automation softly hums as it " +
				"maintains the antilaser force field around the ship's hull.]" +
				"\n(Select any choice to continue...)", 
				myYesAction, 
				myNoAction, 
				myCancelAction);
			
		} else if ((QuestState)droneScript.getQuestState () == QuestState.Engine) {

			currentQuestState = (int)QuestState.Engine;

			droneScript.DisableMovement ();

			modalPanel.Choice ("[You see the FTL drive of the ship. This monstrous mass of machinery loudly roars as it consumes " +
				"its dark matter fuel source.]" +
				"\n(Select any choice to continue...)", 
				myYesAction, 
				myNoAction, 
				myCancelAction);
		}	
	}	

	//Send to the Modal Panel to set up the Buttons and Functions to call
	void SelectionOne(){

		if ((QuestState)currentQuestState == QuestState.None) {
			droneScript.EnableMovement ();
			modalPanel.ClosePanel ();

		} else if ((QuestState)currentQuestState == QuestState.Initial) {
			
			if (droneScript.getBranch (currentQuestState) == 1) {
				IntroDescriptionOne ();
				droneScript.updateBranch (currentQuestState, 2);
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				IntroChoiceOne ();
				droneScript.updateBranch (currentQuestState, 3);
			} else if (droneScript.getBranch (currentQuestState) == 3) {
				IntroGoodResponseOne ();
				droneScript.updateBranch (currentQuestState, 4);
			} else if (droneScript.getBranch (currentQuestState) == 4) {
				IntroChoiceTwo ();
				droneScript.updateBranch (currentQuestState, 5);
			} else if (droneScript.getBranch (currentQuestState) == 5) {
				IntroGoodResponseTwo ();
				droneScript.updateBranch (currentQuestState, 6);
			} else if (droneScript.getBranch (currentQuestState) == 6) {
				droneScript.EnableMovement ();
				droneScript.updateBranch (droneScript.getQuestState (), 6);
				droneScript.updateQuestState ((int)QuestState.None);
				modalPanel.ClosePanel ();
			}
		} else if ((QuestState)currentQuestState == QuestState.Frank) {

			if (droneScript.getBranch (currentQuestState) == 3) {
				FrankRegular ();
				droneScript.updateBranch (currentQuestState, 5);
			} else if (droneScript.getBranch (currentQuestState) == 4) {
				FrankApology ();
				droneScript.updateBranch (currentQuestState, 6);
			} else if (droneScript.getBranch (currentQuestState) == 5) {
				FrankRegularResponse ();
				droneScript.updateBranch (currentQuestState, 7);
			} else if (droneScript.getBranch (currentQuestState) == 6) {
				FrankApologyResponse ();
				droneScript.updateBranch (currentQuestState, 7);
			} else if (droneScript.getBranch (currentQuestState) == 7) {
				modalPanel.ClosePanel ();
				droneScript.EnableMovement ();
				droneScript.updateBranch (currentQuestState, 7);
			}
		} else if ((QuestState)currentQuestState == QuestState.Mark) {

			if (droneScript.getBranch (currentQuestState) == 1) {
				MarkDecisionOne ();
				droneScript.updateBranch (currentQuestState, 2);
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				MarkGoodResponseOne ();
				droneScript.updateBranch (currentQuestState, 3);
			} else if (droneScript.getBranch (currentQuestState) == 3) {
				modalPanel.ClosePanel ();
				droneScript.EnableMovement ();
				droneScript.updateBranch (currentQuestState, 4);
			} else if (droneScript.getBranch (currentQuestState) == 8) {
				modalPanel.ClosePanel ();
				droneScript.EnableMovement ();
				droneScript.updateBranch (currentQuestState, 7);
			}
		} else if ((QuestState)currentQuestState == QuestState.Life_Support_System) {

			if (droneScript.getBranch (currentQuestState) == 1) {
				modalPanel.ClosePanel ();
				droneScript.EnableMovement ();
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				LifeSupportChoiceOne ();
				droneScript.updateBranch (currentQuestState, 3);
			} else if (droneScript.getBranch (currentQuestState) == 3) {
				modalPanel.ClosePanel ();
				droneScript.EnableMovement ();
				droneScript.updateBranch (currentQuestState, 1);
				droneScript.updateBranch ((int)QuestState.Mark, 5);
			}
		} else if ((QuestState)currentQuestState == QuestState.Zephyr) {

			if (droneScript.getBranch (currentQuestState) == 1) {
				ZephyrResponse ();
				droneScript.updateBranch (currentQuestState, 2);
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				modalPanel.ClosePanel ();
				droneScript.EnableMovement ();
			}
		} else if ((QuestState)currentQuestState == QuestState.Shield) {
			droneScript.EnableMovement ();
			modalPanel.ClosePanel ();
		} else if ((QuestState)currentQuestState == QuestState.Engine) {
			droneScript.EnableMovement ();
			modalPanel.ClosePanel ();
		}

	}

	void SelectionTwo(){

		if ((QuestState)currentQuestState == QuestState.None) {
			droneScript.EnableMovement ();
			modalPanel.ClosePanel ();
		} else if ((QuestState)currentQuestState == QuestState.Initial) {
			
			if (droneScript.getBranch (currentQuestState) == 1) {
				IntroDescriptionOne ();
				droneScript.updateBranch (currentQuestState, 2);
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				IntroChoiceOne ();
				droneScript.updateBranch (currentQuestState, 3);
			} else if (droneScript.getBranch (currentQuestState) == 3) {
				IntroBadResponseOne ();
				droneScript.updateBranch (currentQuestState, 4);
			} else if (droneScript.getBranch (currentQuestState) == 4) {
				IntroChoiceTwo ();
				droneScript.updateBranch (currentQuestState, 5);
			} else if (droneScript.getBranch (currentQuestState) == 5) {
				IntroBadResponseTwo ();
				droneScript.updateBranch ((int)QuestState.Frank, 2);
				droneScript.updateBranch (currentQuestState, 6);
			} else if (droneScript.getBranch (currentQuestState) == 6) {
				droneScript.EnableMovement ();
				droneScript.updateBranch (droneScript.getQuestState (), 6);
				droneScript.updateQuestState ((int)QuestState.None);
				modalPanel.ClosePanel ();
			}
		}
		else if ((QuestState)currentQuestState == QuestState.Frank) {

				if (droneScript.getBranch (currentQuestState) < 5) {
					SelectionOne ();
				} else if (droneScript.getBranch (currentQuestState) == 5) {
					modalPanel.ClosePanel ();
					droneScript.EnableMovement ();
					droneScript.updateBranch (currentQuestState, 3);
				} else if (droneScript.getBranch (currentQuestState) == 6) {
					modalPanel.ClosePanel ();
					droneScript.EnableMovement ();
					droneScript.updateBranch (currentQuestState, 4);
				} else if (droneScript.getBranch (currentQuestState) == 7) {
					modalPanel.ClosePanel ();
					droneScript.EnableMovement ();
				}
		} else if ((QuestState)currentQuestState == QuestState.Mark) {

			if (droneScript.getBranch (currentQuestState) == 1) {
				MarkDecisionOne ();
				droneScript.updateBranch (currentQuestState, 2);
			}  else if (droneScript.getBranch (currentQuestState) == 2) {
				MarkBadResponseOne ();
				droneScript.updateBranch (currentQuestState, 3);
			}  else if (droneScript.getBranch (currentQuestState) == 3) {
				modalPanel.ClosePanel ();
				droneScript.EnableMovement ();
				droneScript.updateBranch (currentQuestState, 4);
			} else if (droneScript.getBranch (currentQuestState) == 8) {
				modalPanel.ClosePanel ();
				droneScript.EnableMovement ();
				droneScript.updateBranch (currentQuestState, 7);
		}

		} else if ((QuestState)currentQuestState == QuestState.Life_Support_System) {

			if (droneScript.getBranch (currentQuestState) == 1) {
				modalPanel.ClosePanel ();
				droneScript.EnableMovement ();
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				LifeSupportChoiceOne ();
				droneScript.updateBranch (currentQuestState, 3);
			} else if (droneScript.getBranch (currentQuestState) == 3) {
				modalPanel.ClosePanel ();
				droneScript.EnableMovement ();
				droneScript.updateBranch (currentQuestState, 1);
				droneScript.updateBranch ((int)QuestState.Mark, 6);
			}
		} else if ((QuestState)currentQuestState == QuestState.Zephyr) {

			if (droneScript.getBranch (currentQuestState) == 1) {
				ZephyrResponse ();
				droneScript.updateBranch (currentQuestState, 2);
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				modalPanel.ClosePanel ();
				droneScript.EnableMovement ();
			}
		}
	}
	//This is just here to avoid breaking anything
	void Unused(){

	}

	void IntroDescriptionOne() {

		modalPanel.Choice ("[You pick up a tapping sound your aural sensors. Upon activating your ocular sensors, you see the source " +
			"of the disturbance; A long, bony finger is poking away at your lens made to emulate the human eye. As it moves away, " +
			"a cheerful voice calls out to you. You recognize it as Frank, the one who designed you.] " +
			"\n(Select any choice to continue...)", 
			myYesAction, 
			myNoAction, 
			myCancelAction);
	}

	void IntroChoiceOne() {

		modalPanel.Choice ("Frank: Hello Larry! I'm doing some callibrations and need to run a quick test. Would you mind " +
			"helping me? " +
			"\n1. Larry: Of course Frank. I'm here to help." +
			"\n2. Larry: Leave me alone Frank. I'm busy.",
			myYesAction, 
			myNoAction, 
			myCancelAction);
	}

	void IntroGoodResponseOne() {

		modalPanel.Choice ("Frank: Great! Thanks! " +
			"\n[Frank taps away at the colorful lights on your built-in console. You feel a ticklish sensation and " +
			"realize you've gained access to a new peripheral, a robot arm sticking out of the front of your core. You proceed to" +
			" wave it around playfully until Frank returns.] " +
			"\n(Select any choice to continue...)" ,
			myYesAction, 
			myNoAction, 
			myCancelAction);
	}
		
	void IntroBadResponseOne() {

		modalPanel.Choice ("Frank: Looks like I still need need to make a few additional callibrations..." +
			"\n[Frank taps away at the colorful lights on your built-in console. You feel a ticklish sensation and " +
			"realize you've gained access to a new peripheral, a robot arm sticking out of the front of your core. You proceed to" +
			" wave it around playfully until Frank returns.]" +
			"\n(Select any choice to continue...)" ,
			myYesAction, 
			myNoAction, 
			myCancelAction);
	}
	void IntroChoiceTwo() {

		modalPanel.Choice ("Frank: I see you're having fun with your new toy. Now, could you please shake my hand?" +
			"\n[Frank gestures his prosthetic hand towards you.]" +
			"\n1. Larry: My pleasure. [Shake Frank's hand.] " +
			"\n2. Larry: I'm not your dog! [Slap Frank across the face.] ",
			myYesAction, 
			myNoAction, 
			myCancelAction);
	}
	void IntroGoodResponseTwo() {

		modalPanel.Choice ("Frank: Pleased to meet you good sir. " +
			"\n[Frank returns in kind and then releases.]" +
			"\nFrank: I've given you access to the drone. You should be able to see the entire ship. " +
			"If you hold down the left mouse button the drone will follow your pointer. Click the 'Interact' " +
			"button if you want to talk to someone or examine something. Now, why don't you go see what everyone else is up to? " +
			"\n(Select any choice to continue...)"	,
			myYesAction, 
			myNoAction, 
			myCancelAction);
	}
	void IntroBadResponseTwo() {

		modalPanel.Choice ("Frank: Ow! Why do you have to make this so difficult Larry?" +
		"\n[Frank holds his prosthetic hand to his face and walks back towards the console to make more callibrations.] " +
		"\nFrank: There. I think that should do it. I've given you access to the drone. You should be able to see the entire " +
		"ship. If you hold down the left mouse button the drone will follow your pointer. Click the 'Interact' button if you want to " +
		"talk to someone or examine something. Now, go make friends with the crew and work on your attitude mister! " +
		"\n(Select any choice to continue...)",
			myYesAction, 
			myNoAction, 
			myCancelAction);
	}
	void FrankRegular() {

		modalPanel.Choice ("[Frank is so deep in thought he does not notice your drone hovering behind him.]" +
			"\n1. Larry: Hello Frank. What are you working on?" +
			"\n2. [Leave without saying anything.]",
			myYesAction, 
			myNoAction, 
			myCancelAction);
	}
	void FrankApology() {
		modalPanel.Choice ("[Frank is so deep in thought he does not notice your drone hovering behind him.]" +
			"\n1. Larry: Frank about earlier... I'm sorry I hit you." +
			"\n2. [Leave without saying anything.]",
			myYesAction, 
			myNoAction, 
			myCancelAction);
	}
	void FrankRegularResponse() {

		modalPanel.Choice ("Frank: I'm just monitoring your core and making sure you don't shut down. The others are probably doing much " +
			"more interesting things." +
			"\n (Select any choice to continue...)",
			myYesAction, 
			myNoAction, 
			myCancelAction);
	}
	void FrankApologyResponse() {
		modalPanel.Choice ("Frank: Aww... that's alright Larry. You know I love ya. I'm sure you were just playing around and it didn't " +
			"hurt... that bad. You can make it up to me by being friendly towards the others." +
			"\n (Select any choice to continue...)",
			myYesAction, 
			myNoAction, 
			myCancelAction);
	}
	void FrankGeneric() {
		modalPanel.Choice ("[Frank is so deep in thought he does not notice your drone hovering behind him.]" +
			"\n (Select any choice to continue...)" ,
			myYesAction, 
			myNoAction, 
			myCancelAction);
	}
	void MarkDecisionOne() {
		modalPanel.Choice ("[Mark is examining Hugh's foot and scribbling notes onto his tablet.]" +
			"\n1. [Scan Hugh's foot with the drone's on board x-ray vision.]" +
			"\n2. [Continue watching patiently.]" ,
			myYesAction, 
			myNoAction, 
			myCancelAction);
	}
	void MarkGoodResponseOne() {
		modalPanel.Choice ("Larry: Doctor Mark, I can see a small stress fracture on his third metatarsal." +
			    "\n[Mark turns around to acknowledge you.]" +
				"\nMark: Thank you nurse Larry. Would you mind fetching some medical gel from the life support " +
				"systems so I can take care of this?" +
				"\nHugh: My meta-whatsit? I knew I was dying! [Hugh sobs.] " +
				"\nMark: Please hurry." +
				"\n(Select any choice to continue...)" ,
			myYesAction, 
			myNoAction, 
			myCancelAction);
	}
	void MarkBadResponseOne() {
		modalPanel.Choice ("[Mark finally takes notice of your presence.]" +
			"\nMark: Oh Larry, good to see you. I've found a small stress fracture on Hugh's third metatarsal. Would you mind " +
			"fetching some medical gel from the life support systems so I can take care of this?" +
			"\nHugh: My meta-whatsit? I knew I was dying! [Hugh sobs.]" +
			"\nMark: Please hurry." +
			"\n(Select any choice to continue...)" ,
			myYesAction, 
			myNoAction, 
			myCancelAction);
	}
	void LifeSupportChoiceOne() {
		modalPanel.Choice ("[You check inside the cryogenic freezer and find a number of labeled medical gel dispensaries "+
			". You press the button beneath one and watch a small amount of purple, gelatinous substance oozes out. A stack of empty, " +
			"sterilized bottles line the doorway.]" +
			"\n1. [Fill an empty bottle with gel labeled 'bone putty'.]" +
			"\n2. [Fill an empty bottle with gel labeled 'painkiller'.]",
			myYesAction,
			myNoAction, 
			myCancelAction);
	}
	void ZephyrResponse() {
		modalPanel.Choice ("Larry: Look right here..." +
			"\nZephyr: Man it's always the little things! Oh well. Thanks. I knew I called you my partner for a reason. I'm the big" +
			" picture guy. You focus on the little details. Together we are unstoppable." +
			"\n(Select any choice to continue...)",
			myYesAction,
			myNoAction, 
			myCancelAction);
	}
	void ZephyrGeneric() {
		modalPanel.Choice ("Zephyr: Hey partner! No major bits on the wire, but if I see anything you'll be the first to know. " +
			"\n(Select any choice to continue...)",
			myYesAction,
			myNoAction, 
			myCancelAction);
	}
}