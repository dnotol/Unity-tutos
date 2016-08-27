using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SurfaceFlow : MonoBehaviour 
{
	#region Statics
	#endregion

	#region PublicVariables
	public SurfaceCreator surface;
	#endregion

	#region PrivateVariables
	private ParticleSystem system;
	private ParticleSystem.Particle[] particles;
	#endregion

	#region Fields
	#endregion

	#region Functions
	private void LateUpdate()
	{
		if (system == null)
		{
			system = GetComponent<ParticleSystem>();
		}
		if (particles == null || particles.Length < system.maxParticles)
		{
			particles = new ParticleSystem.Particle[system.maxParticles];
		}
		int particleCount = system.GetParticles(particles);
		PositionParticles();
		system.SetParticles(particles, particleCount);
	}

	private void PositionParticles()
	{
		Quaternion q = Quaternion.Euler(surface.rotation);
		Quaternion qInv = Quaternion.Inverse(q);
		NoiseMethod method = Noise.noiseMethods[(int)surface.type][surface.dimensions - 1];
		float amplitude = surface.damping ? surface.strength / surface.frequency : surface.strength;
		for (int i = 0; i < particles.Length; i++)
		{
			Vector3 position = particles[i].position;
			Vector3 point = q * new Vector3(position.x, position.z) + surface.offset;
			NoiseSample sample = Noise.Sum(method, point,
				surface.frequency, surface.octaves, surface.lacunarity, surface.persistence);
			sample = surface.type == NoiseMethodType.Value ? (sample - 0.5f) : (sample * 0.5f);
			sample *= amplitude;
			sample.derivative = qInv * sample.derivative;
			position.y = sample.value;
			particles[i].position = position;
		}
	}
	#endregion

	#region Events
	#endregion
}
