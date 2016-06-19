using UnityEngine;
using System.Collections;

public class BodyPart : MonoBehaviour 
{
    #region constants
    #endregion

    #region variables
    string m_Name = "GenericBodyPart";
    #endregion

    #region fields
    #endregion

    #region functions
    public string Name
    {
        get { return m_Name; }
        set { m_Name = value; }
    }
    #endregion

    #region events
    #endregion
}
