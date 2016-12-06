﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalWindow : MonoBehaviour {
	private ModalPanel modalPanel;

	public UnityAction myYesAction;
	public UnityAction myNoAction;
	public UnityAction myCancelAction;

	GameObject drone;
	Drone droneScript;
	bool isCornholio = false;
	bool isLordOfStars = false;
	bool helpedZephyr = false;
	bool helpedMark = false;
	bool helpedCornelius = false;
	bool helpedShultz = false;

	enum QuestState {Initial, None, Frank, Mark, Life_Support_System, Zephyr, Shield, Engine, Turret, Shultz, Bar, Junkbox, Toolbox, Damage, Cornelius, Footlocker, Cornlocker};
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

			if (droneScript.getBranch (currentQuestState) == 1) {

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

			} else if (droneScript.getBranch (currentQuestState) == 6) {
				string objectives = "Active:";
				int completion = 0;

				if (!helpedMark)
					objectives += "\nInteract with Mark and Hugh in the Med Bay.";
				if (!helpedShultz)
					objectives += "\nInteract with Shultz in the Generator Room.";
				if (!helpedCornelius)
					objectives += "\nInteract with Cornelius on Bridge.";
				if (!helpedZephyr)
					objectives += "\nInteract with Zephyr in Sensor Room.";
				objectives += "\n\nCompleted:";
				if (helpedMark) {
					objectives += "\nHelped Mark assuage Hugh.";
					completion += 1;
				}
				if (helpedShultz) {
					objectives += "\nTook care of the hole in the 'hool' for Shultz.";
					completion += 1;
				}
				if (helpedCornelius) {
					objectives += "\nPlayed a game of black jack with Cornelius.";
					completion += 1;
				}
				if (helpedZephyr) {
					objectives += "\nSquashed a bug for Zephyr.";
					completion += 1;
				}

				objectives += "\n\n(Select any choice to continue...)";

				if (completion == 4)
					droneScript.updateBranch (currentQuestState, 7);

				modalPanel.Choice (objectives,
					myYesAction,
					myNoAction,
					myCancelAction);
			}
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
				"the usually gruff and serious Hugh cradles his naked, hairy foot with a pained expression.]" +
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
				"\nHugh: Whoopee! I feel like a million bucks. I bet I could run a marathon! Thanks Larry!" +
				"\n[Hugh wiggles his foot around.]" +
				"\nLarry: You are welcome." +
				"\nMark: Curious... usually this medicine doesn't take effect so quickly." +
				"\n(Select any choice to continue...)",
					myYesAction,
					myNoAction,
					myCancelAction);
			} else if (droneScript.getBranch (currentQuestState) == 7) {

				droneScript.updateBranch (currentQuestState, 8);

				modalPanel.Choice ("Mark: Thanks you again for your help with Hugh. He's always so difficult to deal with. " +
				"To him, every little ache and pain is sign of sure death or debilitating illness. " +
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

				modalPanel.Choice ("[Zephyr stares blankly at his terminal. Ocasionally he will type something in, grind his teeth" +
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

			modalPanel.Choice ("[You see the shield generator for the ship. This amorphous apparatus of automation hums softly as it " +
			"maintains the antilaser force field around the ship's hull.]" +
			"\n(Select any choice to continue...)",
				myYesAction,
				myNoAction,
				myCancelAction);

		} else if ((QuestState)droneScript.getQuestState () == QuestState.Engine) {

			currentQuestState = (int)QuestState.Engine;

			droneScript.DisableMovement ();

			modalPanel.Choice ("[You see the FTL drive of the ship. This monstrous mass of machinery roars loudly as it consumes " +
			"its dark matter fuel source.]" +
			"\n(Select any choice to continue...)",
				myYesAction,
				myNoAction,
				myCancelAction);
		} else if ((QuestState)droneScript.getQuestState () == QuestState.Turret) {

			currentQuestState = (int)QuestState.Turret;

			droneScript.DisableMovement ();

			modalPanel.Choice ("[You see the weapons control panel for the ship. Usually Hugh can be seen manning the guns, but he's not" +
			" at his post.]" +
			"\n(Select any choice to continue...)",
				myYesAction,
				myNoAction,
				myCancelAction);
		} else if ((QuestState)droneScript.getQuestState () == QuestState.Shultz) {

			currentQuestState = (int)QuestState.Shultz;

			droneScript.DisableMovement ();

			if (droneScript.getBranch (currentQuestState) == 1) {
				modalPanel.Choice ("[Shultz and his tools are haphazardly sprawled out on the floor of the generator room.]" +
				"\nShultz: Larry ish dat yoo? I sheem to have mishplashed mah hool paching kit." +
				"\n1. Larry: Someone's had a little too much to drink... again." +
				"\n2. [You let out a long sigh.]" +
				"\nLarry: Where is it? ",
					myYesAction,
					myNoAction,
					myCancelAction);
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				modalPanel.Choice ("Shultz: Lasht thing ah remember ish grabbin a mixer from the bar. Everything after that ish" +
				" fushy..." +
				"\n(Select any choice to continue...)",
					myYesAction,
					myNoAction,
					myCancelAction);
			} else if (droneScript.getBranch (currentQuestState) == 3) {
				droneScript.updateBranch (currentQuestState, 4);
				modalPanel.Choice ("[Shultz is fast asleep.]" +
				"\nShultz: ZZZ... Ah gotta patch the hole in the hool... ZZZ..." +
				"\n(Select any choice to continue...)",
					myYesAction,
					myNoAction,
					myCancelAction);
			} else if (droneScript.getBranch (currentQuestState) == 5) {
				droneScript.updateBranch (currentQuestState, 7);
				modalPanel.Choice ("[Shultz yawns and checks his maintenance tablet. He appears to have sobered up.]" +
				"\nShultz: Oh. you fixed the hull. Thanks for covering for me Larry. I promise, starting tomorrow, I'll be a new man." +
				" No more drinking on the job." +
				"\nLarry: Uh-huh. Heard that one before." +
				"\nShultz: I mean it this time! " +
				"\n(Select any choice to continue...)",
					myYesAction,
					myNoAction,
					myCancelAction);
			} else if (droneScript.getBranch (currentQuestState) == 6) {
				droneScript.updateBranch (currentQuestState, 7);
				modalPanel.Choice ("Shultz: Hey Larry! Want to go get a drink? " +
				"\nLarry: Dammit Shultz..." +
				"\n(Select any choice to continue...)",
					myYesAction,
					myNoAction,
					myCancelAction);
			}

		} else if ((QuestState)droneScript.getQuestState () == QuestState.Bar) {

			currentQuestState = (int)QuestState.Bar;

			droneScript.DisableMovement ();

			if (droneScript.getBranch (currentQuestState) == 1) {

				if (droneScript.getBranch ((int)QuestState.Shultz) == 2) {
					droneScript.updateBranch (currentQuestState, 2);
				}

				modalPanel.Choice ("[You see the common lounge's bar and kitchen. You do not need to eat to survive, but you were built with taste" +
				" sensors as a learning implement so you find it fun to try the vast array of premium snacks and drinks on offer here.]" +
				"\n(Select any choice to continue...)",
					myYesAction,
					myNoAction,
					myCancelAction);
			}
		} else if ((QuestState)droneScript.getQuestState () == QuestState.Junkbox) {

			currentQuestState = (int)QuestState.Junkbox;

			droneScript.DisableMovement ();

			if (droneScript.getBranch (currentQuestState) == 1) {

				if (droneScript.getBranch ((int)QuestState.Shultz) == 2) {
					droneScript.updateBranch (currentQuestState, 2);
				}

				modalPanel.Choice ("[You find a box of gadgets and and gizmos aplenty, some whosits and whatsits galore, and also at " +
				"least twenty thingamabobs.]" +
				"\n(Select any choice to continue...)",
					myYesAction,
					myNoAction,
					myCancelAction);
			}
		} else if ((QuestState)droneScript.getQuestState () == QuestState.Toolbox) {

			currentQuestState = (int)QuestState.Toolbox;

			droneScript.DisableMovement ();

			if (droneScript.getBranch (currentQuestState) == 1) {

				if (droneScript.getBranch ((int)QuestState.Shultz) == 2) {
					droneScript.updateBranch (currentQuestState, 2);
				}

				modalPanel.Choice ("[You find a box of gadgets and and gizmos aplenty, some whosits and whatsits galore, and also at " +
				"least twenty thingamabobs.]" +
				"\n(Select any choice to continue...)",
					myYesAction,
					myNoAction,
					myCancelAction);
			}
		} else if ((QuestState)droneScript.getQuestState () == QuestState.Damage) {

			currentQuestState = (int)QuestState.Damage;

			droneScript.DisableMovement ();

			if (droneScript.getBranch (currentQuestState) == 1) {

				if (droneScript.getBranch ((int)QuestState.Shultz) == 3) {
					droneScript.updateBranch (currentQuestState, 2);
				}

				modalPanel.Choice ("[You see a gaping hole in the hull of the ship. It must have happened when you and the crew were" +
				" passing through that last asteroid field. Luckily, it looks like it didn't breach the inner hull, but it should be" +
				" patched before an further damage occurs.]" +
				"\n(Select any choice to continue...)",
					myYesAction,
					myNoAction,
					myCancelAction);
			}
		} else if ((QuestState)droneScript.getQuestState () == QuestState.Cornelius) {

			currentQuestState = (int)QuestState.Cornelius;

			droneScript.DisableMovement ();

			if (droneScript.getBranch (currentQuestState) == 1) {

				droneScript.updateBranch (currentQuestState, 3);

				modalPanel.Choice ("[Captain Cornelius is slouching in his oversized Captain's chair and gazing blankly into the " +
				"vastness of space.]" +
				"\nLarry: Captain." +
				"\n[Cornelius turns to face you.]" +
				"\nCornelius: Larry." +
				"\n[Cornelius lets out a deep sigh.]" +
				"\nCornelius: I'm so bored! No action since that last asteroid field we went through. Want to help relieve your" +
				" Captain of some boredom and grab a deck of cards out of my footlocker in the sleeping quarters?" +
				"\n(Select any choice to continue...) ",
					myYesAction,
					myNoAction,
					myCancelAction);
			} else if (droneScript.getBranch (currentQuestState) == 2) {

				droneScript.updateBranch (currentQuestState, 3);

				modalPanel.Choice ("Cornelius: I'm dying... of boredom. Quick Larry. Before it's too late... " +
				"\nLarry: Your vitals look fine to me." +
				"\n[Cornelius plays dead.]" +
				"\n(Select any choice to continue...)",
					myYesAction,
					myNoAction,
					myCancelAction);
			} else if (droneScript.getBranch (currentQuestState) == 4) {

				modalPanel.Choice ("Cornelius: Ah my alien deck. A fine choice Larry. Now let's play some black jack, but first, we " +
				"should make things a little more interesting. If I win, you have to call me 'Master Cornelius, Lord of the Stars'. What " +
				"do you want if you win?" +
				"\n1. Larry: I get to call you Captain Cornholio." +
				"\n2. Larry: I get to be Captain for the day.",
					myYesAction,
					myNoAction,
					myCancelAction);
			} else if (droneScript.getBranch (currentQuestState) == 6) {

				if (isLordOfStars) {
					modalPanel.Choice ("Cornelius: Larry." +
						"\nLarry: Master Cornelius, Lord of the Stars.",
						myYesAction,
						myNoAction,
						myCancelAction);
				}
				else if (isCornholio) {
					modalPanel.Choice ("Cornelius: Larry." +
						"\nLarry: Cornholio.",
						myYesAction,
						myNoAction,
						myCancelAction);
				}
				else {
					modalPanel.Choice ("Cornelius: Captain." +
						"\nLarry: Cornelius.",
						myYesAction,
						myNoAction,
						myCancelAction);
				}
			}
		} else if ((QuestState)droneScript.getQuestState () == QuestState.Footlocker) {

			currentQuestState = (int)QuestState.Footlocker;

			droneScript.DisableMovement ();

			if (droneScript.getBranch (currentQuestState) == 1) {

				if (droneScript.getBranch ((int)QuestState.Cornelius) == 2) {
					droneScript.updateBranch (currentQuestState, 2);
				}

				modalPanel.Choice ("[The footlocker contains dirty underwear and other worthless personal effects.]" +
				"\n(Select any choice to continue...)",
					myYesAction,
					myNoAction,
					myCancelAction);
			}
		} else if ((QuestState)droneScript.getQuestState () == QuestState.Cornlocker) {

			currentQuestState = (int)QuestState.Cornlocker;

			droneScript.DisableMovement ();

			if (droneScript.getBranch (currentQuestState) == 1) {

				if (droneScript.getBranch ((int)QuestState.Cornelius) == 2) {
					droneScript.updateBranch (currentQuestState, 2);
				}

				modalPanel.Choice ("[The footlocker contains dirty underwear and other worthless personal effects.]" +
				"\n(Select any choice to continue...)",
					myYesAction,
					myNoAction,
					myCancelAction);
			}
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
				droneScript.updateBranch (currentQuestState, 6);
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 7) {
				FinalMessage ();
				droneScript.updateBranch (droneScript.getQuestState (), 8);
			} else if (droneScript.getBranch (currentQuestState) == 8) {
				Exit ();
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
				helpedMark = true;
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
				helpedZephyr = true;
				modalPanel.ClosePanel ();
				droneScript.EnableMovement ();
			}
		} else if ((QuestState)currentQuestState == QuestState.Shield) {
			droneScript.EnableMovement ();
			modalPanel.ClosePanel ();
		} else if ((QuestState)currentQuestState == QuestState.Engine) {
			droneScript.EnableMovement ();
			modalPanel.ClosePanel ();
		} else if ((QuestState)currentQuestState == QuestState.Turret) {
			droneScript.EnableMovement ();
			modalPanel.ClosePanel ();
		} else if ((QuestState)currentQuestState == QuestState.Shultz) {
			if (droneScript.getBranch (currentQuestState) == 1) {
				ShultzGoodResponseOne ();
				droneScript.updateBranch (currentQuestState, 2);
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 4) {
				droneScript.updateBranch (currentQuestState, 3);
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 7) {
				helpedShultz = true;
				droneScript.updateBranch (currentQuestState, 6);
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			}
		} else if ((QuestState)currentQuestState == QuestState.Bar) {
			if (droneScript.getBranch (currentQuestState) == 1) {
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				BarQuestTrigger ();
				droneScript.updateBranch (currentQuestState, 1);
			}
		} else if ((QuestState)currentQuestState == QuestState.Junkbox) {
			if (droneScript.getBranch (currentQuestState) == 1) {
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				JunkboxQuestTrigger ();
				droneScript.updateBranch (currentQuestState, 1);
			}
		} else if ((QuestState)currentQuestState == QuestState.Toolbox) {
			if (droneScript.getBranch (currentQuestState) == 1) {
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				ToolboxQuestTrigger ();
				droneScript.updateBranch (currentQuestState, 1);
				droneScript.updateBranch ((int)QuestState.Shultz, 3);
			}
		} else if ((QuestState)currentQuestState == QuestState.Damage) {
			if (droneScript.getBranch (currentQuestState) == 1) {
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				DamageQuestTrigger ();
				droneScript.updateBranch (currentQuestState, 3);
			} else if (droneScript.getBranch (currentQuestState) == 3) {
				modalPanel.ClosePanel ();
				droneScript.EnableMovement ();
				droneScript.updateBranch ((int)QuestState.Shultz, 5);
				DestroyObject (GameObject.Find ("Damage"));
			}
		} else if ((QuestState)currentQuestState == QuestState.Cornelius) {
			if (droneScript.getBranch (currentQuestState) == 3) {
				droneScript.updateBranch (currentQuestState, 2);
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				DamageQuestTrigger ();
				droneScript.updateBranch (currentQuestState, 3);
			}  else if (droneScript.getBranch (currentQuestState) == 4) {
				isCornholio = true;
				Cornholio ();
				droneScript.updateBranch (currentQuestState, 5);
			}  else if (droneScript.getBranch (currentQuestState) == 5) {
				Bust();
				isLordOfStars = true;
				droneScript.updateBranch (currentQuestState, 6);
			} else if (droneScript.getBranch (currentQuestState) == 6) {
				helpedCornelius = true;
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			}
		} else if ((QuestState)currentQuestState == QuestState.Footlocker) {
			if (droneScript.getBranch (currentQuestState) == 1) {
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				FootlockerQuestTrigger ();
				droneScript.updateBranch (currentQuestState, 1);
			}
		} else if ((QuestState)currentQuestState == QuestState.Cornlocker) {
			if (droneScript.getBranch (currentQuestState) == 1) {
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				CornlockerQuestTrigger ();
				droneScript.updateBranch (currentQuestState, 1);
				droneScript.updateBranch ((int)QuestState.Cornelius, 4);
			}
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
			} else if (droneScript.getBranch (currentQuestState) == 7) {
				FinalMessage ();
				droneScript.updateBranch (droneScript.getQuestState (), 8);
			} else if (droneScript.getBranch (currentQuestState) == 7) {
				Exit ();
			}
		} else if ((QuestState)currentQuestState == QuestState.Frank) {

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
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				MarkBadResponseOne ();
				droneScript.updateBranch (currentQuestState, 3);
			} else if (droneScript.getBranch (currentQuestState) == 3) {
				modalPanel.ClosePanel ();
				droneScript.EnableMovement ();
				droneScript.updateBranch (currentQuestState, 4);
			} else if (droneScript.getBranch (currentQuestState) == 8) {
				helpedMark = true;
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
				helpedZephyr = true;
				modalPanel.ClosePanel ();
				droneScript.EnableMovement ();
			}
		} else if ((QuestState)currentQuestState == QuestState.Shield) {
			droneScript.EnableMovement ();
			modalPanel.ClosePanel ();
		} else if ((QuestState)currentQuestState == QuestState.Engine) {
			droneScript.EnableMovement ();
			modalPanel.ClosePanel ();
		} else if ((QuestState)currentQuestState == QuestState.Turret) {
			droneScript.EnableMovement ();
			modalPanel.ClosePanel ();
		} else if ((QuestState)currentQuestState == QuestState.Shultz) {
			if (droneScript.getBranch (currentQuestState) == 1) {
				ShultzBadResponseOne ();
				droneScript.updateBranch (currentQuestState, 2);
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 4) {
				droneScript.updateBranch (currentQuestState, 3);
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 7) {
				helpedShultz = true;
				droneScript.updateBranch (currentQuestState, 6);
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			}
		} else if ((QuestState)currentQuestState == QuestState.Bar) {
			if (droneScript.getBranch (currentQuestState) == 1) {
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				BarQuestTrigger ();
				droneScript.updateBranch (currentQuestState, 1);
			}
		} else if ((QuestState)currentQuestState == QuestState.Junkbox) {
			if (droneScript.getBranch (currentQuestState) == 1) {
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				JunkboxQuestTrigger ();
				droneScript.updateBranch (currentQuestState, 1);
			}
		} else if ((QuestState)currentQuestState == QuestState.Toolbox) {
			if (droneScript.getBranch (currentQuestState) == 1) {
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				ToolboxQuestTrigger ();
				droneScript.updateBranch (currentQuestState, 1);
				droneScript.updateBranch ((int) QuestState.Shultz, 3);
			}
		} else if ((QuestState)currentQuestState == QuestState.Damage) {
			if (droneScript.getBranch (currentQuestState) == 1) {
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				DamageQuestTrigger ();
				droneScript.updateBranch(currentQuestState, 3);
			} else if (droneScript.getBranch (currentQuestState) == 3) {
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
				droneScript.updateBranch ((int)QuestState.Shultz, 5);
				DestroyObject(GameObject.Find("Damage"));
			}
		} else if ((QuestState)currentQuestState == QuestState.Cornelius) {
			if (droneScript.getBranch (currentQuestState) == 3) {
				droneScript.updateBranch (currentQuestState, 2);
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				DamageQuestTrigger ();
				droneScript.updateBranch (currentQuestState, 3);
			}  else if (droneScript.getBranch (currentQuestState) == 4) {
				Captain ();
				droneScript.updateBranch (currentQuestState, 5);
			}   else if (droneScript.getBranch (currentQuestState) == 5) {
				if (isCornholio) {
					CornholioStay ();
				}
				else {
					CaptainStay ();
				}
				droneScript.updateBranch (currentQuestState, 6);
			} else if (droneScript.getBranch (currentQuestState) == 6) {
				helpedCornelius = true;
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			}
		} else if ((QuestState)currentQuestState == QuestState.Footlocker) {
			if (droneScript.getBranch (currentQuestState) == 1) {
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				FootlockerQuestTrigger ();
				droneScript.updateBranch (currentQuestState, 1);
			}
		} else if ((QuestState)currentQuestState == QuestState.Cornlocker) {
			if (droneScript.getBranch (currentQuestState) == 1) {
				droneScript.EnableMovement ();
				modalPanel.ClosePanel ();
			} else if (droneScript.getBranch (currentQuestState) == 2) {
				CornlockerQuestTrigger ();
				droneScript.updateBranch (currentQuestState, 1);
				droneScript.updateBranch ((int)QuestState.Cornelius, 4);
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
			"\nFrank: It's time for your daily crew interaction. I've given you access to the drone. You should be able to see the entire ship. " +
			"If you hold down the left mouse button the drone will follow your pointer. Click the 'Interact' " +
			"button if you want to talk to someone or examine something. " +
			"Interact with the drone pad to check your objectives and once you're done. Now go make friends with the crew! " +
			"\n(Select any choice to continue...)"	,
			myYesAction,
			myNoAction,
			myCancelAction);
	}
	void IntroBadResponseTwo() {

		modalPanel.Choice ("Frank: Ow! Why do you have to make this so difficult Larry?" +
		"\n[Frank holds his prosthetic hand to his face and walks back towards the console to make more callibrations.] " +
		"\nFrank: There. I think that should do it. It's time for your daily crew interaction. I've given you access to the drone. You should be able to see the entire " +
		"ship. If you hold down the left mouse button the drone will follow your pointer. Click the 'Interact' button if you want to " +
		"talk to someone or examine something. Interact with the drone pad to check your objectives and once you're done. For now go make friends with " +
		" the crew and work on your attitude mister!  " +
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
			"\n(Select any choice to continue...)",
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
			"\nMark: Oh Larry. Good to see you. I've found a small stress fracture on Hugh's third metatarsal. Would you mind " +
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
			". You press the button beneath one and watch as a small amount of purple, gelatinous substance oozes out. A stack of empty, " +
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
	void ShultzGoodResponseOne() {
		modalPanel.Choice ("[Shultz lets out an irritating cackle.]" +
			"\nShultz: I gotsh into mah shpecial brew. Been shaving it fer a shpecial occashion." +
			"\n [Shultz's face turns serious... at least as serious as it can get in his condition.]" +
			"\nShultz: Lishten Larry. If ah don't find that kit the Cap ish gonna tan mah hide. Lasht thing ah remember ish" +
			" going into the bar in the common lounch fer a mixer." +
			"\n(Select any choice to continue...)",
			myYesAction,
			myNoAction,
			myCancelAction);
	}
	void ShultzBadResponseOne() {
		modalPanel.Choice ("Shultz: Where'sh wat now?" +
			"\nLarry: The hull patching kit." +
			"\nShultz: Oh yesh... well... Ah remember grabbin a mixer fer mah shpecial brew from the bar and then... Ah don't know." +
			"\n(Select any choice to continue...)",
			myYesAction,
			myNoAction,
			myCancelAction);
	}
	void BarQuestTrigger() {
		modalPanel.Choice ("[Your olfactory sensors pick up the trail of a stench matching Shultz's 'shpecial brew' coming from the" +
			" other side of the room.]" +
			"\n(Select any choice to continue...)",
			myYesAction,
			myNoAction,
			myCancelAction);
	}
	void JunkboxQuestTrigger() {
		modalPanel.Choice ("[This is not the junk you're looking for.]" +
			"\n(Select any choice to continue...)",
			myYesAction,
			myNoAction,
			myCancelAction);
	}
	void ToolboxQuestTrigger() {
		modalPanel.Choice ("[At the bottom of the box is an odoriferous bag containing astral epoxy and tools of stellar origin. This must be it.]" +
			"\n(Select any choice to continue...)",
			myYesAction,
			myNoAction,
			myCancelAction);
	}
	void DamageQuestTrigger() {
		modalPanel.Choice ("[You use the drone's robot arm to drop a glob of epoxy and pull a smoothing tool out of the bag to " +
			"spread it over the hole under cover of a foldable antivacuum maintenance hood. At first it appears to be a bland, gray paste, " +
			"but it quickly hardens and takes on the molecular structure of the material around it. Within moments the spot on the hull looks " +
			"good as new.]" +
			"\n(Select any choice to continue...)",
			myYesAction,
			myNoAction,
			myCancelAction);
	}
	void FootlockerQuestTrigger() {
		modalPanel.Choice ("[You find credit cards, an identity card, but no playing cards. Worthless.]" +
			"\n(Select any choice to continue...)",
			myYesAction,
			myNoAction,
			myCancelAction);
	}
	void CornlockerQuestTrigger() {
		modalPanel.Choice ("[You find a stack of playing card decks. You take the one that has a big purple alien on the front.]" +
			"\n(Select any choice to continue...)",
			myYesAction,
			myNoAction,
			myCancelAction);
	}
	void Cornholio() {
		modalPanel.Choice ("[Cornelius stretches his shirt over his head and proceeds to deal out two cards to you. The first is a jack with" +
			" an illustration of an orange alien balanced upon many legs. The second has an illustration of six creepy alien eyes.]" +
			"\n1. Larry: Hit me." +
			"\n2. Larry: Stay.",
			myYesAction,
			myNoAction,
			myCancelAction);
	}
	void Captain() {
		modalPanel.Choice ("Cornelius: Sure. What's the worst that could happen?" +
			"\n[Cornelius deals out two cards to you. The first is a jack with" +
			" an illustration of an orange alien balanced upon many legs. The second has an illustration of six creepy alien eyes.]" +
			"\n1. Larry: Hit me." +
			"\n2. Larry: Stay.",
			myYesAction,
			myNoAction,
			myCancelAction);
	}
	void Bust() {
		modalPanel.Choice ("[Captain Cornelius deals you another card. This time it's a king with an illustration of a blue alien. Its " +
			"fangs are dripping green acid. You bust and show Cornelius your hand. This prompts a smug response.]" +
			"\nCornelius: What's my name?" +
			"\nLarry: Master Cornelius, Lord of the Stars." +
			"\nCornelius: That's right." +
			"\n(Select any choice to continue...)",
			myYesAction,
			myNoAction,
			myCancelAction);
	}
	void CornholioStay() {
		modalPanel.Choice ("[Cornelius has a queen with an illustration of a pink alien held aloft by pointed wings. His other card has" +
			" two stars on it. When he draws the next card he pulls a king with an illustration of a blue alien. Its fangs are dripping " +
			"green acid. He busts and plants his face firmly into his palm.]" +
			"\nCornelius: I am the great Captain Cornholio. I need teepee for my porthole." +
			"\nLarry: Get it yourself." +
			"\n(Select any choice to continue...)",
			myYesAction,
			myNoAction,
			myCancelAction);
	}
	void CaptainStay() {
		modalPanel.Choice ("[Cornelius has a queen with an illustration of a pink alien held aloft by pointed wings. His other card has" +
			" two stars on it. When he draws the next card he pulls a king with an illustration of a blue alien. Its fangs are dripping " +
			"green acid. He busts, plants his face firmly into his palm and places his hat on your drone.]" +
			"\nCornelius: Don't burn her down." +
			"\nLarry: No promises." +
			"\n(Select any choice to continue...)",
			myYesAction,
			myNoAction,
			myCancelAction);
	}
	void FinalMessage() {
		modalPanel.Choice ("Frank: Look like you had fun Larry. I'm glad. Time to shut off the drone now." +
		"\n[Frank clears his throat.]" +
		"\nFrank: Hey you! The one behind the fourth wall! Thanks for playing TheFiveHorseman's level 1 demo of SinguLarry." +
		" We hope you enjoyed your time with it and have a wonderful rest of your day!" +
		"\nLarry: Bye bye!" +
		"\n(Select any choice to exit...)",
			myYesAction,
			myNoAction,
			myCancelAction);
	}
	void Exit() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}
}
