using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EcoCat : MonoBehaviour {

    public Collider2D bottleDepotCollider2D;
    private CircleCollider2D catCollider2D;
//    public Collider2D treeCollider2D;

	private Rigidbody2D rigidBody2D;
	public IObservable<bool> FacingRight;
	private ReactiveProperty<int> numCansCollected = new ReactiveProperty<int> (0);
    private ReactiveProperty<int> numBottlesCollected = new ReactiveProperty<int> (0);
	private AudioSource canSound;
	private AudioSource jumpSound;
	private AudioSource plantTreeSound;
	private AudioSource ecoCatSound;

	private ReactiveProperty<bool> isOnGround = new ReactiveProperty<bool>(false);
	private ReactiveProperty<int> numSeedsCollected = new ReactiveProperty<int>(0);
	private ReactiveProperty<float> hungerLevel = new ReactiveProperty<float> (1);

	public GameObject tree;
	public ReadOnlyReactiveProperty<bool> IsCatWalking;

	public CarbonLevel carbonLevel;

	public ReadOnlyReactiveProperty<bool> IsOnGround {
		get {
			return isOnGround.ToReadOnlyReactiveProperty ();
		}
	}

	public ReadOnlyReactiveProperty<int> NumCanCollected {
		get {
			return numCansCollected.ToReadOnlyReactiveProperty();
		}
	}
    public ReadOnlyReactiveProperty<int> NumBottlesCollected {
        get {
            return numBottlesCollected.ToReadOnlyReactiveProperty();
        }
    }
    public ReadOnlyReactiveProperty<int> NumSeedsCollected {
        get {
            return numSeedsCollected.ToReadOnlyReactiveProperty();
        }
    }
	public ReadOnlyReactiveProperty<float> HungerLevel {
		get {
			return hungerLevel.DistinctUntilChanged().ToReadOnlyReactiveProperty();
		}
	}

	void Awake() {
		rigidBody2D = GetComponent<Rigidbody2D> ();

		IsCatWalking = Observable
			.EveryUpdate ()
			.Select (_ => Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
			.ToReadOnlyReactiveProperty ();

		FacingRight = Observable
			.EveryUpdate()
			.Where(_ => Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
			.Select(_ => Input.GetKeyDown(KeyCode.RightArrow))
			.AsObservable();

        catCollider2D = GetComponent<CircleCollider2D>();
		var audioSources = GetComponents<AudioSource> ();
		canSound = audioSources[0];
		jumpSound = audioSources [1];
		plantTreeSound = audioSources [2];
		ecoCatSound = audioSources [3];
	}

	void Start() {
		var jumpCounter = 0;

		InputManager.Instance.Jump
			.Do(_ => jumpCounter++)
			.Do(_ => {
				if((jumpCounter % 3) == 0) {
					ecoCatSound.Play();
				} else {
					jumpSound.Play();
				}
			})
			.Do(_ => isOnGround.Value = false)
			.Subscribe (_ => {
			var originalVelocity = rigidBody2D.velocity;
			rigidBody2D.velocity = new Vector2(originalVelocity.x, 2.5f);
		}).AddTo(this);

		InputManager.Instance.HorizontalForce.Subscribe (force => {
			rigidBody2D.AddForce(Vector2.right * force * 8);
		}).AddTo (this);

		HungerLevel
			.Where(hungerLevel => hungerLevel <= 0)
			.Subscribe (_ => {
				// CAT Dies :(
				GameManager.Instance.LoadScene("gameOver");
		}).AddTo (this);
			
		Observable.EveryUpdate()
			.Where(_ => carbonLevel.carbonLevelFull.Value)
			.Subscribe (_ => {
				var decreaseAmount = Time.deltaTime / 20f;
				hungerLevel.Value = Mathf.Max(0f, hungerLevel.Value - decreaseAmount);
			});


	}

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Can") {
            Destroy(coll.gameObject);
            numCansCollected.Value++;
			canSound.Play ();
		} else if (coll.gameObject.tag == "Bottle") {
            Destroy(coll.gameObject);
            numBottlesCollected.Value++;
            canSound.Play(); // Change to bottle sound
        }
        else if (coll.gameObject.tag == "Land") {
			isOnGround.Value = true;
        }
    }

    // Using Update() for now, maybe change later
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            BuySeeds();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlantTree();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Pluck an apple if you jump, or at least try to
            PluckApple();
        }
    }

    // Determine if ecocat has enough cans and is touching the depot
    void BuySeeds()
    {
        if (catCollider2D.IsTouching(bottleDepotCollider2D)) {
            // Case 1: num cans >= 3; use cans alone
            if (numCansCollected.Value >= 3) {
                numCansCollected.Value = numCansCollected.Value - 3;
                numSeedsCollected.Value++;
            }
            // Case 2: num cans < 3, but cans + bottles > 3 -> then use cans until no more, then bottles are
            // used instead
            else if (numCansCollected.Value + numBottlesCollected.Value >= 3 ) {
                // paid is between 0 and 2
                int paid = numCansCollected.Value;
                numCansCollected.Value = 0;
                numBottlesCollected.Value -= (3 - paid);
                numSeedsCollected.Value++;
            }
        }
    }

    void PlantTree()
    {
        // Cat must be grounded
        if (numSeedsCollected.Value >= 1 && IsOnGround.Value)
        {
            // plant a tree
			Instantiate(tree, transform.position + Vector3.down * (catCollider2D.radius + 0.05f), Quaternion.identity);
			numSeedsCollected.Value--;
			plantTreeSound.Play ();
        }
    }

    // Note Pluck Apple is not working, because the tree collider isn't being recognized
    void PluckApple()
    {
//        if (catCollider2D.IsTouching(treeCollider2D))
//        {
//            Debug.Log("Pluck an apple");
//        }
    }
}
