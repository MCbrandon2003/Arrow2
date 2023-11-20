using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector3 starting_point;
    Transform target;
    Vector3 delta;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.isKinematic == true)
        {
            this.transform.position = target.position + delta;
        }
    }
    void OnCollisionEnter(Collision collision)
    {   if (rb.isKinematic == false)
        {
            rb.isKinematic = true;
            target = collision.gameObject.transform;
            delta = transform.position - target.position;
        }
    }
}
