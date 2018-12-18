using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistance;

    private Transform target;
    private Vector3 myPos;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //myPos = transform.position;
    }

    private void Update()
    {
        //if (!target) return;

        if (Vector2.Distance(transform.position, target.position) > stopDistance)
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
