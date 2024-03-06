using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponShooter : WeaponShooter {

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		if (Shoot())
		{
			fireInterval = 3 + Random.value * 5;
			fireRate = 60 / fireInterval;
		}
	}
}
