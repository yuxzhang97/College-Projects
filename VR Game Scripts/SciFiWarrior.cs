using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SciFiWarrior : Enemy {
    public bool walking = true;
    private bool shooting = false;
	private Animator animator;
    private WeaponShooter weapon;

    private NavMeshAgent agent;

    Transform playerTran;

	protected override void die () {
		setAnimatorFalse();
		animator.SetBool("Die", true);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        Destroy(gameObject, 4);
	}

	void setAnimatorFalse() {
		foreach(AnimatorControllerParameter param in animator.parameters)
		{
			animator.SetBool(param.name, false);
		}
	}

	// Use this for initialization
	void Start () {
		init();
		gameObject.tag = "Enemy";
		animator = this.gameObject.GetComponent<Animator>();
		setAnimatorFalse();
        weapon = GetComponentInChildren<WeaponShooter>(); 
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if(isDead)
        {
            return ;
        }
        if(!agent)
        {
            walking = false;
        }
        else
        {
            walking = (agent.velocity.magnitude > 0.1);
        }
        setAnimatorFalse();
		if (walking)
        {
            if(shooting)
            {
                animator.SetBool("WalkForward_Shoot",true);
            }
            else
            {
                animator.SetBool("Run_Guard",true);
            }
        }
        else
        {
            if(shooting)
            {
                animator.SetBool("Idle_Shoot",true);
            }
            else
            {
                animator.SetBool("Idle_GunMiddle", true);
            }
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if(isDead)
        {
            return ;
        }
        //Debug.Log(other.gameObject.name);
        if(other.tag == "Player")
        {
            shooting = true;
            transform.rotation = Quaternion.LookRotation(other.transform.position - transform.position);
            weapon.Shoot();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(isDead)
        {
            return ;
        }
        if(other.tag == "Player")
        {
            shooting = false;
        }
    }
}
