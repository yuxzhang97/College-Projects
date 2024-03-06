using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TabletButtonQuit : TabletButton {

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public override void ButtonAction()
    {
        //QUIT Action
        SceneManager.LoadScene("Menu");
        // #if UNITY_EDITOR
        //     UnityEditor.EditorApplication.isPlaying = false;
        // #else
        //     Application.Quit();
        // #endif
    }
}
