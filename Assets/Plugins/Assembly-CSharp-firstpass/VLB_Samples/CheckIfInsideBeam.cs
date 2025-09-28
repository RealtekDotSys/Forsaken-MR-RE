namespace VLB_Samples
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Collider), typeof(global::UnityEngine.Rigidbody), typeof(global::UnityEngine.MeshRenderer))]
	public class CheckIfInsideBeam : global::UnityEngine.MonoBehaviour
	{
		private bool isInsideBeam;

		private global::UnityEngine.Material m_Material;

		private global::UnityEngine.Collider m_Collider;

		private void Start()
		{
			m_Collider = GetComponent<global::UnityEngine.Collider>();
			global::UnityEngine.MeshRenderer component = GetComponent<global::UnityEngine.MeshRenderer>();
			if ((bool)component)
			{
				m_Material = component.material;
			}
		}

		private void Update()
		{
			if ((bool)m_Material)
			{
				m_Material.SetColor("_Color", isInsideBeam ? global::UnityEngine.Color.green : global::UnityEngine.Color.red);
			}
		}

		private void FixedUpdate()
		{
			isInsideBeam = false;
		}

		private void OnTriggerStay(global::UnityEngine.Collider trigger)
		{
			global::VLB.VolumetricLightBeam component = trigger.GetComponent<global::VLB.VolumetricLightBeam>();
			if ((bool)component)
			{
				isInsideBeam = !component.IsColliderHiddenByDynamicOccluder(m_Collider);
			}
		}
	}
}
