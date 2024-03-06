using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorGunBullet : Projectile
{

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
             //Call Enemy's Script
            Character ph = other.gameObject.GetComponent<Character>();
            ph.damage(damage);
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //TODO: Instantiate Particle Effects
        gameObject.SetActive(false);
    }

    protected override void Fire()
    {
        base.Fire();
        rb.AddForce(Random.Range(-15f, 15f) * transform.up + Random.Range(-15f, 15f) * transform.right, ForceMode.Impulse);
    }
}
