namespace VLB
{
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.RequireComponent(typeof(global::VLB.VolumetricLightBeam))]
	[global::UnityEngine.HelpURL("http://saladgamer.com/vlb-doc/comp-triggerzone/")]
	public class TriggerZone : global::UnityEngine.MonoBehaviour
	{
		public bool setIsTrigger = true;

		public float rangeMultiplier = 1f;

		private const int kMeshColliderNumSides = 8;

		private global::UnityEngine.Mesh m_Mesh;

		private void Update()
		{
			global::VLB.VolumetricLightBeam component = GetComponent<global::VLB.VolumetricLightBeam>();
			if ((bool)component)
			{
				global::UnityEngine.MeshCollider orAddComponent = base.gameObject.GetOrAddComponent<global::UnityEngine.MeshCollider>();
				float lengthZ = component.fallOffEnd * rangeMultiplier;
				float radiusEnd = global::UnityEngine.Mathf.LerpUnclamped(component.coneRadiusStart, component.coneRadiusEnd, rangeMultiplier);
				m_Mesh = global::VLB.MeshGenerator.GenerateConeZ_Radius(lengthZ, component.coneRadiusStart, radiusEnd, 8, 0, cap: false, doubleSided: false);
				m_Mesh.hideFlags = global::VLB.Consts.ProceduralObjectsHideFlags;
				orAddComponent.sharedMesh = m_Mesh;
				if (setIsTrigger)
				{
					orAddComponent.convex = true;
					orAddComponent.isTrigger = true;
				}
				global::UnityEngine.Object.Destroy(this);
			}
		}
	}
}
