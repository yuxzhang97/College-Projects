using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurret : MonoBehaviour {

    private WeaponShooter weapon;
    public GameObject target;
    public GameObject muzzle;

    // Use this for initialization
    void Start () {
        weapon = GetComponent<WeaponShooter>();
        //muzzle = this.transform.Find("turret_muzzle").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(target.transform);
        muzzle.transform.LookAt(target.transform);
        
		
	}
}
