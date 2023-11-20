using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject CrossBow;
    private bool atSpot = false;
    private Spot spot;

    void Start()
    {

    }

    void Update()
    {   if (atSpot && CrossBow.GetComponent<CrossBow>().shots >= 0)
        {
            spot.shots = CrossBow.GetComponent<CrossBow>().shots;
        }

        if(spot.shots == 0)
        {
            CrossBow.GetComponent<CrossBow>().ready_to_shoot = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spot")
        {
            spot = collision.gameObject.GetComponent<Spot>();
            atSpot = true;
            if (spot.shots > 0)
            {
                CrossBow.GetComponent<CrossBow>().ready_to_shoot = true;
                CrossBow.GetComponent<CrossBow>().shots = spot.shots;
            }
            
            
            spot = collision.gameObject.GetComponent<Spot>();
        }
        else if(collision.gameObject.name == "Terrain")
        {
            
                CrossBow.GetComponent<CrossBow>().ready_to_shoot = false;
                atSpot = false;
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Spot")
        {

            CrossBow.GetComponent<CrossBow>().ready_to_shoot= false;
            atSpot = false;

        }
    }

    void OnGUI()
    {
        if (atSpot)
        {
            // Create a new GUIStyle
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 30; // Set the font size to 30

            // Draw the message with the new GUIStyle
            GUI.Label(new Rect(10, 50, 500, 100), "您已到达射击位,剩余射击次数："+spot.shots, style);
        }
    }
}
