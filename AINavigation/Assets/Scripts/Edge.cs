using System;

[System.Serializable]
public class Edge : IComparable<Edge> 
{
	#region Statics
	#endregion

	#region PublicVariables
	public float m_fCost;
	public Vertex m_pVertex;
	#endregion

	#region PrivateVariables
	#endregion

	#region Fields
	#endregion

	#region Functions
	public Edge( Vertex vertex = null, float cost = 1f )
	{
		this.m_pVertex = vertex;
		this.m_fCost = cost;
	}

	public int CompareTo( Edge other )
	{
		float result = m_fCost - other.m_fCost;
		int idA = m_pVertex.GetInstanceID();
		int idB = other.m_pVertex.GetInstanceID();
		if (idA == idB)
			return 0;
		return (int)result;
	}

	public bool Equals ( Edge other )
	{
		return (other.m_pVertex.m_id == this.m_pVertex.m_id);
	}

	public override bool Equals ( object obj )
	{
		Edge other = (Edge)obj;
		return (other.m_pVertex.m_id == this.m_pVertex.m_id);
	}

	public override int GetHashCode()
	{
		return this.m_pVertex.GetHashCode();
	}
	#endregion

	#region Events
	#endregion
}
