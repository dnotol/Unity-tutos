using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour 
{
	#region Statics
	#endregion

	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField]
	private Camera cam;
	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private float cameraRotationX = 0f;
	private float currentCameraRotationX = 0f;
	private Vector3 thrusterForce = Vector3.zero;

	[SerializeField]
	private float cameraRotationLimit = 85f;

	private Rigidbody rb;
	#endregion

	#region Fields
	#endregion

	#region Functions
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void Move( Vector3 _movement )
	{
		velocity = _movement;
	}

	public void Rotate(Vector3 _rotation)
	{
		rotation = _rotation;
	}

	public void RotateCamera(float  _rotation)
	{
		cameraRotationX = _rotation;
	}

	void FixedUpdate()
	{
		PerformMovement();
		PerformRotation();
	}

	private void PerformMovement()
	{
		if ( velocity != Vector3.zero )
		{
			rb.MovePosition( rb.position + velocity * Time.deltaTime);
		}

		if ( thrusterForce != Vector3.zero )
		{
			rb.AddForce(thrusterForce * Time.deltaTime, ForceMode.Acceleration);
		}
	}

	private void PerformRotation()
	{
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
		if( cam != null )
		{
			currentCameraRotationX -= cameraRotationX;
			currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
			cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
		}
	}

	public void ApplyThruster(Vector3 _thrusterForce)
	{
		thrusterForce = _thrusterForce;
	}

	#endregion

	#region Events
	#endregion
}
