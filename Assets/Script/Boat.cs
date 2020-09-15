using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

    public int horizontaleSpeed = 1;
    public int verticalForce = 10;
    public int gravity = 5;
    public int cloudForce = 500;

    private bool isFalling = true;

 


    // Start is called before the first frame update
    void Start()
    {


        
    }

    // Update is called once per frame
    void Update()
    {


        /*        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
                {
                    GetComponent<Rigidbody>().AddForce(Vector3.up * verticaleForce, ForceMode.Impulse);
                }*/


       // this.transform.position += new Vector3(horizontaleSpeed * Time.deltaTime, 0, 0);

        GetComponent<Rigidbody2D>().AddForce(-Vector3.up * gravity);
        
        //Debug.Log(GetComponent<Rigidbody>().velocity);

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && isFalling)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * verticalForce, ForceMode2D.Impulse);
        }
#endif

        if (GetComponent<Rigidbody2D>().velocity.y < -10    )
        {
            //isFalling = true;
            this.horizontaleSpeed = 5;
        }


    }


    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.tag.Equals("Cloud"))
        {
            Debug.Log("sa passe");
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * cloudForce, ForceMode2D.Impulse);
            this.horizontaleSpeed = this.horizontaleSpeed * 2;


        }
    }

}
