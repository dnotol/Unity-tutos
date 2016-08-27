using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SurfaceCreator))]
public class SurfaceCreatorInspector : Editor
{
	#region Statics
	#endregion

	#region PublicVariables
	#endregion

	#region PrivateVariables
	private SurfaceCreator creator;
	#endregion

	#region Fields
	#endregion

	#region Functions
	private void OnEnable()
	{
		creator = target as SurfaceCreator;
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
			creator.Refresh();
		}
	}

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		DrawDefaultInspector();
		if (EditorGUI.EndChangeCheck())
		{
			RefreshCreator();
		}
	}
	#endregion

	#region Events
	#endregion
}
