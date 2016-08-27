using UnityEngine;

public class PlayerUI : MonoBehaviour 
{
	#region Statics
	#endregion

	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField]
	RectTransform thrusterFuelAmount;

	[SerializeField]
	GameObject pauseMenu;

	private PlayerController controller;
	#endregion

	#region Fields
	#endregion

	#region Functions

	void Start()
	{
		PauseMenu.IsOn = false;
	}

	public void SetController(PlayerController _controller)
	{
		controller = _controller;
	}

	void Update()
	{
		SetFuelAmount( controller.GetThrusterFuelAmount() );

		if (Input.GetKeyDown(KeyCode.Escape))
			TogglePauseMenu();
	}

	void SetFuelAmount( float _amount )
	{
		thrusterFuelAmount.localScale = new Vector3(1f, _amount, 1f);
	}

	void TogglePauseMenu()
	{
		pauseMenu.SetActive(!pauseMenu.activeSelf);
		PauseMenu.IsOn = pauseMenu.activeSelf;
	}

	#endregion

	#region Events
	#endregion
}
