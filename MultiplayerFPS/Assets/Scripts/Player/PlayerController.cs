using UnityEngine;

[RequireComponent(typeof(PlayerMotor), typeof(ConfigurableJoint), typeof(Animator))]
public class PlayerController : MonoBehaviour 
{
	#region Statics
	#endregion

	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField]
	private float speed = 5f;
	[SerializeField]
	private float lookSensitivity = 3f;

	[SerializeField]
	private float thrusterForce = 1000f;
	[SerializeField]
	private float thrusterFuelBurnSpeed = 1f;
	[SerializeField]
	private float thrusterFuelRegenSpeed = .3f;
	private float thrusterFuelAmount = 1f;

	[SerializeField]
	private LayerMask environmentMask;

	[Header("Joint(spring) options")]
	[SerializeField]
	private float jointSpring = 20;
	[SerializeField]
	private float jointMaxForce = 40;

	private PlayerMotor motor;
	private ConfigurableJoint configurableJoint;
	private Animator animator;
	#endregion

	#region Fields
	#endregion

	#region Functions
	void Start()
	{
		motor = GetComponent<PlayerMotor>();
		configurableJoint = GetComponent<ConfigurableJoint>();
		animator = GetComponent<Animator>();
		SetJointSetting(jointSpring);
	}

	void Update()
	{
		// Setting target position for the spring so the physics work right when on top of an object
		RaycastHit _hit;
		if (Physics.Raycast(transform.position, Vector3.down, out _hit, 100f, environmentMask))
		{
			configurableJoint.targetPosition = new Vector3(0f, -_hit.point.y, 0f);
		}
		else
		{
			configurableJoint.targetPosition = new Vector3(0f, 0f, 0f);
		}

		if (PauseMenu.IsOn)
			return;

		float _xMov = Input.GetAxis("Horizontal");
		float _zMov = Input.GetAxis("Vertical");

		Vector3 _movHorizontal = transform.right * _xMov;
		Vector3 _movVertical = transform.forward * _zMov;

		Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

		motor.Move(_velocity);

		animator.SetFloat("ForwardVelocity", _zMov);

		float _yRot = Input.GetAxisRaw("Mouse X");
		Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;
		motor.Rotate(_rotation);

		float _xRot = Input.GetAxisRaw("Mouse Y");
		float _cameraRotationX = _xRot * lookSensitivity;
		motor.RotateCamera(_cameraRotationX);

		Vector3 _thrusterForce = Vector3.zero;
		if (Input.GetButton("Jump") && thrusterFuelAmount > 0f)
		{
			thrusterFuelAmount -= thrusterFuelBurnSpeed * Time.deltaTime;
			if ( thrusterFuelAmount >= 0.01f )
				_thrusterForce = Vector3.up * thrusterForce;
			SetJointSetting(0f);
		}
		else
		{
			thrusterFuelAmount += thrusterFuelRegenSpeed * Time.deltaTime;
			SetJointSetting(jointSpring);
		}
		thrusterFuelAmount = Mathf.Clamp01(thrusterFuelAmount);

		motor.ApplyThruster(_thrusterForce);
	}

	public float GetThrusterFuelAmount()
	{
		return thrusterFuelAmount;
	}

	private void SetJointSetting( float _jointSpring )
	{
		configurableJoint.yDrive = new JointDrive{
			positionSpring = _jointSpring,
			maximumForce = jointMaxForce };
	}
	#endregion

	#region Events
	#endregion
}
