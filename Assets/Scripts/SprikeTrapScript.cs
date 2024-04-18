using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprikeTrapScript : MonoBehaviour
{
    public GameManager gameManager;
    public float damage;
    private bool canDamage = true;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && canDamage)
        {
            gameManager.TakeDamage(damage);
            StartCoroutine(DamageCooldown());
        }
    }

    IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(5);
        canDamage = true;
    }
}
