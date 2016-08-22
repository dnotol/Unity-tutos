using UnityEngine;
using System.Collections.Generic;

public class VertexVisibility : Vertex 
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
	void Awake()
	{
		m_lNeighbours = new List<Edge>();
	}

	public void FindNeighbours(List<Vertex> vertices)
	{
		Collider _C = gameObject.GetComponent<Collider>();
		_C.enabled = false;
		Vector3 _pDirection = Vector3.zero;
		Vector3 _pOrigin = transform.position;
		Vector3 _pTarget = Vector3.zero;
		RaycastHit[] _aHits;
		Ray _pRay;
		float distance = 0f;
		for (int i = 0; i < vertices.Count; i++)
		{
			if (vertices[i] == this)
				continue;
			_pTarget = vertices[i].transform.position;
			_pDirection = _pTarget - _pDirection;
			distance = _pDirection.magnitude;
			_pRay = new Ray(_pOrigin, _pDirection);
			_aHits = Physics.RaycastAll(_pRay, distance);
			if (_aHits.Length == 1)
			{
				if (_aHits[0].collider.gameObject.tag.Equals("Vertex"))
				{
					Edge e = new Edge();
					e.m_fCost = distance;
					GameObject go = _aHits[0].collider.gameObject;
					Vertex v = go.GetComponent<Vertex>();
					if (v != vertices[i])
						continue;
					e.m_pVertex = v;
					m_lNeighbours.Add(e);
				}
			}
		}
		_C.enabled = true;
	}
	#endregion

	#region Events
	#endregion
}
