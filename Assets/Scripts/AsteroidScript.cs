using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public GameObject asteroidExplosion;
    public float speed;  // speed of the asteroid
    public float angularSpeed;  // speed of rotation
    Rigidbody Asteroid;

    float size; // size of the asteroid
    int hp = 2;

    // Start is called before the first frame update
    void Start()
    {
        size = Random.Range(0.5f, 2.0f);    // random size
        float speedX = Random.Range(-0.2f, 0.2f) * speed;   // random offset X
        Asteroid = GetComponent<Rigidbody>();
        Asteroid.angularVelocity = Random.insideUnitSphere * angularSpeed;  // rotate the asteroid
        Asteroid.velocity = new Vector3(speedX, 0, -speed) / size;  // speed depends on the size
        Asteroid.transform.localScale *= size;  // chamge the scale of the asteroid
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            return;
        }

        GameObject explosion = Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
        explosion.transform.localScale /= hp;
        hp--;
        if (hp == 0)
        {
            GameController.score += 10;
            Destroy(gameObject);
        }
    }
}
