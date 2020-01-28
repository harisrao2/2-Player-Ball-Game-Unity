using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player2Control : MonoBehaviour
{
    private Rigidbody rb;
    public float speed2;
    public int count;
    public Text countText;
    public Text winText;
    private bool onGround;
    private float startTime;
    public Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        onGround = true;
        rb = GetComponent<Rigidbody>();
        //speed2 = 4;
        count = 0;
        SetCountText();
        winText.text = "";
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -5)
        {
            transform.position = new Vector3(-1, 0.5f, 0);
            rb.velocity = new Vector3(0, 0, 0);
            count = count - 5;
            SetCountText();


        }
        float t = 120-(Time.time - startTime);
       
        if (t ==0)
        {
            timerText.text = "10";
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
        else
        {
            timerText.text = t.ToString("f0");
        }
        
        if (onGround)
        {
            if (Input.GetButtonDown("Jump1"))
            {
                rb.velocity = new Vector3(0, 5, 0);
                onGround = false;
            }
        }

        FixedUpdate();
    }
    void FixedUpdate()
    {

        float moveHorizntal = Input.GetAxis("Horizontal1");
        float moveVertical = Input.GetAxis("Vertical1");
        Vector3 mov = new Vector3(moveHorizntal, 0, moveVertical);

        rb.AddForce(mov * speed2);
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
        if (other.gameObject.CompareTag("p1"))
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
            countText.text = "Player 2 Score: " + count.ToString();
        }
        else
        {
            countText.text = "Player 2 Score: " + count.ToString();
        }
    }



}
