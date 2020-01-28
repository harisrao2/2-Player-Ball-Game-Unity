using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public int count;
    public Text countText;
    public Text winText;
    private bool onGround;
    private object startPos;
    public Text timerText;
    private float startTime;
    


    // Start is called before the first frame update
    void Start()
    {
        onGround = true;
        rb = GetComponent<Rigidbody>();
        // speed = 4;
        count = 0;
        SetCountText();
        winText.text = "";
        startTime = Time.time;
        
        
    

       
    }

    // Update is called once per frame
    void Update()
    {
        float t = 120 - (Time.time - startTime);

        if (t <=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            timerText.text = "0";
            if (count > GameObject.Find("Player2").GetComponent<Player2Control>().count)
            {
                winText.text = "Player 1 Wins";
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }
            else if (count < GameObject.Find("Player2").GetComponent<Player2Control>().count)
            {
                winText.text = "Player 2 Wins";
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                winText.text = "Draw";
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            
        }
        else
        {
            timerText.text = t.ToString("f0");
        }

        if (gameObject.transform.position.y < -5)
        {
            transform.position = new Vector3(-1, 0.5f, 0);
            rb.velocity = new Vector3(0, 0, 0);
            count = count - 5;
            SetCountText();


        }

      

        if (onGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector3(0, 5, 0);
                onGround = false;
            }
        }
        
        FixedUpdate();
    }
     void FixedUpdate()
    {
        
        float moveHorizntal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 mov = new Vector3(moveHorizntal, 0, moveVertical);
  
        rb.AddForce(mov * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectables"))
        {
            other.gameObject.SetActive(false);
            count = count + 2;
            SetCountText();
        }
        if (other.gameObject.CompareTag("SuperCollectables"))
        {
            other.gameObject.SetActive(false);
            count = count + 5;
            SetCountText();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            if (count > 0)
            {
                count = count - 1;
                SetCountText();
            }
        }
        if (other.gameObject.CompareTag("p2"))
        {
            if ((rb.position.y+0.2) < other.gameObject.transform.position.y)
            {
                if (count > 0)
                {
                    count = count - 1;
                    SetCountText();
                }
            }

        }
    }
    void SetCountText()
    {
        
        if (count < 0)
        {
            count = 0;
            countText.text = "Player 1 Score: " + count.ToString();
        }
        else
        {
            countText.text = "Player 1 Score: " + count.ToString();
        }
    }
    

   

}
