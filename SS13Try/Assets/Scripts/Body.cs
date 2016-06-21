using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Body : MonoBehaviour 
{
    #region constants
    #endregion
    
    #region variables
    [SerializeField]
    List<BodyPart> m_BodyParts = new List<BodyPart>();
    #endregion

    #region fields
    public List<BodyPart> BodyParts
    {
        get { return m_BodyParts; }
    }
    #endregion

    #region functions
    public void Start()
    {
        CreateHumanBody();
    }

    private void CreateHumanBody()
    {
        m_BodyParts.Add(new Heart());
        m_BodyParts.Add(new Liver());
        m_BodyParts.Add(new Lung());
    }

    public void DamageRandomBodyPart( float value )
    {
        m_BodyParts[Random.Range(0, m_BodyParts.Count)].Damage( value );
    }

    void OnMouseDown()
    {
        DamageRandomBodyPart( Random.Range(10,20) );
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, 1);
    }
    #endregion

    #region events
    #endregion
}
