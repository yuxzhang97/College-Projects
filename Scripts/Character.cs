using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	// Use this for initialization

	public int StartHealth = 100;
	protected int health;
	protected bool isDead;

	public void damage(int dmg) {
		if(!isDead)
		{
			health -= dmg;
			Debug.Log(health);
			isDead = (health <= 0);
			if(isDead)
			{
				die();
			}
		}
	}

	public bool getDead()
	{
		return isDead;
	}

	virtual protected void die()
	{

	}

	virtual protected void init()
	{
		health = StartHealth;
		isDead = (health <= 0);
		//this.gameObject.tag = "Character";
	}

	void Start () {
		init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
