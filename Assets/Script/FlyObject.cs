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
        //y_oldPosition = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        speed = GameObject.Find("Boat").GetComponent<Boat>().horizontaleSpeed;
        
/*        if (this.tag.Equals("Debris"))
        {
            this.transform.position += new Vector3(-(speed / scaling) * Time.deltaTime, Y_position,3);
        }
        else
        {*/
           // this.transform.position += new Vector3();
        this.transform.Translate(new Vector3(-(speed / scaling) * Time.deltaTime, Y_position));
        //}

        // if the boat exceed the screen (Up)
        if (Boat.GetComponent<Boat>().flyUp)
        {

            transform.position =  Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y-10, this.transform.position.z),Time.time);
        }
        // if the boat exceed the screen (Down)
        if (Boat.GetComponent<Boat>().flyDown)  
        {
            transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y + 10, this.transform.position.z), Time.time);
        }
        Destroy();
    }

    public void Destroy()
    {
        if(this.transform.position.x < -17)
        {
            Destroy(this.gameObject);
        }
    }

 
}
