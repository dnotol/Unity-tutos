using UnityEngine;
using System.Collections;

public class VertexDirichlet : Vertex 
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
	public void OnTriggerEnter(Collider col)
	{
		string _ObjName = col.gameObject.name;
		if (_ObjName.Equals("Agent") || _ObjName.Equals("Player"))
		{
			VertexReport _Report = new VertexReport(m_id, col.gameObject);
			SendMessageUpwards("AddLocation", _Report);
		}
	}

	public void OnTriggerExit(Collider col)
	{
		string _ObjName = col.gameObject.name;
		if (_ObjName.Equals("Agent") || _ObjName.Equals("Player"))
		{
			VertexReport _Report = new VertexReport(m_id, col.gameObject);
			SendMessageUpwards("RemoveLocation", _Report);
		}
	}
	#endregion

	#region Events
	#endregion
}
