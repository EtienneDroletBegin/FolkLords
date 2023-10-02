using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator m_Animator;
    private float m_Speed = 2f;
    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }
    private void Update()
    {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        m_Animator.SetFloat("V", V);
        m_Animator.SetFloat("H", H);
        transform.Translate(new Vector3(0, V*m_Speed*Time.deltaTime, 0));
        transform.Translate(new Vector3(H * m_Speed * Time.deltaTime, 0, 0));

    }
}
