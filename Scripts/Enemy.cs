using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject football;
    public Vector3 startPos;
    private NavMeshAgent navgent;
    public float changeInX;
    public float changeInZ;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeEnemySpeed", 0, 6);
        startPos = transform.position;
        navgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navgent.SetDestination(new Vector3(football.transform.position.x + changeInX, 0, football.transform.position.z + changeInZ));
    }
    private void ChangeEnemySpeed()
    {
        navgent.speed = Random.Range(1.5f, 4.5f);
    }
}
