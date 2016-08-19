using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour 
{
	#region Statics
	#endregion

	#region PublicVariables
	public Mesh[] m_pMeshes;
	public Material m_pMaterial;
	public int m_iMaxDepth = 2;
	public float m_fChildScale = 0.5f;
	public float spawnProbability = 0.75f;
	public float m_fMaxRotationSpeed;
	public float m_fMaxTwist;
	#endregion

	#region PrivateVariables
	private int m_iDepth = 0;
	private Material[,] m_pMaterials;
	private float m_fRotationSpeed;
	private static Vector3[] childDirections = 
	{
		Vector3.up,
		Vector3.right,
		Vector3.left,
		Vector3.forward,
		Vector3.back
	};

	private static Quaternion[] childOrientations = 
	{
		Quaternion.identity,
		Quaternion.Euler(0f, 0f, -90f),
		Quaternion.Euler(0f, 0f, 90f),
		Quaternion.Euler(90f, 0f, 0f),
		Quaternion.Euler(-90f, 0f, 0f)
	};
	#endregion

	#region Fields
	private void Start()
	{
		transform.Rotate(Random.Range(-m_fMaxTwist, m_fMaxTwist), 0f, 0f);
		m_fRotationSpeed = Random.Range(-m_fMaxRotationSpeed, m_fMaxRotationSpeed);
		if (m_pMaterials == null)
		{
			InitializeMaterials();
		}
		gameObject.AddComponent<MeshFilter>().mesh = m_pMeshes[Random.Range(0, m_pMeshes.Length)];
		gameObject.AddComponent<MeshRenderer>().material = m_pMaterials[m_iDepth, Random.Range(0,2)];
		if (m_iDepth < m_iMaxDepth)
		{
			StartCoroutine(CreateChildren());
		}
	}

	private void InitializeMaterials()
	{
		m_pMaterials = new Material[m_iMaxDepth + 1, 2];
		for (int i = 0; i <= m_iMaxDepth; i++)
		{
			float _t = i / (m_iMaxDepth - 1f);
			_t *= _t;
			m_pMaterials[i,0] = new Material(m_pMaterial);
			m_pMaterials[i,0].color = Color.Lerp(Color.white, Color.yellow, _t);
			m_pMaterials[i,1] = new Material(m_pMaterial);
			m_pMaterials[i,1].color = Color.Lerp(Color.white, Color.cyan, _t);
		}
		m_pMaterials[m_iMaxDepth, 0].color = Color.magenta;
		m_pMaterials[m_iMaxDepth, 1].color = Color.red;
	}

	private void Initialize(Fractal parent, int childIndex)
	{
		m_fMaxTwist = parent.m_fMaxTwist;
		m_fMaxRotationSpeed = parent.m_fMaxRotationSpeed;
		m_pMeshes = parent.m_pMeshes;
		m_pMaterial = parent.m_pMaterial;
		m_pMaterials = parent.m_pMaterials;
		m_iMaxDepth = parent.m_iMaxDepth;
		m_iDepth = parent.m_iDepth + 1;
		m_fChildScale = parent.m_fChildScale;
		transform.parent = parent.transform;
		transform.localScale = Vector3.one * m_fChildScale;
		transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * m_fChildScale);
		transform.localRotation = childOrientations[childIndex];
		spawnProbability = parent.spawnProbability;
	}

	private IEnumerator CreateChildren()
	{
		for (int i = 0; i < childDirections.Length; i++)
		{
			if (Random.value < spawnProbability)
			{
				yield return new WaitForSeconds(Random.Range(0.1f, 0.9f));
				new GameObject("Fractal Child").
					AddComponent<Fractal>().Initialize(this, i);
			}
		}
	}

	private void Update()
	{
		transform.Rotate(0f, m_fRotationSpeed * Time.deltaTime, 0f);
	}
	#endregion

	#region Functions
	#endregion

	#region Events
	#endregion
}
