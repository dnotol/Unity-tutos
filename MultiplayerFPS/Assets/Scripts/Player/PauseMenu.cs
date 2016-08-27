using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections;

public class PauseMenu : MonoBehaviour 
{
	#region Statics
	#endregion

	#region PublicVariables
	public static bool IsOn = false;
	#endregion

	#region PrivateVariables
	private NetworkManager networkManager;
	#endregion

	#region Fields
	#endregion

	#region Functions
	public void Start()
	{
		networkManager = NetworkManager.singleton;
	}

	public void LeaveRoom()
	{
		MatchInfo _matchInfo = networkManager.matchInfo;
		networkManager.matchMaker.DropConnection(_matchInfo.networkId, _matchInfo.nodeId, 0, networkManager.OnDropConnection);
		networkManager.StopHost();
	}
	#endregion

	#region Events
	#endregion
}
