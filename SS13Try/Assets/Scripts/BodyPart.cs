using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public abstract class BodyPart
{
    #region constants
    #endregion

    #region variables
    public event EventHandler<DamageReceivedEventArgs> DamageReceived;
    private string m_Name = "GenericBodyPart";
    private float m_MaxHP = 100;
    private float m_HP = 100;
    #endregion

    #region fields
    public string Name
    {
        get { return m_Name; }
        set { m_Name = value; }
    }

    public float HP
    {
        get { return m_HP; }
        set { m_HP = value; }
    }

    public float MaxHP
    {
        get { return m_MaxHP; }
        set { m_MaxHP = value; }
    }
    #endregion

    #region functions
    public BodyPart() { }

    public void Damage(float damage)
    {
        m_HP -= damage;
        DamageReceivedEventArgs _eArgs = new DamageReceivedEventArgs();
        _eArgs.damage = damage;
        _eArgs.damageKilled = m_HP <= 0;
        OnDamageReceived(_eArgs);
    }

    #endregion

    #region events
    public class DamageReceivedEventArgs : EventArgs
    {
        public float damage { get; set; }
        public bool damageKilled { get; set; } 
    }

    protected virtual void OnDamageReceived( DamageReceivedEventArgs e )
    {
        EventHandler<DamageReceivedEventArgs> _Handler = DamageReceived;
            if ( _Handler != null )
            {
                _Handler(this, e);
            }
    }
    #endregion
}
