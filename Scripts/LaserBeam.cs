using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserBeam : MonoBehaviour {

    public float laserEndOffset = 0.05f;
    public float laserPtOffsetMultiplier = 0.25f;

    GameObject laserShadow;

    GameObject lefthand, righthand;

    OVRGrabbable ovr_grab;

    //AsyncOperation unload_async = null;
    public static List<string> loadedScenes = new List<string>();

    LineRenderer line;
    bool[] hover = { false, false, false };
    Animator[] animtr = new Animator[3];

    // Use this for initialization
    void Start () {
        line = GetComponent<LineRenderer>();
        laserShadow = GameObject.Find("laserFX");
        animtr[0] = GameObject.Find("ui_start").GetComponent<Animator>();
        animtr[1] = GameObject.Find("ui_options").GetComponent<Animator>();
        animtr[2] = GameObject.Find("ui_quit").GetComponent<Animator>();
        ovr_grab = GetComponentInParent<OVRGrabbable>();;
        lefthand = GameObject.Find("LeftHandAnchor");
        righthand = GameObject.Find("RightHandAnchor");

        SceneManager.sceneLoaded += SceneManager_SceneLoaded;
    }

    // Update is called once per frame
    void Update () {
        RaycastHit hit;
        Vector3 dir = transform.TransformDirection(Vector3.forward);
        if(Physics.Raycast(this.transform.position, dir, out hit))
        {
            //Generate Laser Beam
            line.SetPosition(1, Vector3.forward*(hit.distance - laserEndOffset));
            //print(hit.point);
            laserShadow.transform.position = hit.point - dir * laserPtOffsetMultiplier;

            //Scene Management
            if(hit.transform.name == "ui_start") {
                btn_hover(0);
            }
            else if(hit.transform.name == "ui_options") {
                btn_hover(1);
            }
            else if(hit.transform.name == "ui_quit") {
                btn_hover(2);
            }
            else if(hover[0]) {
                btn_away(0);
            }
            else if(hover[1]) {
                btn_away(1);
            }
            else if(hover[2]) {
                btn_away(2);
            }
        }

        bool laser_is_pressed = laser_press();

        if(hover[0] && (laser_is_pressed||Input.GetKeyDown(KeyCode.Space)))
        {
            SceneManager.LoadScene("Main");
        }
        else if (hover[1] && (laser_is_pressed || Input.GetKeyDown(KeyCode.Space)))
        {
            //SceneManager.LoadScene("Player");
        }
        else if(hover[2] && (laser_is_pressed || Input.GetKeyDown(KeyCode.Escape)))
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }


    void btn_hover(int index)
    {
        animtr[index].enabled = true;
        hover[index] = true;
        animtr[index].SetBool("btn_bool", true);
    }

    void btn_away(int index)
    {
        hover[index] = false;
        animtr[index].SetBool("btn_bool", false);
        //animt.enabled = false;
    }

    bool laser_press()
    {
        GameObject pick_hand;

        if(ovr_grab.isGrabbed)
        {
            pick_hand = ovr_grab.grabbedBy.gameObject;
        }
        else
        {
            return false;
        }

        if (pick_hand == lefthand)
        {
            return OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
        }
        else if (pick_hand == righthand)
        {
            return OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
        }
        return false;
    }

    void SceneManager_SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        loadedScenes.Add(arg0.name);
    }

}
