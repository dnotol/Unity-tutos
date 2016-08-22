using UnityEngine;
using System.Collections;

public class TextureCreator : MonoBehaviour 
{
	#region Statics
	#endregion

	#region PublicVariables
	[Range(2, 512)]
	public int m_iResolution = 256;
	public float m_fFrequency = 1f;
	[Range(1, 8)]
	public int m_fOctaves = 1;
	[Range(1f, 4f)]
	public float lacunarity = 2f;
	[Range(0f, 1f)]
	public float persistence = 0.5f;
	[Range(1, 3)]
	public int dimensions = 3;
	public NoiseMethodType type;
	public Gradient coloring;
	#endregion

	#region PrivateVariables
	private Texture2D m_pTexture;
	#endregion

	#region Fields
	#endregion

	#region Functions
	private void OnEnable()
	{
		if (m_pTexture == null)
		{
			m_pTexture = new Texture2D(m_iResolution, m_iResolution, TextureFormat.RGB24, true);
			m_pTexture.name = "Procedural Texture";
			m_pTexture.wrapMode = TextureWrapMode.Clamp;
			m_pTexture.filterMode = FilterMode.Trilinear;
			m_pTexture.anisoLevel = 9;
			GetComponent<MeshRenderer>().material.mainTexture = m_pTexture;
		}
		FillTexture();
	}

	private void Update()
	{
		if (transform.hasChanged)
		{
			transform.hasChanged = false;
			FillTexture();
		}
	}

	public void FillTexture()
	{
		if (m_pTexture.width != m_iResolution)
		{
			m_pTexture.Resize(m_iResolution, m_iResolution);
		}

		Vector3 point00 = transform.TransformPoint (new Vector3(-0.5f, -0.5f));
		Vector3 point10 = transform.TransformPoint (new Vector3(0.5f, -0.5f));
		Vector3 point01 = transform.TransformPoint (new Vector3(-0.5f, 0.5f));
		Vector3 point11 = transform.TransformPoint (new Vector3(0.5f, 0.5f));

		NoiseMethod method = Noise.noiseMethods[(int)type][dimensions - 1];
		float _fStepSize = 1f / m_iResolution;

		for (int y = 0; y < m_iResolution; y++)
		{
			Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * _fStepSize);
			Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * _fStepSize);
			for (int x = 0; x < m_iResolution; x++)
			{
				Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * _fStepSize);
				float sample = Noise.Sum(method, point, m_fFrequency, m_fOctaves, lacunarity, persistence);
				if (type != NoiseMethodType.Value)
				{
					sample = sample * 0.5f + 0.5f;
				}
				m_pTexture.SetPixel(x, y, coloring.Evaluate(sample));
			}
		}
		m_pTexture.Apply();
	}
	#endregion

	#region Events
	#endregion
}
