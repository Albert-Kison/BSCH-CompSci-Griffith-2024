using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSourceScript : MonoBehaviour
{
    public GameObject Asteroid;
    public float minDelay, maxDelay;    // the time intervals to launch an asteroid
    float nextLaunch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if the game is not started
        if (GameController.isStarted == false)
        {
            return;
        }

        if (Time.time > nextLaunch)
        {
            // Get the random position of the asteroid launch
            float halfWidth = transform.localScale.x / 2;
            float X = Random.Range(-halfWidth, halfWidth);
            float Z = transform.position.z;
            Vector3 asteroidPosition = new Vector3(X, 0, Z);

            // launch the asteroid
            Instantiate(Asteroid, asteroidPosition, Quaternion.identity);

            // change the time of the next launch
            nextLaunch = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
