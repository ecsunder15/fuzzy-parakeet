using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	//how far away and how fast we rotate
	public float rotateSpeed;
	public float radius;

	//point and angle at which we move around
	private Vector2 target;
	private float angle;

	// Use this for initialization
	public virtual void Start () {
		target = transform.position;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		angle += rotateSpeed * Time.deltaTime;

		//set the circular angle point thing to rotate around
		var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
		transform.position = _centre + offset;
	}

}
