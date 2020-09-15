using BoatGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyObject : MonoBehaviour
{
    public float speed = 1;
    public float scaling = 1;

    public GameObject Boat;
    
    private float Y_position = 0;
    private float y_oldPosition;
    // Start is called before the first frame update
    void Start()
    {
        Boat = GameObject.Find("Boat");
        y_oldPosition = this.transform.position.y;
        Debug.Log(" hola les amis");
    }

    // Update is called once per frame
    void Update()
    {
        speed = GameObject.Find("Boat").GetComponent<Boat>().horizontaleSpeed;
        // this.transform.Translate(new Vector3(-(speed  / scaling )* Time.deltaTime, Y_position));

        Debug.Log(Y_position);
        this.transform.position += new Vector3(-(speed / scaling) * Time.deltaTime, Y_position);
        if (Boat.GetComponent<Boat>().flyUp)
        {
            Debug.Log("sa passe");
            transform.position =  Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y-10),Time.time);
            //GetComponent<Rigidbody2D>().AddForce(Vector3.down * 50 , ForceMode2D.Impulse);
 

            //Y_position = -1;
          
        }
        if (Boat.GetComponent<Boat>().flyDown)  
        {
            //this.transform.position += new Vector3(0, 10);
            Boat.GetComponent<Boat>().flyDown = false;
        }
    }
}
