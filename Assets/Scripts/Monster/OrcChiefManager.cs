using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcChiefManager : MonoBehaviour
{
    Vector3 m_velocity;
    Vector3 m_gravityMovement;
    public CharacterController m_OrcController;
    public Transform m_groundCheck;
    public Animator animator;
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
        m_isGrounded = Physics.CheckSphere(m_groundCheck.position, m_radius, m_groundMask);
        if(m_isGrounded && m_velocity.y < 0)
        {
            m_velocity.y = 0f;
        }
        m_velocity.y += -9.8f * Time.deltaTime;
        m_gravityMovement.y = m_velocity.y * Time.deltaTime;
        m_OrcController.Move(m_gravityMovement);
        
    }
    void OnTriggerEnter(Collider other) {
        Debug.Log("트리거 작동");
        if(other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            animator.SetBool("IsAttack", true);
            Debug.Log("레이어 제대로 읽음");
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Arrow"))
        {
            Debug.Log("오크가 화살에 맞았다.");
        }
    }
}
