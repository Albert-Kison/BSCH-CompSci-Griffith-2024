using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipScript : MonoBehaviour
{
    public Rigidbody playerShipRb;
    public GameObject LazerGun;
    public GameObject LazerShot;
    public GameObject playerExplosion;

    public float speed; // the speed of the ship
    public float tilt;  // rotation speed
    public float xMin, xMax, zMin, zMax;    // the bounries
    public float shotDelay = 0.25f; // delay between the shots
    float nextShotTime;

    int hp = 3;

    // Start is called before the first frame update
    void Start()
    {
        playerShipRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the jump button is pressed, instantiate the shots each shotDelay time intervals
        if (Input.GetButton("Jump") && Time.time > nextShotTime)
        {
            Instantiate(LazerShot, LazerGun.transform.position, Quaternion.identity);
            nextShotTime = Time.time + shotDelay;
        }

        playerShipRb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed; // move the ship
        playerShipRb.position = new Vector3(Mathf.Clamp(playerShipRb.position.x, xMin, xMax), 0, Mathf.Clamp(playerShipRb.position.z, zMin, zMax)); // do not let it go outside the boudries
        playerShipRb.rotation = Quaternion.Euler(playerShipRb.velocity.z * tilt, 0, -playerShipRb.velocity.x * tilt);   // rotate the ship when turning
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lazer")
        {
            return;
        }

        hp--;
        if (hp == 0)
        {
            Instantiate(playerExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
