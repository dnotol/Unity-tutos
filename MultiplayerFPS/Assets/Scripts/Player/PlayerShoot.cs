using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour
{
	#region Statics
	#endregion

	#region PublicVariables
	#endregion

	#region PrivateVariables

	private PlayerWeapon currentWeapon;
	private WeaponManager weaponManager;

	[SerializeField]
	Camera cam;
	[SerializeField]
	LayerMask mask;
	private const string PLAYER_TAG = "Player";
	#endregion

	#region Fields
	#endregion

	#region Functions
	void Start()
	{
		if (cam == null)
		{
			Debug.Log("PlayerShoot : No camera referenced!");
			enabled = false;
		}

		weaponManager = GetComponent<WeaponManager>();
	}

	void Update()
	{
		currentWeapon = weaponManager.GetCurrentWeapon();

		if (PauseMenu.IsOn)
			return;

		if (currentWeapon.fireRate <= 0f)
		{
			if (Input.GetButtonDown("Fire1"))
			{
				Shoot();
			}
		}
		else
		{
			if (Input.GetButtonDown("Fire1"))
			{
				InvokeRepeating("Shoot", 0f, 1f / currentWeapon.fireRate);
			}
			else if (Input.GetButtonUp("Fire1"))
			{
				CancelInvoke("Shoot");
				//Maybe remove this later.
				weaponManager.GetCurrentWeaponGraphics().muzzleFlash.Stop();
			}
		}

	}

	//This is called on the server when a player shoots
	[Command]
	void CmdOnShoot()
	{
		RpcDoShootEffect();
	}

	//This is called on all clients when need to do a shoot effect
	[ClientRpc]
	void RpcDoShootEffect()
	{
		weaponManager.GetCurrentWeaponGraphics().muzzleFlash.Play();
	}

	//This is called on the server when we hit something
	[Command]
	void CmdOnHit(Vector3 _pos, Vector3 _normal)
	{
		RpcDoImpactEffect(_pos, _normal);
	}

	//This is called on all clients to spawn effects
	[ClientRpc]
	void RpcDoImpactEffect(Vector3 _pos, Vector3 _normal)
	{
		GameObject _hitEffect = Instantiate( weaponManager.GetCurrentWeaponGraphics().hitEffect, _pos, Quaternion.LookRotation(_normal) ) as GameObject;
		Destroy(_hitEffect, 2f);
	}

	[Client]
	void Shoot()
	{
		if (!isLocalPlayer)
			return;

		// We shoot, call the OnShoot on the server
		CmdOnShoot();

		Debug.Log("SHOOT !!");
		RaycastHit _hit;
		if (Physics.Raycast(new Ray(cam.transform.position, cam.transform.forward), out _hit, currentWeapon.range, mask))
		{
			if (_hit.collider.tag == PLAYER_TAG)
				CmdPlayerShot(_hit.collider.name, currentWeapon.damage);

			// We hit something, call the hit method on the server
			CmdOnHit(_hit.point, _hit.normal);
		}
	}

	[Command]
	void CmdPlayerShot(string _playerID, int _damage)
	{
		Debug.Log(_playerID + " has been shot !" );
		Player _player = GameManager.GetPlayer(_playerID);
		_player.RpcTakeDamage(_damage);
	}
	#endregion

	#region Events
	#endregion
}
