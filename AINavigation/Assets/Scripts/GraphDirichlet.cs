using UnityEngine;
using System.Collections.Generic;

public class GraphDirichlet : Graph
{
	Dictionary<int, List<int>> m_DobjToVertex;
	#region Statics
	#endregion

	#region PublicVariables
	public override void Start()
	{
		base.Start();
		m_DobjToVertex = new Dictionary<int, List<int>>();
	}

	public override void Load()
	{
		Vertex[] _aVerts = GameObject.FindObjectsOfType<Vertex>();
		m_lVertices = new List<Vertex>(_aVerts);
		for (int i = 0; i < m_lVertices.Count; i++)
		{
			VertexVisibility _pVv = m_lVertices[i] as VertexVisibility;
			_pVv.m_id = i;
			_pVv.FindNeighbours(m_lVertices);
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

	public Vertex GetNearestVertex(GameObject obj)
	{
		int _iObjId = obj.GetInstanceID();
		Vector3 _pObjPos = obj.transform.position;
		if (!m_DobjToVertex.ContainsKey(_iObjId))
			return null;
		List<int> _lVertIds = m_DobjToVertex[_iObjId];
		Vertex _pVertex = null;
		float _fDist = Mathf.Infinity;
		for (int i = 0; i < _lVertIds.Count; i++)
		{
			int _Id = _lVertIds[i];
			Vertex _V = m_lVertices[_Id];
			Vector3 vPos = _V.transform.position;
			float _fD = Vector3.Distance(_pObjPos, vPos);
			if (_fD < _fDist)
			{
				_pVertex = _V;
				_fDist = _fD;
			}
		}
		return _pVertex;
	}

	public override Vertex[] GetNeighbours(Vertex v)
	{
		List<Edge> _lEdges = v.m_lNeighbours;
		Vertex[] _aNs = new Vertex[_lEdges.Count];
		int i;
		for (i = 0; i < _lEdges.Count; i++)
		{
			_aNs[i] = _lEdges[i].m_pVertex;
		}
		return _aNs;
	}

	public override Edge[] GetEdges(Vertex v)
	{
		return m_lVertices[v.m_id].m_lNeighbours.ToArray();
	}

	public void AddLocation(VertexReport report)
	{
		int _iObjId = report.m_pObj.GetInstanceID();
		if (!m_DobjToVertex.ContainsKey(_iObjId))
		{
			m_DobjToVertex.Add(_iObjId, new List<int>());
		}
		m_DobjToVertex[_iObjId].Add(report.m_iVertex);
	}

	public void RemoveLocation(VertexReport report)
	{
		int _iObjId = report.m_pObj.GetInstanceID();
		m_DobjToVertex[_iObjId].Remove(report.m_iVertex);
	}
	#endregion

	#region PrivateVariables
	#endregion

	#region Fields
	#endregion

	#region Functions
	#endregion

	#region Events
	#endregion
}
