using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private Transform target;
    private Animator m_animator;
    private float m_Speed = 2;
    private bool Moving;
    private Vector3 m_lastPos = Vector3.zero;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void UpdateAnimator()
    {
        Vector3 direction = target.position - transform.position;


        m_animator.SetFloat("H", Moving?direction.x:0);
        m_animator.SetFloat("V", Moving?direction.y:0);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > 0.75f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, m_Speed * Time.deltaTime);
            Moving = true;

        }
        else
        {
            Moving = false;
        }

        UpdateAnimator();

        m_lastPos = transform.position;
    }
}
