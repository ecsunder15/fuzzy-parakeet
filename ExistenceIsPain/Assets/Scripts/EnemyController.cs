using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	//how far away and how fast we rotate
	public float rotateSpeed;
	public float radius;

	//health pack bullets
	public GameObject healthPrefab;
	public Transform healthSpawn;

	//point and angle at which we move around
	private Vector2 target;
	private float angle;

	//shot timer vars
	private float shotTimer;
	public float timeOfLastShot;
	public float waitingTime;

	// Use this for initialization
	public void Start () {
		target = transform.position;
	}
	
	// Update is called once per frame
	public void Update () {
		angle += rotateSpeed * Time.deltaTime;


		//set the circular angle point thing to rotate around
		var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
		transform.position = target + offset;

	}

	public void FixedUpdate() {
		shotTimer = Time.fixedTime;

		//fire a health pack every 2 seconds
		if (shotTimer - timeOfLastShot > 2) {
			timeOfLastShot = shotTimer;
			Fire ();
		}
	}

	void Fire() {
		// Create the Bullet from the Bullet Prefab
		GameObject healthPack = (GameObject)Instantiate (healthPrefab, healthSpawn.position, healthSpawn.rotation);

		// Add velocity to the bullet
		healthPack.GetComponent<Rigidbody2D>().velocity = transform.right * -7f;

		// Destroy the bullet after 3 seconds
		Destroy(healthPack, 3.0f);
	}

}
