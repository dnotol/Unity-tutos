using UnityEngine;
using UnityEngine.Networking;


public class HostGame : MonoBehaviour 
{
	#region Statics
	#endregion

	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField]
	private uint roomSize = 6;

	private string roomName;

	private NetworkManager networkManager;
	#endregion

	#region Fields
	#endregion

	#region Functions
	public void Start()
	{
		networkManager = NetworkManager.singleton;
		if( networkManager.matchMaker == null )
		{
			networkManager.StartMatchMaker();
		}
	}

	public void SetRoomName(string _name)
	{
		roomName = _name;
	}

	public void CreateRoom()
	{
		if (roomName != "" && roomName != null)
		{
			//create room
			networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
		}
	}
	#endregion

	#region Events
	#endregion
}
