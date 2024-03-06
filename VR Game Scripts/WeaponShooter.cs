using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooter : MonoBehaviour {

    public Transform muzzle;
    public GameObject projectile;

    public float fireRate = 600f;
    private float lastFire;
    protected float fireInterval;

    // Use this for initialization
    void Start()
    {
        //Initialization
        lastFire = Time.time;
        fireInterval = 60 / fireRate;
        gameObject.tag = "Weapon";
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    public bool Shoot()
    {
        if(Time.time-lastFire > fireInterval)
        {
            Instantiate(projectile, muzzle.position, muzzle.rotation);
            AudioSource ad = GetComponent<AudioSource>();
            if (ad != null) ad.Play();
            lastFire = Time.time;
            return true;
        }
        return false;
    }

}
