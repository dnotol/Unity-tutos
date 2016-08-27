using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class RoomListItem : MonoBehaviour 
{
	#region Statics
	#endregion

	#region PublicVariables
	public delegate void JoinRoomDelegate(MatchInfoSnapshot _match);
	#endregion

	#region PrivateVariables
	private JoinRoomDelegate joinRoomCallback;

	[SerializeField]
	private Text roomNameText;

	private MatchInfoSnapshot match;
	#endregion

	#region Fields
	#endregion

	#region Functions
	public void Setup(MatchInfoSnapshot _match, JoinRoomDelegate _joinRoomCallback )
	{
		match = _match;
		joinRoomCallback = _joinRoomCallback;

		roomNameText.text = _match.name + " (" + match.currentSize + "/" + match.maxSize + ")";
	}

	public void JoinRoom ()
	{
		joinRoomCallback.Invoke(match);
	}
	#endregion

	#region Events
	#endregion
}
