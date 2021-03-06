﻿using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public Transform p1_Transform;
	public Transform p2_Transform;

	public int moveSpeed;

	private Transform myTransform;

	void Awake () {
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		p1_Transform = GameObject.Find ("P1").transform;
		p2_Transform = GameObject.Find ("P2").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
			float dist_1 = Vector3.Distance(myTransform.position, p1_Transform.position);
			float dist_2 = Vector3.Distance(myTransform.position, p2_Transform.position);

			Transform target;

			if (dist_1 < dist_2) {
				target = p1_Transform;
			} else {
				target = p2_Transform;
			}
			//rotate to look at the player
			myTransform.LookAt(target);

			//move towards the player
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
	}
}
