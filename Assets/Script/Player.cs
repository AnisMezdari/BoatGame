using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int score;
    public Boat boat;
    public int bonus;
    public int cannon = 0;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        bonus = 0;
        InvokeRepeating("CalculScore", 0, 0.5f);
        InvokeRepeating("ResetCannon", 0, 5f);

    }

    // Update is called once per frame
    void Update()
    {
        //CalculScore();
        SetTextScore();
        SetTextCannon();
        bonus = 0;
    }

    private void CalculScore()
    {
        if (!boat.death)
        {
            //float pivot = Time.timeSinceLevelLoad;

            score += (int) boat.horizontaleSpeed;
        }
        
    }
    private void SetTextScore()
    {
        GameObject.Find("score_var").gameObject.GetComponent<Text>().text = score.ToString();

    }
    private void SetTextCannon()
    {
        GameObject.Find("var_cannon").gameObject.GetComponent<Text>().text = cannon.ToString();
    }

    public void OnClickReplay()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void OnClickQuit()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void ResetCannon()
    {
        if (cannon < 2)
        {
            cannon++;
        }
    }



}
