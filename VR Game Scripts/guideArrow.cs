using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guideArrow : MonoBehaviour {

    public GameLogic gm;
    public GameObject player;

    CheckPoint nextcp;
    int nextcpIdx;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        nextcp = gm.checkpoints[gm.get_checkpoint_index()];
		if(nextcp)
        {
            Vector3 diff = nextcp.transform.position - player.transform.position;
            diff.y = 0;
            //Adjust Arrow
            float angle = Mathf.Rad2Deg * Mathf.Atan2(diff.z, diff.x);
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, angle + player.transform.rotation.eulerAngles.y);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
