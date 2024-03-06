using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFSM : MonoBehaviour {

    public Text tutorialText;
    public GameObject HighlightFX;

    int currentState = -1;
    int numState = 6;
    TutorialState[] instructions = new TutorialState[] {
        new TutorialState("Welcome To Tutorial, Here you will learn how to navigate our stunning spaceship!\nNow, Try to grab and move the " +
        "left throttle , which is used for accelerate & decelerate, push it forward to accelerate.","accelerate"),
        new TutorialState("Pull it backward to decelerate your spaceship.","decelerate"),
        new TutorialState("Great! Next try the right stick (not the one on touch), that is an orientation (pitch and row) controller, use it now!","rotation"),
        new TutorialState("Don't forget you could also \"twist\" the stick to make ship to yaw!","yaw"),
        new TutorialState("Nice! Now try the shooting device! Use your trigger button on any contoller to shoot!","shoot"),
        new TutorialState("Excellent!! Seems like you got it! You are now able to navigate this ship, but be careful though!","intentionally blank")
      };

    class TutorialState
    {
        public string instruction;
        public string name;
        //float delay;
        public bool status = true;            //false for undergoing; true for ended
        //int type;
        //GameObject highlightObj;

        public TutorialState(string ins, string name)
        {
            this.instruction = ins;
            this.name = name;
        }

        //public bool GetStatus() { return this.status; }
    }

    public int MoveToNextState()
    {
        // Error code -1: Reach the end of tutorial
        if (currentState >= numState) return -1;

        // Error code -2: Previous state's text not all typed yet
        if (currentState>=0 && !instructions[currentState].status) return -2;

        currentState++;
        //tutorialText.text = instructions[currentState];
        StartCoroutine(TypedText(instructions[currentState]));
        return 0;
    }

    //public int GetCurrState()
    //{
    //    return currentState;
    //}

    public string GetCurrState()
    {
        return instructions[currentState].name;
    }

    IEnumerator TypedText(TutorialState state)
    {
        state.status = false;

        string text = state.instruction;
        tutorialText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            tutorialText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        state.status = true;
        if (currentState == numState-1) state.name = "end";
    }
}
