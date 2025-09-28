namespace VLB_Samples
{
	public class LightGenerator : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.Range(1f, 100f)]
		[global::UnityEngine.SerializeField]
		private int CountX = 10;

		[global::UnityEngine.Range(1f, 100f)]
		[global::UnityEngine.SerializeField]
		private int CountY = 10;

		[global::UnityEngine.SerializeField]
		private float OffsetUnits = 1f;

		[global::UnityEngine.SerializeField]
		private float PositionY = 1f;

		[global::UnityEngine.SerializeField]
		private bool NoiseEnabled;

		[global::UnityEngine.SerializeField]
		private bool AddLight = true;

		public void Generate()
		{
			for (int i = 0; i < CountX; i++)
			{
				for (int j = 0; j < CountY; j++)
				{
					global::UnityEngine.GameObject gameObject = null;
					gameObject = ((!AddLight) ? new global::UnityEngine.GameObject("Light_" + i + "_" + j, typeof(global::VLB.VolumetricLightBeam), typeof(global::VLB_Samples.Rotater)) : new global::UnityEngine.GameObject("Light_" + i + "_" + j, typeof(global::UnityEngine.Light), typeof(global::VLB.VolumetricLightBeam), typeof(global::VLB_Samples.Rotater)));
					gameObject.transform.position = new global::UnityEngine.Vector3((float)i * OffsetUnits, PositionY, (float)j * OffsetUnits);
					gameObject.transform.rotation = global::UnityEngine.Quaternion.Euler((float)global::UnityEngine.Random.Range(-45, 45) + 90f, global::UnityEngine.Random.Range(0, 360), 0f);
					global::VLB.VolumetricLightBeam component = gameObject.GetComponent<global::VLB.VolumetricLightBeam>();
					if (AddLight)
					{
						global::UnityEngine.Light component2 = gameObject.GetComponent<global::UnityEngine.Light>();
						component2.type = global::UnityEngine.LightType.Spot;
						component2.color = new global::UnityEngine.Color(global::UnityEngine.Random.value, global::UnityEngine.Random.value, global::UnityEngine.Random.value, 1f);
						component2.range = global::UnityEngine.Random.Range(3f, 8f);
						component2.intensity = global::UnityEngine.Random.Range(0.2f, 5f);
						component2.spotAngle = global::UnityEngine.Random.Range(10f, 90f);
						if (global::VLB.Config.Instance.geometryOverrideLayer)
						{
							component2.cullingMask = ~(1 << global::VLB.Config.Instance.geometryLayerID);
						}
					}
					else
					{
						component.color = new global::UnityEngine.Color(global::UnityEngine.Random.value, global::UnityEngine.Random.value, global::UnityEngine.Random.value, 1f);
						component.fallOffEnd = global::UnityEngine.Random.Range(3f, 8f);
						component.spotAngle = global::UnityEngine.Random.Range(10f, 90f);
					}
					component.coneRadiusStart = global::UnityEngine.Random.Range(0f, 0.1f);
					component.geomCustomSides = global::UnityEngine.Random.Range(12, 36);
					component.fresnelPow = global::UnityEngine.Random.Range(1f, 7.5f);
					component.noiseEnabled = NoiseEnabled;
					gameObject.GetComponent<global::VLB_Samples.Rotater>().EulerSpeed = new global::UnityEngine.Vector3(0f, global::UnityEngine.Random.Range(-500, 500), 0f);
				}
			}
		}
	}
}
