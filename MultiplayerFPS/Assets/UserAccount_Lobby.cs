using UnityEngine;
using UnityEngine.UI;

public class UserAccount_Lobby : MonoBehaviour 
{
	#region Statics
	#endregion

	#region PublicVariables
	public Text usernameText;
	#endregion

	#region PrivateVariables
	#endregion

	#region Fields
	#endregion

	#region Functions
	void Start()
	{
		if ( UserAccountManager.ISLOGGEDIN )
			usernameText.text = UserAccountManager.LOGGEDIN_USERNAME;
	}

	public void LogOut()
	{
		if (UserAccountManager.ISLOGGEDIN)
			UserAccountManager.instance.LogOut();
	}
	#endregion

	#region Events
	#endregion
}
