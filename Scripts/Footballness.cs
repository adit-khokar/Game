using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Footballness : MonoBehaviour
{
    private Transform bibi;
    private Rigidbody rb;
    public PlayerController pc;
    public int playerGoals;
    public int enemyGoals;
    public GameObject playerScore;
    public GameObject enemyScore;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position.y < -5)
        {
            ResetBall();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            playerGoals++;
            ResetBall();
        }
        if (collision.gameObject.CompareTag("Enemy") && !pc.isHoldingBall)
        {
            bibi = collision.gameObject.transform;
            EnemyBallContact();
        }
        
    }
    private void EnemyBallContact()
    {
         bibi.position = bibi.GetComponent<Enemy>().startPos;
         enemyGoals++;
         ResetBall();
    }

    private void ResetBall()
    {
        rb.velocity = Vector3.zero;
        rb.position = new Vector3(-0.75f, 0.8f, 10);
        enemyScore.GetComponent<Text>().text = enemyGoals.ToString();
        playerScore.GetComponent<Text>().text = playerGoals.ToString();
    }
}
