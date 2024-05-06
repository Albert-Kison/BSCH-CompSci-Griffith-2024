using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetScript : MonoBehaviour
{
    public Transform target;
    public float dumping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), Time.deltaTime * dumping);
    }
}
