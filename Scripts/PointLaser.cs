using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLaser : MonoBehaviour {

    //public GameObject laserShadowFX;
    //GameObject laserEndPt;
    LineRenderer laserLine;

    bool pressed;
    TabletButton fish_mem_btn;

    // Use this for initialization
    void Start () {
        //laserEndPt = null;
        laserLine = GetComponent<LineRenderer>();
        pressed = false;
        fish_mem_btn = null;
    }
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.Touch) || Input.GetKeyDown(KeyCode.Tab)) pressed = true;
        else pressed = false;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 dir = transform.forward;
        if (Physics.Raycast(this.transform.position, dir, out hit, LayerMask.NameToLayer("UI")))
        {
            //Generate Laser Beam
            laserLine.SetPosition(1, Vector3.forward * hit.distance);

            //Generate Laser EndPoint
            //if (laserEndPt == null) laserEndPt = Instantiate(laserShadowFX);
            //laserEndPt.SetActive(true);
            //laserEndPt.transform.position = hit.point;


            //Trigger Action If applicable
            TabletButton btn = hit.transform.gameObject.GetComponent<TabletButton>();
            Debug.Log(hit.transform.gameObject.name);
            if (btn)
            {
                btn.ButtonOn();
                fish_mem_btn = btn;
            }
            else if(fish_mem_btn)
            {
                fish_mem_btn.ButtonOff();
                fish_mem_btn = null;
            }
            if (btn && pressed)
            {
                btn.ButtonAction();
                fish_mem_btn = null;
            }

        }
        //else if (laserEndPt)
        //{
        //    laserEndPt.SetActive(false);
        //}
    }
}
