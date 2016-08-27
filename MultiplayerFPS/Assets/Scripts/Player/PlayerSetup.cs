using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player), typeof(PlayerController))]
public class PlayerSetup : NetworkBehaviour 
{
	#region Statics
	#endregion

	#region 
	[HideInInspector]
	public GameObject playerUIInstance;
	#endregion

	#region PrivateVariables
	[SerializeField]
	Behaviour[] componentsToDisable;

	[SerializeField]
	string remoteLayerName = "RemotePlayer";
	[SerializeField]
	string dontDrawLayerName = "DontDraw";
	[SerializeField]
	GameObject playerGraphics;

	[SerializeField]
	GameObject playerUIPrefab;
	
	#endregion

	#region Fields
	#endregion

	#region Functions
	void Start()
	{
		if (!isLocalPlayer)
		{
			DisableComponents();
			AssignRemoteLayer();
		}
		else
		{
			// Disable the player graphics for local player
			SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName) );

			// Create the player UI
			playerUIInstance = Instantiate(playerUIPrefab);
			playerUIInstance.name = playerUIPrefab.name;

			// configure player UI
			PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
			if (ui == null)
				Debug.Log("No playerUI component on PlayerUI prefab.");
			else
				ui.SetController(GetComponent<PlayerController>());

			GetComponent<Player>().Setup();
		}	
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		string _netID = GetComponent<NetworkIdentity>().netId.ToString();
		Player _player = GetComponent<Player>();
		GameManager.RegisterPlayer(_netID, _player);
	}

	void AssignRemoteLayer()
	{
		gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
	}

	void DisableComponents()
	{
		foreach (Behaviour bhv in componentsToDisable)
			bhv.enabled = false;
	}

	void OnDisable()
	{
		Destroy(playerUIInstance);

		if ( isLocalPlayer )
			GameManager.instance.SetSceneCameraActive(true);

		GameManager.UnregisterPlayer(transform.name);
	}

	private void SetLayerRecursively(GameObject obj, int newLayer)
	{
		obj.layer = newLayer;
		foreach( Transform child in obj.transform )
		{
			SetLayerRecursively(child.gameObject, newLayer);
		}
	}

	#endregion

	#region Events
	#endregion
}
