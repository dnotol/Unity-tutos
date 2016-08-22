using UnityEngine;
using System.Collections;

public class VertexReport : MonoBehaviour 
{
	#region Statics
	#endregion

	#region PublicVariables
	public int m_iVertex;
	public GameObject m_pObj;
	#endregion

	#region PrivateVariables
	#endregion

	#region Fields
	#endregion

	#region Functions
	public VertexReport(int vertexId, GameObject obj)
	{
		m_iVertex = vertexId;
		this.m_pObj = obj;
	}
	#endregion

	#region Events
	#endregion
}
