using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs498
{
	public class HandGrab : MonoBehaviour {

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnTriggerStay(Collider other)
		{
			transform.root.gameObject.GetComponent<PlayerController>().childTrigger(other);
		}
	}
}