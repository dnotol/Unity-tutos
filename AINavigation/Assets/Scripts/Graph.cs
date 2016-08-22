using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Graph : MonoBehaviour 
{
	#region Statics
	#endregion

	#region PublicVariables
	public GameObject m_pVertexPrefab;
	#endregion

	#region PrivateVariables
	protected List<Vertex> m_lVertices;
	protected List<List<Vertex>> m_lNeighbours;
	protected List<List<float>> m_lCosts;
	#endregion

	#region Fields
	#endregion

	#region Function
	public virtual void Start()
	{
		Load();
	}

	public virtual void Load()
	{

	}

	public virtual int GetSize()
	{
		if (ReferenceEquals(m_lVertices, null))
			return 0;
		return m_lVertices.Count;
	}

	public virtual Vertex GetNearestVertex( Vector3 position )
	{
		return null;
	}

	public virtual Vertex GetVertexObj( int id )
	{
		if (ReferenceEquals(m_lVertices, null) || m_lVertices.Count == 0)
			return null;
		if (id < 0 || id >= m_lVertices.Count)
			return null;
		return m_lVertices[id];
	}

	public virtual Vertex[] GetNeighbours( Vertex v )
	{
		if (ReferenceEquals(m_lNeighbours, null) || m_lNeighbours.Count == 0)
			return new Vertex[0];
		if (v.m_id < 0 || v.m_id >= m_lNeighbours.Count)
			return new Vertex[0];
		return m_lNeighbours[v.m_id].ToArray();
	}

	public virtual Edge[] GetEdges(Vertex v)
	{
		return null;
	}
	#endregion

	#region Events
	#endregion
}
