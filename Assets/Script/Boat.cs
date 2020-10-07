using BoatGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Boat : MonoBehaviour
{

    public float horizontaleSpeed = 1;
    public int verticalForce = 10;
    public float gravity = 5;
    public int cloudForce = 20;

    public GameObject game;
    public Player player;

    private bool isFalling = true;
    public bool isBoosting = false;
    public bool flyUp = false;
    public bool flyDown = false;
    private bool boost = false;
    public bool death = false;

    private Vector2 startPosition;
    private Vector2 endPosition;

 


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddSpeedByTime", 0f, 1f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        BoatController();
        AddDefaultForce();
        DetectChangeMapUpAndDown();
        Death();

    }


    private void AddDefaultForce()
    {
        gravity = 5;
        GetComponent<Rigidbody2D>().AddForce(-Vector3.up * gravity, ForceMode2D.Force);
        if (GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            isFalling = true;
            //isBoosting = false;
        }
        else
        {
           isFalling = false;
        }
    }

    private void BoatController()
    {
        if (!death)
        {
#if UNITY_EDITOR

            if ( isFalling)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    gravity = 2f;
                    //GetComponent<Rigidbody2D>().AddForce(-Vector3.up * gravity, ForceMode2D.Impulse);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    GetComponent<Rigidbody2D>().AddForce(-Vector3.up * 1.7f, ForceMode2D.Impulse);
                }

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if(player.cannon > 0 && isFalling)
                {
                    Boost(7.5f);
                    player.cannon--;
                }
                
            }
          
#endif

            // Mobile version  (touch)  
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Stationary && isFalling)
            {
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                gravity = 1f;
            }
            if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended && isFalling)
            {
                GetComponent<Rigidbody2D>().AddForce(-Vector3.up * 1.7f, ForceMode2D.Impulse);
            }

            if (Input.touchCount > 0)
            {
                var touch = Input.touches[0];
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        // Stockage du point de départ
                        startPosition = touch.position;
                        break;
                    case TouchPhase.Ended:
                        // Stockage du point de fin
                        endPosition = touch.position;
                        break;
                }
                if (Mathf.Abs(startPosition.y - endPosition.y) > 300 && Input.touches[0].phase == TouchPhase.Moved) 
                {
                    if( player.cannon > 0 && isFalling)
                    {
                        Boost(7.5f);
                        player.cannon--;
                    }
                    startPosition.y = 0;
                    endPosition.y = 0;
                }

            }

        }
    }

    private void  DetectChangeMapUpAndDown()
    {
        if (!death)
        {
            flyUp = false;
            flyDown = false;
            if (this.transform.position.y > 5)
            {
                this.transform.position += new Vector3(0, -10);
                flyUp = true;
                game.GetComponent<Game>().stage++;
                
            }
            if (this.transform.position.y < -5)
            {
                this.transform.position += new Vector3(0, 10);
                flyDown = true;
                game.GetComponent<Game>().stage--;
                if (game.GetComponent<Game>().stage == -1)
                {
                    death = true;
                }
            }
        }
       

    }

    private void Death()
    {
        if (death)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().AddForce(-Vector3.up * gravity * 1.5f, ForceMode2D.Impulse);
            //GetComponent<Rigidbody2D>().AddForce(Vector3.right * horizontaleSpeed * 1.5f, ForceMode2D.Impulse);
            horizontaleSpeed = 0;
            this.transform.Rotate(new Vector3(0, 0, -5));

            GameObject.Find("Dead_text").GetComponent<Text>().enabled = true;
            GameObject.Find("score_text_endGame").GetComponent<Text>().enabled = true;
            GameObject.Find("score_var_endGame").GetComponent<Text>().enabled = true;
            GameObject.Find("background_endGame").GetComponent<Image>().enabled = true;
            GameObject.Find("score_var_endGame").GetComponent<Text>().text = player.score.ToString();
            GameObject.Find("replay").GetComponent<Image>().enabled = true;
            GameObject.Find("replay").transform.GetChild(0).GetComponent<Text>().enabled = true;
            GameObject.Find("Quit").GetComponent<Image>().enabled = true;
            GameObject.Find("Quit").transform.GetChild(0).GetComponent<Text>().enabled = true;

        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!death)
        {
            if (collider.tag.Equals("Cloud"))
            {
                Boost(cloudForce);
                //Game gameScript = game.GetComponent<BoatGame.Game>();
                horizontaleSpeed += 5;
                isBoosting = true;
                StartCoroutine(Timer(2));
               
            }

            if (collider.tag.Equals("Asteroid"))
            {
                if (GetComponent<Rigidbody2D>().velocity.y > 0)
                {
                    Destroy(collider.gameObject);
                }else
                {
                    death = true;
                }
                
            }
            if (collider.tag.Equals("Star"))
            {
                player.score += 10;
                Destroy(collider.gameObject);
            }
        }
        
    }

    private void Boost( float power )
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().AddForce(Vector3.up * power, ForceMode2D.Impulse);
    }

    private IEnumerator Timer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (!death)
        {
            horizontaleSpeed -= 5;
        }
        isBoosting = false;

    }

    private void AddSpeedByTime()
    {
        horizontaleSpeed += 0.3f;
    }

}
