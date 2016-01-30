﻿using UnityEngine;
using System.Collections;

public class Pickups : MonoBehaviour {

	public float rotateSpeed;

	private StatsManager playerStats;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {

		//Spin the Pickup
		transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {

		//Check if it is the player
		if (other.tag == "Player") {
			playerStats = other.GetComponent<StatsManager> ();

			//Check which pickup it is and apply effects
			switch (gameObject.name) 
			{
			case "SpeedUp":
				playerStats.SetSpeed (10f);
				break;
			case "FireRateUp":
				playerStats.SetFireRate (0.4f);
				break;
			case "BulletSpeedUp":
				playerStats.SetBulletSpeed (15f);
				break;
			}

			//Get rid of the pickup since it has been used
			Destroy (gameObject);
		}
	}
}