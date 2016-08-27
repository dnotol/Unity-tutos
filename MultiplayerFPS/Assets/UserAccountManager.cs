using UnityEngine;
using System.Collections;
using DatabaseControl;
using UnityEngine.SceneManagement;

public class UserAccountManager : MonoBehaviour
{
	#region Statics
	#endregion

	#region PublicVariables
	public static UserAccountManager instance;
	public static string LOGGEDIN_USERNAME { get; protected set; } //stores username once logged in
	private static string LOGGEDIN_PASSWORD = ""; //stores password once logged in
	public static bool ISLOGGEDIN { get; protected set; }
	public static string LOGGEDIN_DATA { get; protected set; }

	public string lobbySceneName = "Lobby";
	public string loggedOutSceneName = "LoginMenu";
	#endregion

	#region PrivateVariables
	#endregion

	#region Fields
	#endregion

	#region Functions
	void Awake()
	{
		if ( instance != null )
		{
			Destroy(gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(this);
	}

	public void LogOut()
	{
		LOGGEDIN_USERNAME = "";
		LOGGEDIN_PASSWORD = "";

		SceneManager.LoadScene(loggedOutSceneName);
	}

	public void LogIn(string _userName, string _passWord)
	{
		LOGGEDIN_USERNAME = _userName;
		LOGGEDIN_PASSWORD = _passWord;
		ISLOGGEDIN = true;

		SceneManager.LoadScene(lobbySceneName);
	}

	public void SendData( string _data )
	{
		if ( ISLOGGEDIN )
		{
			StartCoroutine(sendSendDataRequest(LOGGEDIN_USERNAME, LOGGEDIN_PASSWORD, _data)); //calls function to send: send data request
		}
	}

	IEnumerator sendSendDataRequest(string username, string password, string data)
	{
		IEnumerator eee = DC.SetUserData(username, password, data);
		while (eee.MoveNext())
		{
			yield return eee.Current;
		}
		WWW returneddd = eee.Current as WWW;
		if (returneddd.text == "ContainsUnsupportedSymbol")
		{
			//One of the parameters contained a - symbol
			Debug.Log("Data Upload Error. Could be a server error. To check try again, if problem still occurs, contact us.");
		}
		if (returneddd.text == "Error")
		{
			//Error occurred. For more information of the error, DC.Login could
			//be used with the same username and password
			Debug.Log("Data Upload Error: Contains Unsupported Symbol '-'");
		}
	}

	public void GetData()
	{ //called when the 'Get Data' button on the data part is pressed

		if ( ISLOGGEDIN )
		{
			StartCoroutine(sendGetDataRequest(LOGGEDIN_USERNAME, LOGGEDIN_PASSWORD)); //calls function to send get data request
		}
	}

	IEnumerator sendGetDataRequest(string username, string password)
	{
		string _data = "ERROR";

		IEnumerator eeee = DC.GetUserData(username, password);
		while (eeee.MoveNext())
		{
			yield return eeee.Current;
		}
		WWW returnedddd = eeee.Current as WWW;
		if (returnedddd.text == "Error")
		{
			//Error occurred. For more information of the error, DC.Login could
			//be used with the same username and password
			Debug.Log("Data Upload Error. Could be a server error. To check try again, if problem still occurs, contact us.");
		}
		else {
			if (returnedddd.text == "ContainsUnsupportedSymbol")
			{
				//One of the parameters contained a - symbol
				Debug.Log("Get Data Error: Contains Unsupported Symbol '-'");
			}
			else {
				//Data received in returned.text variable
				string DataRecieved = returnedddd.text;
				_data = DataRecieved;
			}
		}

		LOGGEDIN_DATA = _data;
	}
	#endregion

	#region Events
	#endregion
}
