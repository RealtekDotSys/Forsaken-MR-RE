public class AnimatronicAudioManager : global::UnityEngine.MonoBehaviour
{
	private float CameraStaticStrength;

	public void SetVolume(float level)
	{
	}

	public void SetMute(bool shouldMute)
	{
	}

	public void SetRtpcValue(string rtpcName, float rtpcValue)
	{
		global::FMODUnity.RuntimeManager.StudioSystem.setParameterByName(rtpcName, rtpcValue);
		if (rtpcName == "CameraStaticStrength")
		{
			CameraStaticStrength = rtpcValue;
		}
	}

	public void SendEvent(string eventName)
	{
		global::UnityEngine.Debug.Log("received event request " + eventName);
		global::FMODUnity.RuntimeManager.PlayOneShot("event:/" + eventName, base.transform.position);
	}

	private void OnGUI()
	{
		_ = CameraStaticStrength;
		_ = 0f;
	}
}
