
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
    private float damageAmount = 100;
	private Scene scene;

    private float deadTime = 2f;
    private float timeOfDeath;

    private float startTime = 0f;
    private float timeAlive = 0f;

    public Slider healthBar;
    public Text timeText;
	public GameObject deadImage;
	public GameObject savedImage;

    private KeyCode right = KeyCode.D;
	private KeyCode left = KeyCode.A;
	private KeyCode jump = KeyCode.Space;
    private KeyCode reset = KeyCode.R;
	private KeyCode home = KeyCode.H;


	Animator anim;
	public bool facingLeft;
	public bool dead = false;
	public bool saved  = false;

	private CapsuleCollider2D playerCollider;

	public AudioClip MusicClip;
	public AudioSource MusicSource;

	public AudioClip MusicClip2;
	public AudioSource MusicSource2;

	// Use this for initialization
	void Start () {
		player = this.gameObject.transform;
        updateHealth();

		playerCollider = this.gameObject.GetComponent<CapsuleCollider2D>();

        startTime = Time.fixedTime;
        timeAlive = 0f;
		anim = GetComponent<Animator>();
		facingLeft = false;

		MusicSource.clip = MusicClip;
		MusicSource2.clip = MusicClip2;
		scene = SceneManager.GetActiveScene();
	}

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(reset))
        {
            string currentLevel = scene.name;
            SceneManager.LoadScene(currentLevel);
        }

		if (Input.GetKeyDown(home))
		{
			
			SceneManager.LoadScene("StartScreen");
		}

		if (dead || saved)
        {
            updateScene();
            return;
        }
        updateTime();

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

			MusicSource.Play ();

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

    private void dealDamage(float damage = 100)
    {
        health -= damage;
        updateHealth();
    }

    public void updateTime()
    {
        timeAlive = Time.fixedTime - startTime;
        timeText.text = "Time: " + timeAlive.ToString("F2");
    }

    private void heal(float damage = 5)
    {
        health += damage;
        updateHealth();
    }

    private void updateHealth()
    {
        healthBar.value = health / maxHealth;
        checkHealth();
    }

    private void checkHealth()
    {
		if (health >= 100f) {

			MusicSource2.Stop();
			timeOfDeath = Time.fixedTime;
			saved = true;
			savedImage.SetActive (true);
			anim.SetBool ("Died", true);
			return;
		}
		if (health <= 0) {

			MusicSource2.Stop();
			timeOfDeath = Time.fixedTime;
			dead = true;
			anim.SetBool ("Died", true);
			deadImage.SetActive (true);
			playerCollider.isTrigger = true;
		}

    }

    private void updateScene()
    {
        string currentLevel = scene.name;

        if (Time.fixedTime - timeOfDeath > deadTime)
        {
			if (dead) {
				switch (currentLevel) {
				case "Level1":
					SceneManager.LoadScene ("Level2");
					break;
				case "Level2":
					SceneManager.LoadScene ("Level3");
					break;
				case "Level3":
					SceneManager.LoadScene ("Level4");
					break;
				case "Level4":
					SceneManager.LoadScene ("Level5");
					break;
				case "Level5":
					SceneManager.LoadScene ("Level6");
					break;
				case "Level6":
					SceneManager.LoadScene ("Level7");
					break;
				case "Level7":
					SceneManager.LoadScene ("Level8");
					break;
				}
			}if (saved) {
				SceneManager.LoadScene(currentLevel);
			}
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
