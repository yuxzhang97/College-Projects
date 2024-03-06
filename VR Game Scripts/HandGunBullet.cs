using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunBullet : Projectile {

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Call Enemy's Script
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.damage(damage);

            if (hitFX != null)
            {
                //Instantiate Particle Effects
                GameObject a = Instantiate(hitFX, collision.contacts[0].point, Quaternion.identity);
                a.GetComponent<AudioSource>().Play();
                Destroy(a, 5);
            }

            gameObject.SetActive(false);
        }
        else if(collision.transform.tag == "Player") { }
        else
        {
            if(hitFX != null)
            {
                //Instantiate Particle Effects
                GameObject a = Instantiate(hitFX, collision.contacts[0].point, Quaternion.identity);
                a.GetComponent<AudioSource>().Play();
                Destroy(a, 5);
            }
            gameObject.SetActive(false);
        }
    }
}
