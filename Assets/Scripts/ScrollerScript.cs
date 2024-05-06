using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerScript : MonoBehaviour
{
    public float speed;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float Z = Mathf.Repeat(Time.time * speed, 120);
        Vector3 offset = new Vector3(0, 0, -Z);
        transform.position = startPosition + offset;
    }
}
