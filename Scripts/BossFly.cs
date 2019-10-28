using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFly : Enemy {


    Vector3 thrust;
    Rigidbody rb;

    float time;

    public float maxX = 300;
    public float maxY = 300;
    public float maxZ = 300;

    Quaternion rotation;

    protected override void die ()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 1);
    }

    void Start()
    {
        init();
        gameObject.tag = "Enemy";
        time = 0;
        thrust = new Vector3(0,0,0); 
        rb = GetComponent<Rigidbody>();
        rotation = transform.rotation;
    }

    void update()
    {
        transform.rotation = rotation;
    }

    void FixedUpdate()
    {

        time += Time.deltaTime;


        if (time > 1.0f)
        {
            thrust = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));


            if (this.transform.localPosition.z > maxX)
            {
                thrust.x =Random.Range(10f, 15.0f);
                
            }

            if (this.transform.localPosition.z < -maxX)
            {
                thrust.x = Random.Range(-15.0f, -10f);
            }

            if (this.transform.localPosition.y > maxY)
            {
                thrust.y = Random.Range(-15.0f, -10f);
               
            }

            if (this.transform.localPosition.y < -maxY)
            {
                thrust.y = Random.Range(10f, 15.0f);
                
            }

            if (this.transform.localPosition.x > maxZ)
            {
                thrust.z = Random.Range(-15.0f, -10f);
                
            }

            if (this.transform.localPosition.x < -maxZ)
            {
                thrust.z = Random.Range(10f, 15.0f);
               
            }

            
            rb.AddForce(thrust, ForceMode.Impulse);




            time = 0;

        

        }

        
    }
}
