using BoatGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

    public int horizontaleSpeed = 1;
    public int verticalForce = 10;
    public int gravity = 5;
    public int cloudForce = 500;

    public GameObject game;

    
    private bool isFalling = true;
    public bool flyUp = false;
    public bool flyDown = false;


    // Start is called before the first frame update
    void Start()
    {


        
    }

    // Update is called once per frame
    void Update()
    {
        flyUp = false;

        /*     Mobile version  (touch)  
         
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * verticaleForce, ForceMode.Impulse);
            }*/


        GetComponent<Rigidbody2D>().AddForce(-Vector3.up * gravity);

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && isFalling)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * verticalForce, ForceMode2D.Impulse);
        }
#endif

        if (GetComponent<Rigidbody2D>().velocity.y < -10    )
        {
            this.horizontaleSpeed = 5;
        }


        if(this.transform.position.y > 5)
        {
            this.transform.position += new Vector3(0, -10);
            flyUp = true;
        }
        if( this.transform.position.y < -5)
        {
            this.transform.position += new Vector3(0, 10);
            flyDown = true;
        }


        Debug.Log(flyUp);
        Debug.Log(flyDown);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.tag.Equals("Cloud"))
        {
            Game gameScript = game.GetComponent<BoatGame.Game>();

            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * cloudForce, ForceMode2D.Impulse);
            this.horizontaleSpeed *=  2;

        }
    }

}
