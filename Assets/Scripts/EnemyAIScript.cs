using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAIScript : MonoBehaviour
{
    public enum State
    {
        Idle,
        Patrol,
        DetectPlayer,
        Chasing,
        AggroIdle,
    }

    public State enemyAIState;
    public float moveSpeed; //speed of the enemy when patrolling

    public float maxSpeed;

    public float chaseSpeed; //speed of the enemy when chasing the player

    private float speed; //current speed of the enemy

    public float detectedPlayerTime; //time the enemy will stay in detect mode before beginning chasing player

    public float aggroTime; //used if player is out of detection radius - enemy will stay in aggro mode for this time, and can immediately resume chasing before going back to idle

    public bool playerDetected; //if the player is detected

    public bool aggro; //if the enemy is in an aggro state
    private Rigidbody2D _myRb;
    
    public Transform[] patrolPoints;
    public Transform destinationPoint;
    public Animator anim;
    public Transform player;
    public GameManager gameManager;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        enemyAIState = State.Patrol;
        _myRb = GetComponent<Rigidbody2D>();
        destinationPoint = patrolPoints[Random.Range(0, patrolPoints.Length)];
        player = GameObject.FindWithTag("Player").transform;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        switch (enemyAIState)
        {
            case State.Idle:
                anim.SetInteger("state", 0);
                break;
            case State.Patrol:
                anim.SetInteger("state", 1);
                MoveTowardsDestination(destinationPoint.position.x, moveSpeed);
                if (Mathf.Abs(transform.position.x - destinationPoint.position.x) < 0.5f)
                {
                    destinationPoint = patrolPoints[Random.Range(0, patrolPoints.Length)];
                }

                break;
            case State.DetectPlayer:
                break;
            case State.Chasing:
                anim.SetInteger("state", 3);
                MoveTowardsDestination(player.position.x, chaseSpeed);

                //Debug.Log(Vector2.Distance(transform.position, player.position));
                if (Vector2.Distance(transform.position, player.position) < 1.5f)
                {
                    // Check if enough time has passed since the last attack
                    if (timer >= 2)
                    {
                        // Attack the player
                        gameManager.TakeDamage(1);
                        // Reset the timer
                        timer = 0f;
                    }
                }
                break;
            case State.AggroIdle:
                break;
        }
    }

    void MoveTowardsDestination(float destinationX, float speed)
    {
        // Calculate the direction towards the destination only along the x-axis
        float directionX = Mathf.Sign(destinationX - transform.position.x);

        // Move only along the x-axis
        _myRb.velocity = new Vector2(directionX * speed, _myRb.velocity.y);

        // Flip the enemy sprite based on movement direction
        if (directionX < 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (directionX > 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true;
            if (aggro == false)
            {
                StopCoroutine("DetectTimer"); //need to stop the Coroutine in case it was previously started e.g. if the player quickly enters and exits the detection radius
                StartCoroutine("DetectTimer");
            }
            if (aggro == true)
            {
                playerDetected = true;
                enemyAIState = State.Chasing;
            }
        }

    }

    IEnumerator DetectTimer()
    {
        enemyAIState = State.DetectPlayer;
        yield return new WaitForSeconds(detectedPlayerTime);
        if (playerDetected == true)
        {
            aggro = true;
            enemyAIState = State.Chasing;
        }
        if (playerDetected == false)
        {
            aggro = false;
            enemyAIState = State.Patrol;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
            if (aggro == true)
            {
                StopCoroutine("AggroTimer");
                StartCoroutine("AggroTimer");
            }
        }
    }

    IEnumerator AggroTimer()
    {
        yield return new WaitForSeconds(aggroTime);
        if (playerDetected == false & aggro == false)
        {
            aggro = false;
            enemyAIState = State.Patrol;
        }
        if (playerDetected == false & aggro == true)
        {
            enemyAIState = State.Patrol;
            destinationPoint = patrolPoints[Random.Range(0, patrolPoints.Length)];
        }
        yield return new WaitForSeconds(aggroTime * 2);
        if (playerDetected == false)
        {
            aggro = false;
            enemyAIState = State.Patrol;
        }

    }
}