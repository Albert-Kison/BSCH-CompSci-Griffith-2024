using System.Collections;
using UnityEngine;

public class SprikeTrapScript : MonoBehaviour
{
    public GameManager gameManager;
    public float damage;
    private bool isPlayerInside = false;
    private Coroutine damageCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // No need to continuously check here, the coroutine will handle it
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInside = true;
            // Start damaging coroutine when player enters the trap
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DamagePlayerRepeatedly());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInside = false;
            // Stop damaging coroutine when player exits the trap
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    IEnumerator DamagePlayerRepeatedly()
    {
        // Continuously damage the player every 2 seconds while they are inside the trap
        while (isPlayerInside)
        {
            gameManager.TakeDamage(damage);
            yield return new WaitForSeconds(2);
        }
    }
}
