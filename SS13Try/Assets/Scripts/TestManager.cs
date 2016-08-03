using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class TestManager : MonoBehaviour 
{
    #region constants
    #endregion

    #region variables
    public Text m_Text;
    public Body m_Body;
    #endregion
    
    #region fields
	#endregion
    
    #region functions
    public void Start()
    {
        m_Body.BodyParts[0].DamageReceived += DebugDamage;
    }

    public void Update()
    {
        m_Text.text = "";
        foreach( BodyPart bp in m_Body.BodyParts )
        {
            m_Text.text += bp.Name + " : " + bp.HP + " / " + bp.MaxHP + "\n";
        }
    }

    private void DebugDamage( object sender, BodyPart.DamageReceivedEventArgs e )
    {
        string _text = (sender as BodyPart).Name + " took " + e.damage + ".";
        if (e.damageKilled)
            _text += " It destroyed it.";
        Debug.Log(_text);
    }
    #endregion
    
    #region events
	#endregion
}
