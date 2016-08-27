using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(PlayerSetup))]
public class Player : NetworkBehaviour
{
	#region Statics
	#endregion

	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SyncVar]
	private bool _isDead = false;
	[SerializeField]
	int maxHealth = 100;
	[SyncVar]
	int currentHealth;
	[SerializeField]
	private Behaviour[] disableOnDeath;
	private bool[] wasEnabled;

	[SerializeField]
	private GameObject[] disableGameObjectOnDeath;

	[SerializeField]
	private GameObject deathEffect;

	[SerializeField]
	private GameObject spawnEffect;

	private bool isFirstSetup = true;
	#endregion

	#region Fields
	public bool IsDead
	{
		get { return _isDead; }
		protected set { _isDead = value; }
	}
	#endregion

	#region Functions
	public void Setup()
	{
		if (isLocalPlayer)
		{
			// Switch cameras
			GameManager.instance.SetSceneCameraActive(false);
			GetComponent<PlayerSetup>().playerUIInstance.SetActive(true);
		}
		CmdBroadCastNewPlayerSetup();
	}

	[Command]
	private void CmdBroadCastNewPlayerSetup()
	{
		RpcSetupPlayerOnAllClient();
	}

	[ClientRpc]
	private void RpcSetupPlayerOnAllClient()
	{
		if ( isFirstSetup )
		{
			wasEnabled = new bool[disableOnDeath.Length];
			for (int i = 0; i < wasEnabled.Length; i++)
			{
				wasEnabled[i] = disableOnDeath[i].enabled;
			}

			isFirstSetup = false;
		}
		SetDefaults();
	}

// 	void Update()
// 	{
// 		if (!isLocalPlayer)
// 			return;
// 		if (Input.GetKey(KeyCode.K))
// 			RpcTakeDamage(999999);
// 	}

	[ClientRpc]
	public void RpcTakeDamage(int _damage)
	{
		if (IsDead)
			return;

		currentHealth -= _damage;
		Debug.Log(transform.name + " now has " + currentHealth + " health.");

		if (currentHealth <= 0)
			Die();
	}

	private void Die()
	{
		IsDead = true;

		// Disable components
		for (int i = 0; i < disableOnDeath.Length; i++)
		{
			disableOnDeath[i].enabled = false;
		}

		// Disable gameobjects
		for (int i = 0; i < disableGameObjectOnDeath.Length; i++)
		{
			disableGameObjectOnDeath[i].SetActive(false);
		}

		// Disable the collider
		Collider _col = GetComponent<Collider>();
		if (_col != null)
			_col.enabled = false;

		// Spawn a death effect
		GameObject _explosionGFX = Instantiate(deathEffect, transform.position, Quaternion.identity) as GameObject;
		Destroy(_explosionGFX, 3f);

		//Switch cameras
		if (isLocalPlayer)
		{
			GameManager.instance.SetSceneCameraActive(true);
			GetComponent<PlayerSetup>().playerUIInstance.SetActive(false);
		}

		Debug.Log(transform.name + " is dead.");

		StartCoroutine(Respawn());
	}

	IEnumerator Respawn()
	{
		yield return new WaitForSeconds(GameManager.instance.matchSetting.respawnDelay);

		Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
		transform.position = _spawnPoint.position;
		transform.rotation = _spawnPoint.rotation;

		yield return new WaitForSeconds(0.1f);

		Setup();
	}

	private void SetDefaults()
	{
		IsDead = false;
		currentHealth = maxHealth;

		//enable the components
		for (int i = 0; i < disableOnDeath.Length; i++)
		{
			disableOnDeath[i].enabled = wasEnabled[i];
		}

		//enable the gameobjetcs
		for (int i = 0; i < disableGameObjectOnDeath.Length; i++)
		{
			disableGameObjectOnDeath[i].SetActive(true);
		}

		Collider _col = GetComponent<Collider>();
		if (_col != null)
			_col.enabled = true;

		// Create spawn effect
		GameObject _explosionGFX = Instantiate(spawnEffect, transform.position, Quaternion.identity) as GameObject;
		Destroy(_explosionGFX, 3f);
	}
	#endregion

	#region Events
	#endregion
}
