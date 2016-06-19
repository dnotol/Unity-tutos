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
	
	#region functions
    public void Start()
    {

    }

    public void Update()
    {
        foreach (BodyPart bp in m_BodyParts)
            Debug.Log(bp.name);
    }
	#endregion
	
	#region events
	#endregion
}
