using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	// Use this for initialization
    protected override void init()
	{
		health = StartHealth;
		isDead = (health <= 0);
		//gameObject.tag = "Enemy";
	}
}