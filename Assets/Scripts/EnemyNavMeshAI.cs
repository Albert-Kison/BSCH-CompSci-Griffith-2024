using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyNavMeshAI : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public NavMeshAgent agent;
    public float velocity;
    private bool aggro;
    public bool destinationReached;
    public float destinationThreshold;
    public Transform[] patrolPoints;
    public float aggroTimer;

    public float patrolSpeed;

    public float aggroSpeed;
    public GameObject exclamationMark;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        aggro = false;
        destinationReached = true;
        exclamationMark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Get the velocity of the enemy
        velocity = agent.velocity.magnitude;
        // Set the speed of the animator to the velocity of the enemy
        animator.SetFloat("velocity", velocity);

        if (aggro == true)
        {
            // Move towards the player using navmesh
            agent.destination = player.position;
        }

        if (aggro == false && destinationReached == true)
        {
            destinationReached = false;
            agent.speed = patrolSpeed;
            // Move towards the patrol points using navmesh
            agent.destination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;
        }

        // Check if the enemy has reached the destination
        if (Vector3.Distance(transform.position, agent.destination) < destinationThreshold)
        {
            destinationReached = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aggro = true;
            agent.speed = aggroSpeed;
            exclamationMark.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            agent.destination = player.position; //this is the last seen player position (since OnTriggerExit only runs once)
            StopCoroutine("AggroTimer");
            StartCoroutine("AggroTimer");
        }
    }

    IEnumerator AggroTimer()
    {
        yield return new WaitForSeconds(aggroTimer);
        exclamationMark.SetActive(false);
        aggro = false;
        destinationReached = true;
    }
}
