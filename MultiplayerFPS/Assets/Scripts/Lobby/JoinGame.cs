using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour
{
	#region Statics
	#endregion

	#region PublicVariables
	#endregion

	#region PrivateVariables
	List<GameObject> roomList = new List<GameObject>();

	[SerializeField]
	private GameObject roomListItemPrefab;

	[SerializeField]
	private Text status;

	[SerializeField]
	private Transform roomListParent;

	private NetworkManager networkManager;
	#endregion

	#region Fields
	#endregion

	#region Functions
	void Start()
	{
		networkManager = NetworkManager.singleton;
		if (networkManager.matchMaker == null)
			networkManager.StartMatchMaker();

		RefreshRoomList();
	}

	public void RefreshRoomList()
	{
		ClearRoomList();

		networkManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
		status.text = "Loading...";
	}

	public void OnMatchList(bool succes, string _extendedInfo, List<MatchInfoSnapshot> _matchList)
	{
		status.text = "";
		if ( !succes || _matchList == null )
		{
			status.text = "Can't reach room list.";
			return;
		}

		foreach(MatchInfoSnapshot match in _matchList )
		{
			GameObject _roomListItemGO = Instantiate( roomListItemPrefab );
			_roomListItemGO.transform.SetParent( roomListParent );

			RoomListItem _roomListItem = _roomListItemGO.GetComponent<RoomListItem>();
			if( _roomListItem != null )
			{
				_roomListItem.Setup(match, JoinRoom);
			}

			// Have a component sit on the gameobject that will take care of setting up the name / amount of users as well as setting a callback function that will join the game

			roomList.Add(_roomListItemGO);
		}

		if (roomList.Count == 0)
			status.text = "No room at the moment.";
	}

	private void ClearRoomList()
	{
		for (int i = 0; i < roomList.Count; i++)
		{
			Destroy(roomList[i]);
		}

		roomList.Clear();
	}

	public void JoinRoom(MatchInfoSnapshot _match)
	{
		networkManager.matchMaker.JoinMatch( _match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined );
		ClearRoomList();
		status.text = "Joining...";
	}

	#endregion

	#region Events
	#endregion
}
