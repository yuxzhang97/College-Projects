using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherMissile : Projectile{

    public bool friendly = false;

    void OnCollisionEnter(Collision collision)
    {
        ExplosionDamage(this.transform.position, 20);

        if (friendly)
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
                    //a.GetComponent<AudioSource>().Play();
                    Destroy(a, 5);
                }

                gameObject.SetActive(false);
            }
            else if (collision.transform.tag == "Player") { }
            else
            {
                if (hitFX != null)
                {
                    //Instantiate Particle Effects
                    GameObject a = Instantiate(hitFX, collision.contacts[0].point, Quaternion.identity);
                    //a.GetComponent<AudioSource>().Play();
                    Destroy(a, 5);
                }
                gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.name == "PlayerModel")
            {
                //Call Enemy's Script
                Character player = collision.gameObject.GetComponent<Character>();
                player.damage(damage);

                if (hitFX != null)
                {
                    //Instantiate Particle Effects
                    GameObject a = Instantiate(hitFX, collision.contacts[0].point, Quaternion.identity);
                    //a.GetComponent<AudioSource>().Play();
                    Destroy(a, 5);
                }

                gameObject.SetActive(false);
            }
            else if (collision.transform.tag == "Enemy") { }
            else
            {
                if (hitFX != null)
                {
                    //Instantiate Particle Effects
                    GameObject a = Instantiate(hitFX, collision.contacts[0].point, Quaternion.identity);
                    //a.GetComponent<AudioSource>().Play();
                    Destroy(a, 5);
                }
                gameObject.SetActive(false);
            }
        }
    }


    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {

            if (hitColliders[i].gameObject.tag == "Enemy")
            {
                Enemy enemy = hitColliders[i].gameObject.GetComponent<Enemy>();
                enemy.damage(damage);
            }

            if (hitColliders[i].gameObject.name == "PlayerModel")
            {
                //Call Enemy's Script
                Character player = hitColliders[i].gameObject.GetComponent<Character>();
                player.damage(damage);


            }
            i++;
        }
    }
}
