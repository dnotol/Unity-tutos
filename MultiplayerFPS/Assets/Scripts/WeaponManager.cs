using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour
{
	#region Statics
	#endregion

	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField]
	private string weaponLayerName = "Weapon";
	[SerializeField]
	private Transform weaponHolder;
	[SerializeField]
	private PlayerWeapon primaryWeapon;
	private PlayerWeapon currentWeapon;
	private WeaponGraphics currentGraphics;
	#endregion

	#region Fields
	#endregion

	#region Functions
	void Start()
	{
		EquipWeapon(primaryWeapon);
	}

	public PlayerWeapon GetCurrentWeapon()
	{
		return currentWeapon;
	}

	public WeaponGraphics GetCurrentWeaponGraphics()
	{
		return currentGraphics;
	}

	void EquipWeapon(PlayerWeapon _weapon)
	{
		currentWeapon = _weapon;

		GameObject _weaponIns = Instantiate(currentWeapon.graphics, weaponHolder.position, weaponHolder.rotation) as GameObject;
		_weaponIns.transform.SetParent(weaponHolder);

		currentGraphics = _weaponIns.GetComponent<WeaponGraphics>();
		if (currentGraphics == null)
			Debug.Log("Error : no weaponGraphics component on the weapon object : " + _weaponIns.name);

		if (isLocalPlayer)
			Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(weaponLayerName));
	}
	#endregion

	#region Events
	#endregion
}
