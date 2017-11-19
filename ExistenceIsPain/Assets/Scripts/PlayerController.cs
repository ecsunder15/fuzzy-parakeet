
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

	Animator anim;
	public bool facingLeft;

	public bool dead = false;


	// Use this for initialization
	void Start () {
		player = this.gameObject.transform;
        updateHealth();

		anim = GetComponent<Animator>();
		facingLeft = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(reset))
        {
            SceneManager.LoadScene("Level1");
        }

        if (dead)
        {
            return;
        }

		if (Input.GetKey (right)) {
			velocity.x += delta;
			if (velocity.x > maxSpeed) {
				velocity.x = maxSpeed;
			}
			if (velocity.x < 0)
				velocity.x = 0;

			facingLeft = false;

		} 
		else if (Input.GetKey (left)) {
			velocity.x -= delta;
			if (velocity.x < -1* maxSpeed) {
				velocity.x = -1* maxSpeed;
			}
			if (velocity.x > 0)
				velocity.x = 0;

			facingLeft = true;

		} 
		else {
			velocity.x = 0;
		}
			
		if (jumps < maxJumps && Input.GetKeyDown (jump)) {
			velocity.y += jumpSpeed;
			jumps += 1;

			//dealDamage();

		} else {
			velocity.y *= .8f;
		}
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            dealDamage();
        }

        player.position += velocity * Time.deltaTime;

		 
		{
			float move = velocity.x;
			anim.SetFloat ("Speed", move);
			anim.SetBool ("FacingLeft", facingLeft);
		}

	}

    void dealDamage(float damage = 5)
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
			dead = true;
			anim.SetBool ("Died", true);
            //SceneManager.LoadScene(1);

        }

    }

	void OnCollisionEnter2D(Collision2D collide){
		//grounded
		if (collide.gameObject.tag == "Ground") {
			jumps = 0;
		}

        if (collide.gameObject.tag == "Projectile")
        {
            dealDamage();
        }
        if (collide.gameObject.tag == "Spikes")
        {
            dealDamage();
        }

        if (collide.gameObject.tag == "Health")
        {
            heal();
        }
    }

    
}
