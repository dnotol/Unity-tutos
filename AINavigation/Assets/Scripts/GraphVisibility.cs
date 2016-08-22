using UnityEngine;
using System.Collections.Generic;

public class GraphVisibility : Graph 
{
	#region Statics
	#endregion

	#region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region Fields
	#endregion

	#region Functions
	public override void Load()
	{
		Vertex[] _aVerts = GameObject.FindObjectsOfType<Vertex>();
		m_lVertices = new List<Vertex>(_aVerts);
		for (int i = 0; i < m_lVertices.Count; i++)
		{
			VertexVisibility vv = m_lVertices[i] as VertexVisibility;
			vv.m_id = i;
			vv.FindNeighbours(m_lVertices);
		}
	}

	public override Vertex GetNearestVertex(Vector3 position)
	{
		Vertex vertex = null;
		float dist = Mathf.Infinity;
		float distNear = dist;
		Vector3 posVertex = Vector3.zero;
		for (int i = 0; i < m_lVertices.Count; i++)
		{
			posVertex = m_lVertices[i].transform.position;
			dist = Vector3.Distance(position, posVertex);
			if (dist < distNear)
			{
				distNear = dist;
				vertex = m_lVertices[i];
			}
		}
		return vertex;
	}

	public override Vertex[] GetNeighbours(Vertex v)
	{
		List<Edge> edges = v.m_lNeighbours;
		Vertex[] ns = new Vertex[edges.Count];
		int i;
		for (i = 0; i < edges.Count; i++)
		{
			ns[i] = edges[i].m_pVertex;
		}
		return ns;
	}

	public override Edge[] GetEdges(Vertex v)
	{
		return m_lVertices[v.m_id].m_lNeighbours.ToArray();
	}
	#endregion

	#region Events
	#endregion
}
