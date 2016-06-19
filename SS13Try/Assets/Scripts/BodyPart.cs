using UnityEngine;
using System.Collections;

[System.Serializable]
public class BodyPart 
{
    #region constants
    #endregion

    #region variables
    string m_Name = "GenericBodyPart";
    #endregion

    #region fields
    public string Name
    {
        get { return m_Name; }
        set { m_Name = value; }
    }
    #endregion

    #region functions
    public BodyPart() { }

    public BodyPart( string name )
    {
        Name = name;
    }
    #endregion

    #region events
    #endregion
}
