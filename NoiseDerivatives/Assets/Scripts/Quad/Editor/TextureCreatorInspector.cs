using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TextureCreator))]
public class TextureCreatorInspector : Editor
{
	private TextureCreator m_pCreator;

	private void OnEnable()
	{
		m_pCreator = target as TextureCreator;
		Undo.undoRedoPerformed += RefreshCreator;
	}

	private void OnDisable()
	{
		Undo.undoRedoPerformed -= RefreshCreator;
	}

	private void RefreshCreator()
	{
		if (Application.isPlaying)
		{
			m_pCreator.FillTexture();
		}
	}

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		DrawDefaultInspector();
		if ( EditorGUI.EndChangeCheck() )
		{
			RefreshCreator();
		}
	}
}