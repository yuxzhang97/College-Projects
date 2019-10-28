using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nav : MonoBehaviour {

    public GameObject target;
    public NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (agent.gameObject.GetComponent<Character>().getDead())
        {
            this.gameObject.SetActive(false);
        }
        if (agent.gameObject.activeSelf)
        {
            agent.SetDestination(target.transform.position);
        }
	}
}
