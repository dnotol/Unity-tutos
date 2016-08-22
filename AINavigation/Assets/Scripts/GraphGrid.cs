using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class GraphGrid : Graph 
{
	#region Statics
	#endregion

	#region PublicVariables
	public GameObject m_pObstaclePrefab;
	public string m_sMapName = "Arena.map";
	public bool m_bGet8vicinity = false;
	public float m_fCellSize = 1F;
	[Range(0, Mathf.Infinity)]
	public float m_fDefaultCost = 1F;
	[Range(0, Mathf.Infinity)]
	public float m_fMaximumCost = Mathf.Infinity;
	#endregion

	#region PrivateVariables
	string m_sMapsDir = "Maps";
	int m_iNumCols;
	int m_iNumRows;
	GameObject[] m_aVertexObjs;
	bool[,] m_aMapVertices;
	#endregion

	#region Fields
	#endregion

	#region Functions
	private int GridToId( int x, int y )
	{
		return Math.Max(m_iNumRows, m_iNumCols) * y + x;
	}

	private Vector2 IdToGrid( int id )
	{
		Vector2 _Location = Vector2.zero;
		_Location.y = Mathf.Floor(id / m_iNumCols);
		_Location.x = Mathf.Floor(id % m_iNumCols);
		return _Location;
	}

	private void LoadMap( string FileName )
	{
		string _Path = Application.dataPath + "/" + m_sMapsDir + "/" + FileName;
		try
		{
			StreamReader _StrmRdr = new StreamReader(_Path);
			using (_StrmRdr)
			{
				int _j = 0;
				int _i = 0;
				int _id = 0;
				string _Line;
				Vector3 _Position = Vector3.zero;
				Vector3 _Scale = Vector3.zero;
				_Line = _StrmRdr.ReadLine();
				_Line = _StrmRdr.ReadLine();//Height
				m_iNumRows = int.Parse(_Line.Split(' ')[1]);
				_Line = _StrmRdr.ReadLine();//Width
				m_iNumCols = int.Parse(_Line.Split(' ')[1]);
				_Line = _StrmRdr.ReadLine();//"map" line in file
				m_lVertices = new List<Vertex>(m_iNumRows * m_iNumCols);
				m_lNeighbours = new List<List<Vertex>>(m_iNumRows * m_iNumCols);
				m_lCosts = new List<List<float>>(m_iNumRows * m_iNumCols);

				m_aVertexObjs = new GameObject[m_iNumRows * m_iNumCols];
				m_aMapVertices = new bool[m_iNumRows, m_iNumCols];
				for (_i = 0; _i < m_iNumRows; _i++)
				{
					_Line = _StrmRdr.ReadLine();
					for (_j = 0; _j < m_iNumCols; _j++)
					{
						bool _IsGround = true;
						if (_Line[_j] != '.')
							_IsGround = false;
						m_aMapVertices[_i, _j] = _IsGround;
						_Position.x = _j * m_fCellSize;
						_Position.y = _i * m_fCellSize;
						_id = GridToId(_j, _i);
						if (_IsGround)
							m_aVertexObjs[_id] = Instantiate(m_pVertexPrefab, _Position, Quaternion.identity) as GameObject;
						else
							m_aVertexObjs[_id] = Instantiate(m_pObstaclePrefab, _Position, Quaternion.identity) as GameObject;
						m_aVertexObjs[_id].name = m_aVertexObjs[_id].name.Replace("(Clone)", _id.ToString());
						Vertex _V = m_aVertexObjs[_id].AddComponent<Vertex>();
						_V.m_id = _id;
						m_lVertices.Add(_V);
						m_lNeighbours.Add(new List<Vertex>());
						//Maybe uncomment this later
						//m_lCosts.Add(new List<float>());
						float _y = m_aVertexObjs[_id].transform.localScale.y;
						_Scale = new Vector3(m_fCellSize, _y, m_fCellSize);
						m_aVertexObjs[_id].transform.localScale = _Scale;
						m_aVertexObjs[_id].transform.parent = gameObject.transform;
					}
				}
				for (_i = 0; _i < m_iNumRows; _i++)
				{
					for (_j = 0; _j < m_iNumCols; _j++)
					{
						SetNeighbours(_j, _i);
					}
				}
			}
		}
		catch( Exception e )
		{
			Debug.LogException(e);
		}
	}

	public override void Load()
	{
		LoadMap(m_sMapName);
	}

	public override Vertex GetNearestVertex(Vector3 position)
	{
		int _col = (int)position.x;
		int _row = (int)position.z;
		Vector2 _p = new Vector2(_col, _row);
		List<Vector2> _explored = new List<Vector2>();
		Queue<Vector2> _Queue = new Queue<Vector2>();
		_Queue.Enqueue(_p);
		do
		{
			_p = _Queue.Dequeue();
			_col = (int)_p.x;
			_row = (int)_p.y;
			int _Id = GridToId(_col, _row);
			if (m_aMapVertices[_row, _col])
				return m_lVertices[_Id];
			if ( ! _explored.Contains(_p) )
			{
				_explored.Add(_p);
				int _i, _j;
				for (_i = _row - 1; _i <= _row + 1; _i++ )
				{
					for ( _j = _col - 1; _j <= _col + 1; _j++ )
					{
						if (_i < 0 || _j < 0)
							continue;
						if (_j >= m_iNumCols || _i >= m_iNumRows)
							continue;
						if (_i == _row && _j == _col)
							continue;
						_Queue.Enqueue(new Vector2(_j, _i));
					}
				}
			}
		} while (_Queue.Count != 0);
		return null; 
	}

	protected void SetNeighbours(int x, int y, bool get8 = false)
	{
		int _iCol = x;
		int _iRow = y;
		int _i, _j;
		int _iVertexId = GridToId(x, y);
		m_lNeighbours[_iVertexId] = new List<Vertex>();
		m_lCosts[_iVertexId] = new List<float>();
		Vector2[] _aPos = new Vector2[0];
		if (get8)
		{
			_aPos = new Vector2[8];
			int _c = 0;
			for (_i = _iRow - 1; _i <= _iRow + 1; _i++)
			{
				for (_j = _iCol - 1; _j <= _iCol; _j++)
				{
					_aPos[_c] = new Vector2(_j, _i);
					_c++;
				}
			}
		}
		else
		{
			_aPos = new Vector2[4];
			_aPos[0] = new Vector2(_iCol    , _iRow - 1);
			_aPos[1] = new Vector2(_iCol - 1, _iRow    );
			_aPos[2] = new Vector2(_iCol + 1, _iRow    );
			_aPos[3] = new Vector2(_iCol    , _iRow + 1);
		}
		foreach (Vector2 p in _aPos)
		{
			_i = (int)p.y;
			_j = (int)p.x;
			if ( _i < 0 || _j < 0 )
				continue;
			if (_i >= m_iNumRows || _j >= m_iNumCols)
				continue;
			if (!m_aMapVertices[_i, _j])
				continue;
			int _Id = GridToId(_j, _i);
			m_lCosts[_iVertexId].Add(m_fDefaultCost);
		}
	}

	#endregion

	#region Events
	#endregion
}
