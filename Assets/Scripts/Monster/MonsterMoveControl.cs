using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveControl : MonoBehaviour
{
    Vector3 m_velocity;
    Vector3 m_gravityMovement;
    public CharacterController m_monsterCharacterController;
    public Transform m_groundCheck;
    public float m_radius = 0.04f;
    public LayerMask m_groundMask;
    bool m_isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(m_monsterCharacterController.enabled)
        {
            m_isGrounded = Physics.CheckSphere(m_groundCheck.position, m_radius, m_groundMask);
            if(m_isGrounded && m_velocity.y < 0)
            {
                m_velocity.y = 0f;
            }
            m_velocity.y += -9.8f * Time.deltaTime;
            m_gravityMovement.y = m_velocity.y * Time.deltaTime;
            m_monsterCharacterController.Move(m_gravityMovement);
        }
    }
}

