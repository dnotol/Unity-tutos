using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Vertex : MonoBehaviour 
{
	#region Statics
	#endregion

	#region PublicVariables
	public int m_id;
	public List<Edge> m_lNeighbours;
	[HideInInspector]
	public Vertex m_pPrev;
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
