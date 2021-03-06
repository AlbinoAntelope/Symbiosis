﻿using UnityEngine;
using System.Collections;




public class PlayerShooting : MonoBehaviour {

	public GameObject bullet;
	public AudioClip shootSound;
	public GameObject Reg_bullet;
	public GameObject RayGun_bullet;

	public Object fireEffect;
	public Object iceEffect;
	public Object earthEffect;

	public float baseBulletSpeed = 10f;
	public float baseFireRate = 0.5f;

	public iAugment aug = null;

	private float nextFire = 0.0f;
	private string playerPrefix;

	private StatsManager playerStats;
	private float bulletSpeedModifier;
	private float fireRateModifier;

	public GameObject shakeCamera;
	public string weaponType;
	private GameObject cur_bullet;

	void Awake () {

		//Get the StatsManager Script
		playerStats = GetComponent<StatsManager> ();
	}

	// Use this for initialization
	void Start () {
		playerPrefix = gameObject.name;
		weaponType = "Pistol";
		cur_bullet = Reg_bullet;
	
	}
	
	// Update is called once per frame
	void Update () {


		//Get the stats for the player
		bulletSpeedModifier = playerStats.GetBulletSpeed ();
		fireRateModifier = playerStats.GetFireRate ();
	
		//Player Shooting
		if (Input.GetButton("FireRight" + playerPrefix) && Time.time > nextFire) 
		{
			Shoot(Vector3.right);
		}
		else if (Input.GetButton("FireDown" + playerPrefix) && Time.time > nextFire)
		{
			Shoot(Vector3.back);
		}
		else if (Input.GetButton("FireUp" + playerPrefix) && Time.time > nextFire) 
		{
			Shoot(Vector3.forward);
		}
		else if (Input.GetButton("FireLeft" + playerPrefix) && Time.time > nextFire) 
		{
			Shoot(Vector3.left);
		}
	}

	void Shoot (Vector3 shootDir) {
		shakeCamera.GetComponent<CameraShaker> ().shake = 0.1f;
		AudioSource.PlayClipAtPoint (shootSound, transform.position);

		aug = GetComponent<StatsManager> ().GetAugment ();
		weaponType = playerStats.weaponType;

		switch (weaponType) {

		case "Pistol":
			baseBulletSpeed = 10f;
			baseFireRate = 0.5f;
			cur_bullet = Reg_bullet;
			break;

		case "RayGun":
			baseBulletSpeed = 20f;
			baseFireRate = 0.000000001f;
			cur_bullet = RayGun_bullet;
			break;
		}
		//Create the bullet and launch it
		GameObject clone = Instantiate (cur_bullet, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation) as GameObject;
		clone.transform.rotation = Quaternion.LookRotation (shootDir);
		Physics.IgnoreCollision (clone.GetComponent<Collider> (), GetComponent<Collider> ());
		clone.GetComponent<Rigidbody> ().velocity = (clone.transform.forward * ((baseBulletSpeed + bulletSpeedModifier)));

		Debug.Log ("Firing augged bullet");

		if (aug != null) {
			Debug.Log ("Applying on-hit effects");
			clone.GetComponent<BulletBehavior> ().setAugment(aug);
			GameObject augEffect;
			if (aug.Element == "fire") {
				Debug.Log ("Applying fire effects");
				augEffect = Instantiate (fireEffect, clone.transform.position, Quaternion.identity) as GameObject;
				augEffect.transform.parent = clone.transform;
			}else if (aug.Element == "ice") {
				Debug.Log ("Applying ice effects");
				augEffect = Instantiate (iceEffect, clone.transform.position, Quaternion.identity) as GameObject;
				augEffect.transform.parent = clone.transform;
			}else if (aug.Element == "earth") {
				Debug.Log ("Applying earth effects");
				augEffect = Instantiate (earthEffect, clone.transform.position, Quaternion.identity) as GameObject;
				augEffect.transform.parent = clone.transform;
			}
		}

		//Set when the next bullet can be fired
		nextFire = Time.time + (baseFireRate + fireRateModifier);
	}
}
