using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemOpt : MonoBehaviour {

	void Start () {

        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration + GetComponent<ParticleSystem>().main.startLifetime.constantMax);                  // Destroy particle effect afer it stops
		
	}


}
