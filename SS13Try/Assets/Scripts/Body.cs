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
    #endregion
    
    #region functions
    public void Start()
    {
        CreateHumanBody();
        foreach (BodyPart bp in m_BodyParts)
        {
            Debug.Log( bp.Name );
        }
    }

    public void Update()
    {

    }

    private void CreateHumanBody()
    {
        m_BodyParts.Add(new Heart());
        m_BodyParts.Add(new Liver());
        m_BodyParts.Add(new Lung());
    }
    #endregion
    
    #region events
	#endregion
}
