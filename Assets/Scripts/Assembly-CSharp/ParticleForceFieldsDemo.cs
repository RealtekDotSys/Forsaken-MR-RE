public class ParticleForceFieldsDemo : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.Header("Overview")]
	public global::UnityEngine.UI.Text FPSText;

	public global::UnityEngine.UI.Text particleCountText;

	public global::UnityEngine.UI.Toggle postProcessingToggle;

	public global::UnityEngine.MonoBehaviour postProcessing;

	[global::UnityEngine.Header("Particle System Settings")]
	public global::UnityEngine.ParticleSystem particleSystem;

	private global::UnityEngine.ParticleSystem.MainModule particleSystemMainModule;

	private global::UnityEngine.ParticleSystem.EmissionModule particleSystemEmissionModule;

	public global::UnityEngine.UI.Text maxParticlesText;

	public global::UnityEngine.UI.Text particlesPerSecondText;

	public global::UnityEngine.UI.Slider maxParticlesSlider;

	public global::UnityEngine.UI.Slider particlesPerSecondSlider;

	[global::UnityEngine.Header("Attraction Particle Force Field Settings")]
	public global::MirzaBeig.Scripting.Effects.AttractionParticleForceField attractionParticleForceField;

	public global::UnityEngine.UI.Text attractionParticleForceFieldRadiusText;

	public global::UnityEngine.UI.Text attractionParticleForceFieldMaxForceText;

	public global::UnityEngine.UI.Text attractionParticleForceFieldArrivalRadiusText;

	public global::UnityEngine.UI.Text attractionParticleForceFieldArrivedRadiusText;

	public global::UnityEngine.UI.Text attractionParticleForceFieldPositionTextX;

	public global::UnityEngine.UI.Text attractionParticleForceFieldPositionTextY;

	public global::UnityEngine.UI.Text attractionParticleForceFieldPositionTextZ;

	public global::UnityEngine.UI.Slider attractionParticleForceFieldRadiusSlider;

	public global::UnityEngine.UI.Slider attractionParticleForceFieldMaxForceSlider;

	public global::UnityEngine.UI.Slider attractionParticleForceFieldArrivalRadiusSlider;

	public global::UnityEngine.UI.Slider attractionParticleForceFieldArrivedRadiusSlider;

	public global::UnityEngine.UI.Slider attractionParticleForceFieldPositionSliderX;

	public global::UnityEngine.UI.Slider attractionParticleForceFieldPositionSliderY;

	public global::UnityEngine.UI.Slider attractionParticleForceFieldPositionSliderZ;

	[global::UnityEngine.Header("Vortex Particle Force Field Settings")]
	public global::MirzaBeig.Scripting.Effects.VortexParticleForceField vortexParticleForceField;

	public global::UnityEngine.UI.Text vortexParticleForceFieldRadiusText;

	public global::UnityEngine.UI.Text vortexParticleForceFieldMaxForceText;

	public global::UnityEngine.UI.Text vortexParticleForceFieldRotationTextX;

	public global::UnityEngine.UI.Text vortexParticleForceFieldRotationTextY;

	public global::UnityEngine.UI.Text vortexParticleForceFieldRotationTextZ;

	public global::UnityEngine.UI.Text vortexParticleForceFieldPositionTextX;

	public global::UnityEngine.UI.Text vortexParticleForceFieldPositionTextY;

	public global::UnityEngine.UI.Text vortexParticleForceFieldPositionTextZ;

	public global::UnityEngine.UI.Slider vortexParticleForceFieldRadiusSlider;

	public global::UnityEngine.UI.Slider vortexParticleForceFieldMaxForceSlider;

	public global::UnityEngine.UI.Slider vortexParticleForceFieldRotationSliderX;

	public global::UnityEngine.UI.Slider vortexParticleForceFieldRotationSliderY;

	public global::UnityEngine.UI.Slider vortexParticleForceFieldRotationSliderZ;

	public global::UnityEngine.UI.Slider vortexParticleForceFieldPositionSliderX;

	public global::UnityEngine.UI.Slider vortexParticleForceFieldPositionSliderY;

	public global::UnityEngine.UI.Slider vortexParticleForceFieldPositionSliderZ;

	private void Start()
	{
		if ((bool)postProcessing)
		{
			postProcessingToggle.isOn = postProcessing.enabled;
		}
		particleSystemMainModule = particleSystem.main;
		particleSystemEmissionModule = particleSystem.emission;
		maxParticlesSlider.value = particleSystemMainModule.maxParticles;
		particlesPerSecondSlider.value = particleSystemEmissionModule.rateOverTime.constant;
		maxParticlesText.text = "Max Particles: " + maxParticlesSlider.value;
		particlesPerSecondText.text = "Particles Per Second: " + particlesPerSecondSlider.value;
		attractionParticleForceFieldRadiusSlider.value = attractionParticleForceField.radius;
		attractionParticleForceFieldMaxForceSlider.value = attractionParticleForceField.force;
		attractionParticleForceFieldArrivalRadiusSlider.value = attractionParticleForceField.arrivalRadius;
		attractionParticleForceFieldArrivedRadiusSlider.value = attractionParticleForceField.arrivedRadius;
		global::UnityEngine.Vector3 position = attractionParticleForceField.transform.position;
		attractionParticleForceFieldPositionSliderX.value = position.x;
		attractionParticleForceFieldPositionSliderY.value = position.y;
		attractionParticleForceFieldPositionSliderZ.value = position.z;
		attractionParticleForceFieldRadiusText.text = "Radius: " + attractionParticleForceFieldRadiusSlider.value;
		attractionParticleForceFieldMaxForceText.text = "Max Force: " + attractionParticleForceFieldMaxForceSlider.value;
		attractionParticleForceFieldArrivalRadiusText.text = "Arrival Radius: " + attractionParticleForceFieldArrivalRadiusSlider.value;
		attractionParticleForceFieldArrivedRadiusText.text = "Arrived Radius: " + attractionParticleForceFieldArrivedRadiusSlider.value;
		attractionParticleForceFieldPositionTextX.text = "Position X: " + attractionParticleForceFieldPositionSliderX.value;
		attractionParticleForceFieldPositionTextY.text = "Position Y: " + attractionParticleForceFieldPositionSliderY.value;
		attractionParticleForceFieldPositionTextZ.text = "Position Z: " + attractionParticleForceFieldPositionSliderZ.value;
		vortexParticleForceFieldRadiusSlider.value = vortexParticleForceField.radius;
		vortexParticleForceFieldMaxForceSlider.value = vortexParticleForceField.force;
		global::UnityEngine.Vector3 eulerAngles = vortexParticleForceField.transform.eulerAngles;
		vortexParticleForceFieldRotationSliderX.value = eulerAngles.x;
		vortexParticleForceFieldRotationSliderY.value = eulerAngles.y;
		vortexParticleForceFieldRotationSliderZ.value = eulerAngles.z;
		global::UnityEngine.Vector3 position2 = vortexParticleForceField.transform.position;
		vortexParticleForceFieldPositionSliderX.value = position2.x;
		vortexParticleForceFieldPositionSliderY.value = position2.y;
		vortexParticleForceFieldPositionSliderZ.value = position2.z;
		vortexParticleForceFieldRadiusText.text = "Radius: " + vortexParticleForceFieldRadiusSlider.value;
		vortexParticleForceFieldMaxForceText.text = "Max Force: " + vortexParticleForceFieldMaxForceSlider.value;
		vortexParticleForceFieldRotationTextX.text = "Rotation X: " + vortexParticleForceFieldRotationSliderX.value;
		vortexParticleForceFieldRotationTextY.text = "Rotation Y: " + vortexParticleForceFieldRotationSliderY.value;
		vortexParticleForceFieldRotationTextZ.text = "Rotation Z: " + vortexParticleForceFieldRotationSliderZ.value;
		vortexParticleForceFieldPositionTextX.text = "Position X: " + vortexParticleForceFieldPositionSliderX.value;
		vortexParticleForceFieldPositionTextY.text = "Position Y: " + vortexParticleForceFieldPositionSliderY.value;
		vortexParticleForceFieldPositionTextZ.text = "Position Z: " + vortexParticleForceFieldPositionSliderZ.value;
	}

	private void Update()
	{
		FPSText.text = "FPS: " + 1f / global::UnityEngine.Time.deltaTime;
		particleCountText.text = "Particle Count: " + particleSystem.particleCount;
	}

	public void ReloadScene()
	{
		global::UnityEngine.SceneManagement.SceneManager.LoadScene(global::UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	public void SetMaxParticles(float value)
	{
		particleSystemMainModule.maxParticles = (int)value;
		maxParticlesText.text = "Max Particles: " + value;
	}

	public void SetParticleEmissionPerSecond(float value)
	{
		particleSystemEmissionModule.rateOverTime = value;
		particlesPerSecondText.text = "Particles Per Second: " + value;
	}

	public void SetAttractionParticleForceFieldRadius(float value)
	{
		attractionParticleForceField.radius = value;
		attractionParticleForceFieldRadiusText.text = "Radius: " + value;
	}

	public void SetAttractionParticleForceFieldMaxForce(float value)
	{
		attractionParticleForceField.force = value;
		attractionParticleForceFieldMaxForceText.text = "Max Force: " + value;
	}

	public void SetAttractionParticleForceFieldArrivalRadius(float value)
	{
		attractionParticleForceField.arrivalRadius = value;
		attractionParticleForceFieldArrivalRadiusText.text = "Arrival Radius: " + value;
	}

	public void SetAttractionParticleForceFieldArrivedRadius(float value)
	{
		attractionParticleForceField.arrivedRadius = value;
		attractionParticleForceFieldArrivedRadiusText.text = "Arrived Radius: " + value;
	}

	public void SetAttractionParticleForceFieldPositionX(float value)
	{
		global::UnityEngine.Vector3 position = attractionParticleForceField.transform.position;
		position.x = value;
		attractionParticleForceField.transform.position = position;
		attractionParticleForceFieldPositionTextX.text = "Position X: " + value;
	}

	public void SetAttractionParticleForceFieldPositionY(float value)
	{
		global::UnityEngine.Vector3 position = attractionParticleForceField.transform.position;
		position.y = value;
		attractionParticleForceField.transform.position = position;
		attractionParticleForceFieldPositionTextY.text = "Position Y: " + value;
	}

	public void SetAttractionParticleForceFieldPositionZ(float value)
	{
		global::UnityEngine.Vector3 position = attractionParticleForceField.transform.position;
		position.z = value;
		attractionParticleForceField.transform.position = position;
		attractionParticleForceFieldPositionTextZ.text = "Position Z: " + value;
	}

	public void SetVortexParticleForceFieldRadius(float value)
	{
		vortexParticleForceField.radius = value;
		vortexParticleForceFieldRadiusText.text = "Radius: " + value;
	}

	public void SetVortexParticleForceFieldMaxForce(float value)
	{
		vortexParticleForceField.force = value;
		vortexParticleForceFieldMaxForceText.text = "Max Force: " + value;
	}

	public void SetVortexParticleForceFieldRotationX(float value)
	{
		global::UnityEngine.Vector3 eulerAngles = vortexParticleForceField.transform.eulerAngles;
		eulerAngles.x = value;
		vortexParticleForceField.transform.eulerAngles = eulerAngles;
		vortexParticleForceFieldRotationTextX.text = "Rotation X: " + value;
	}

	public void SetVortexParticleForceFieldRotationY(float value)
	{
		global::UnityEngine.Vector3 eulerAngles = vortexParticleForceField.transform.eulerAngles;
		eulerAngles.y = value;
		vortexParticleForceField.transform.eulerAngles = eulerAngles;
		vortexParticleForceFieldRotationTextY.text = "Rotation Y: " + value;
	}

	public void SetVortexParticleForceFieldRotationZ(float value)
	{
		global::UnityEngine.Vector3 eulerAngles = vortexParticleForceField.transform.eulerAngles;
		eulerAngles.z = value;
		vortexParticleForceField.transform.eulerAngles = eulerAngles;
		vortexParticleForceFieldRotationTextZ.text = "Rotation Z: " + value;
	}

	public void SetVortexParticleForceFieldPositionX(float value)
	{
		global::UnityEngine.Vector3 position = vortexParticleForceField.transform.position;
		position.x = value;
		vortexParticleForceField.transform.position = position;
		vortexParticleForceFieldPositionTextX.text = "Position X: " + value;
	}

	public void SetVortexParticleForceFieldPositionY(float value)
	{
		global::UnityEngine.Vector3 position = vortexParticleForceField.transform.position;
		position.y = value;
		vortexParticleForceField.transform.position = position;
		vortexParticleForceFieldPositionTextY.text = "Position Y: " + value;
	}

	public void SetVortexParticleForceFieldPositionZ(float value)
	{
		global::UnityEngine.Vector3 position = vortexParticleForceField.transform.position;
		position.z = value;
		vortexParticleForceField.transform.position = position;
		vortexParticleForceFieldPositionTextZ.text = "Position Z: " + value;
	}
}
