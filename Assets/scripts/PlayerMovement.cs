using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour,IDamageable
{
    private Animator m_Animator;
    private float m_Speed = 2f;
    private Vector2 m_Velocity;
    private Vector2 m_Position;

    public float health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void FullHeal()
    {
        health = 100.0f;
    }

    public void Heal(float _amount)
    {
        health += _amount;
    }

    public void Kill()
    {
        health = 0;
    }

    public void TakeDamage(float _amount)
    {
        health -= _amount;
    }

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Vector2 m_velocity = GetComponent<Rigidbody2D>().velocity;
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        m_Animator.SetFloat("V", V);
        m_Animator.SetFloat("H", H);
        m_velocity.x = H * m_Speed;
        m_velocity.y = V * m_Speed;

        GetComponent<Rigidbody2D>().velocity = m_velocity ;

        //transform.Translate(new Vector3(0, V*m_Speed*Time.deltaTime, 0));
        //transform.Translate(new Vector3(H * m_Speed * Time.deltaTime, 0, 0));


        if (Input.GetKeyDown(KeyCode.B))
        {
            SaveData dataToSave = new SaveData(transform.position, PartyManager.GetInstance().getParty());
            SaveSystem.save(dataToSave);
            Debug.Log(dataToSave.Position.ToString());
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            SaveData dataToLoad = SaveSystem.load();
            transform.position = dataToLoad.Position;
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            gameObject.ResetPosition();
        }
    }

    
}
