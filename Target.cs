using UnityEngine;
using System.Collections;
public class Target : MonoBehaviour
{   public int basepoint;
    public float pace;
    public bool is_moving;
    int sign;
    Vector3 ori;
    public int scores;
    // Use this for initialization
    void Start()
    {   
        if(is_moving == false)
        {
            pace = 0;
        }
        else
        {
            sign = 1;
            ori = this.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(is_moving == true)
        {
            this.transform.position += new Vector3(sign * 0.01f, 0, 0);
            if(transform.position.x - ori.x > 5 || transform.position.x - ori.x < -5 )
            {
                sign *= -1;
            }
        }
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        // Get the first contact point
        ContactPoint contact = collision.contacts[0];

        // Get the starting_point property from the arrow's script
        // Replace "ArrowScript" with the name of the script that contains the starting_point property
        if (collision.gameObject.tag == "Arrow")
        {
            Vector3 starting_point = collision.gameObject.GetComponent<Arrow>().starting_point;

            // Calculate the distance
            float distance = Vector3.Distance(contact.point, starting_point);
            Debug.Log("hitting point:" + contact.point);
            // Print the distance
            Debug.Log("Distance: " + distance);
            Transform center = transform.Find("center");
            int factor = 0;
            if(Vector3.Distance(contact.point,center.position)<0.09)
            {
                factor = 5;
            }
            else if(Vector3.Distance(contact.point, center.position) < 0.37)
            {
                factor = 3;
            }
            else
            {
                factor = 1;
            }

            if(distance > 5)
            {
                scores += basepoint + factor * (int)distance;
            }
        }
    }
}