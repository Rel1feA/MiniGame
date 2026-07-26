using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RECode.REFramework;

public class Bat : MonoBehaviour
{
    [SerializeField]
    private float chaseSpeed;
    [SerializeField]
    private float minSleepTime;
    [SerializeField]
    private float maxSleepTime;

    private float sleepTime;
    private Vector2 chaseDir;

    private Transform targetTra;
    private Rigidbody2D rb2D;
    private bool isSleeping=true;
    private float timer;
    private Animator animator;

    private void Awake()
    {
        rb2D=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        sleepTime=Random.Range(minSleepTime,maxSleepTime);
        RandDir();
    }

    private void Update()
    {
        if(timer<sleepTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            isSleeping= false;
            animator.Play("Fly");
        }
    }


    private void FixedUpdate()
    {
        if(!isSleeping)
        {
            Chase();
        }
    }

    public void Chase()
    {
        if (targetTra != null)
        {
            chaseDir = (targetTra.position - transform.position).normalized;
        }
        rb2D.velocity = chaseDir * chaseSpeed;
    }

    private void RandDir()
    {
        float randX = Random.Range(-1f, 1f);
        float randY = Random.Range(-1f, 1f);
        chaseDir = new Vector2(randX, randY).normalized;
    }

    public void SetTarget(Transform _transform)
    {
        if(_transform==null)
        {
            RandDir();
        }
        targetTra = _transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

        }
    }
}
