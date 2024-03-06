using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour {

    protected Collider cld;
    protected Rigidbody rb;

    public float fireForce = 100f;
    public float lifeTime = 4f;
    public int damage = 10;
    public GameObject hitFX;

    // Use this for initialization
    void Start () {
        cld = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();


        //Fire to the forward orientation on instantiatation
        Fire();
        Destroy(gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {

    }

    protected virtual void Fire()
    {
        rb.AddForce(fireForce * transform.forward, ForceMode.Impulse);
    }

}
