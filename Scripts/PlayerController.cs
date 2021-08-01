using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public bool isHoldingBall=false;

    public GameObject ballPrefab;
    public GameObject teleText;
    public GameObject telePoint;

    private Footballness fbness;
    private Rigidbody playerRB;
    private Animator playerAnim;

    private bool isCapable;
    private int telepabel=4;
    private int myGoals;
    private float horizontalInput;
    private float verticalInput;


    void Start()
    {
        InvokeRepeating("MoreeeTele", 0, 20);
        fbness = ballPrefab.GetComponent<Footballness>();
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isHoldingBall)
        {
            ballPrefab.transform.SetPositionAndRotation(transform.position + new Vector3(0,0.7f,1), transform.rotation);
            Invoke("NoLongerCapable", 5);
            if (myGoals != fbness.playerGoals) { isHoldingBall = false; }
        }
        else { myGoals=fbness.playerGoals;}

        if (transform.position.x < -25)
        {
            transform.position = new Vector3(-25, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 25)
        {
            transform.position = new Vector3(25, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -40)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -40);
        }
        if (transform.position.z > 59)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 59);
        }


        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        playerRB.AddForce(speed * Time.deltaTime * new Vector3(horizontalInput, 0, verticalInput), ForceMode.Impulse);

        if (Input.GetMouseButtonUp(0) && isCapable)
        {
            if (isHoldingBall) { isHoldingBall = false; speed = 300; }
            else { isHoldingBall = true; speed = 200; }
        }

        if (Input.GetMouseButtonUp(1) && telepabel>0)
        {
            Vector3 temp = transform.position;
            transform.position = telePoint.transform.position;
            telePoint.transform.position = temp;
            telepabel--;
            teleText.GetComponent<Text>().text = "TELEPORTS LEFT : " + telepabel.ToString();
            teleText.GetComponent<Text>().color = Random.ColorHSV();
        }


        playerAnim.SetInteger("RightLeft", (int)horizontalInput);
        playerAnim.SetInteger("ForwardBackward", (int)verticalInput);

        if (horizontalInput == 0 && verticalInput == 0)
        {
            playerAnim.SetBool("IsIdle", true);
            playerAnim.SetInteger("ForwardBackward", 0);
            playerAnim.SetInteger("RightLeft", 0);
        }
        else
        {
            playerAnim.SetBool("IsIdle", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Football")
        {
            isCapable = true;
            Invoke("YesNoLongerCapable", 4);
        }
    }
    
    public void NoLongerCapable()
    {
        isCapable = false;
        isHoldingBall = false;
    }
    public void YesNoLongerCapable()
    {
        if (!isHoldingBall)
        {
            NoLongerCapable();
        }
    }
    public void MoreeeTele()
    {
        if (telepabel < 4) 
        { 
            telepabel++;
            teleText.GetComponent<Text>().text = "TELEPORTS LEFT : " + telepabel.ToString();
            teleText.GetComponent<Text>().color = Random.ColorHSV();
        }
    }
}
