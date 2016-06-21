using UnityEngine;
using System.Collections;

public class CharacterMover : MonoBehaviour 
{
    #region constants
    #endregion

    #region variables
    private Rigidbody m_Rb;
    public float m_Speed = 10.0f;
    public float m_JumpSpeed = 15.0f;
    #endregion

    #region fields
    #endregion

    #region functions
    void Start()
    {
        m_Rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CatchInput();
        //ApplyGravity();
    }

    void CatchInput()
    {
        Vector3 _Direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _Direction *= m_Speed;
        if ( Input.GetKeyDown(KeyCode.Space) )
        {
            _Direction.y = m_JumpSpeed;
        }
        _Direction += Physics.gravity;
        //_Direction *= Time.deltaTime;
        m_Rb.AddForce(_Direction, ForceMode.Acceleration);
    }

    void ApplyGravity()
    {
        m_Rb.AddForce(Physics.gravity * Time.deltaTime);
    }
	#endregion
	
	#region events
	#endregion
}
