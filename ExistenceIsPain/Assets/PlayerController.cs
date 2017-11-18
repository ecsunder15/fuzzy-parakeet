
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	private int jumps = 0;
	public int maxJumps = 2;
	public float jumpSpeed = 10;

	public float delta = 3f;
	private float maxSpeed = 8f;
	private Transform player;
	private Vector3 velocity = Vector3.zero;

    public float health = 50f;
    private float maxHealth = 100f;
    private float healAmount = 5;
    private float damageAmount = 10;

    public Slider healthBar;

	private KeyCode right = KeyCode.D;
	private KeyCode left = KeyCode.A;
	private KeyCode jump = KeyCode.Space;
    private KeyCode reset = KeyCode.R;

	//KREEEEE
	Animator anim;
	public bool facingLeft;

	// Use this for initialization
	void Start () {
		player = this.gameObject.transform;

		//KREEEEEE
		anim = GetComponent<Animator>();
		facingLeft = false;
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKey (right)) {
			velocity.x += delta;
			if (velocity.x > maxSpeed) {
				velocity.x = maxSpeed;
			}
			if (velocity.x < 0)
				velocity.x = 0;

			//KREEEEEE
			facingLeft = false;

		} 
		else if (Input.GetKey (left)) {
			velocity.x -= delta;
			if (velocity.x < -1* maxSpeed) {
				velocity.x = -1* maxSpeed;
			}
			if (velocity.x > 0)
				velocity.x = 0;

			//KREEEEEE
			facingLeft = true;

		} 
		else {
			velocity.x = 0;
		}
			
		if (jumps < maxJumps && Input.GetKeyDown (jump)) {
			velocity.y += jumpSpeed;
			jumps += 1;
		} else {
			velocity.y *= .8f;
		}


		player.position += velocity * Time.deltaTime;


		//KREEEEE
		 
		{
			float move = velocity.x;
			//float move = Input.GetAxis ("Horizontal");
			anim.SetFloat ("Speed", move);
			anim.SetBool ("FacingLeft", facingLeft);
		}
		//unkree

	}

    void dealDamagae(float damage = 5)
    {
        health -= damage;
        updateHealth();
    }

    void heal(float damage = 5)
    {
        health += damage;
        updateHealth();
    }

    void updateHealth()
    {
        healthBar.value = health / maxHealth;
        checkHealth();
    }

    void checkHealth()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(1);
        }

    }

	void OnCollisionEnter2D(Collision2D collide){
		//grounded
		if (collide.gameObject.tag == "Ground") {
			jumps = 0;
		}

        if (collide.gameObject.tag == "Projectile")
        {
            dealDamagae();
        }
        if (collide.gameObject.tag == "Spikes")
        {
            dealDamagae();
        }

        if (collide.gameObject.tag == "Health")
        {
            heal();
        }
    }

    
}
