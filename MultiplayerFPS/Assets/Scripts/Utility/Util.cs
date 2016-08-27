using UnityEngine;

public class Util 
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
	public static void SetLayerRecursively( GameObject _obj, int _newLayer )
	{
		if (_obj == null)
			return;

		_obj.layer = _newLayer;

		foreach (Transform _child in _obj.transform)
		{
			if (_child == null)
				continue;
			SetLayerRecursively(_child.gameObject, _newLayer);
		}
	}
	#endregion

	#region Events
	#endregion
}
