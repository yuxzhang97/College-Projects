using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TabletButtonRestart : TabletButton {

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ButtonAction()
    {
        //Restart Action
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
