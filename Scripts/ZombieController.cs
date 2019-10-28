using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : Enemy {

    public GameObject target;

    public float AttackInterval = 1;

    public int dmg = 20;

    Animator animator;
    Vector3 zombPosition;

    private float lastAttack;

    protected override void die ()
    {
        setAnimatorFalse();
        animator.SetBool("Die", true);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Destroy(gameObject, 4);
    }

    void setAnimatorFalse()
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            animator.SetBool(param.name, false);
        }
    }

    // Use this for initialization
    void Start () {
        init();
        gameObject.tag = "Enemy";
        animator = GetComponent<Animator>();
        setAnimatorFalse();
        animator.SetBool("Walk", true);
        zombPosition = this.transform.position;
        lastAttack = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3 relativePos = target.transform.position - this.transform.position;

        zombPosition.y = 0.5f;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = new Quaternion(0,rotation.y,0,rotation.w);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == target.name)
        {
            animator.SetBool("CloseToCylinder", true);
            if(Time.time - lastAttack > AttackInterval)
            {
                other.gameObject.GetComponent<Character>().damage(dmg);
                lastAttack = Time.time;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == target.name)
        {
            animator.SetBool("CloseToCylinder", false);
        }
    }
}
