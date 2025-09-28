namespace MirzaBeig.Scripting.Effects
{
	public class AttractionParticleAffector : global::MirzaBeig.Scripting.Effects.ParticleAffector
	{
		[global::UnityEngine.Header("Affector Controls")]
		public float arrivalRadius = 1f;

		public float arrivedRadius = 0.5f;

		private float arrivalRadiusSqr;

		private float arrivedRadiusSqr;

		protected override void Awake()
		{
			base.Awake();
		}

		protected override void Start()
		{
			base.Start();
		}

		protected override void Update()
		{
			base.Update();
		}

		protected override void LateUpdate()
		{
			float x = base.transform.lossyScale.x;
			arrivalRadiusSqr = arrivalRadius * arrivalRadius * x;
			arrivedRadiusSqr = arrivedRadius * arrivedRadius * x;
			base.LateUpdate();
		}

		protected override global::UnityEngine.Vector3 GetForce()
		{
			if (parameters.distanceToAffectorCenterSqr < arrivedRadiusSqr)
			{
				global::UnityEngine.Vector3 result = default(global::UnityEngine.Vector3);
				result.x = 0f;
				result.y = 0f;
				result.z = 0f;
				return result;
			}
			if (parameters.distanceToAffectorCenterSqr < arrivalRadiusSqr)
			{
				float num = 1f - parameters.distanceToAffectorCenterSqr / arrivalRadiusSqr;
				return global::UnityEngine.Vector3.Normalize(parameters.scaledDirectionToAffectorCenter) * num;
			}
			return global::UnityEngine.Vector3.Normalize(parameters.scaledDirectionToAffectorCenter);
		}

		protected override void OnDrawGizmosSelected()
		{
			if (base.enabled)
			{
				base.OnDrawGizmosSelected();
				float x = base.transform.lossyScale.x;
				float num = arrivalRadius * x;
				float num2 = arrivedRadius * x;
				global::UnityEngine.Vector3 center = base.transform.position + offset;
				global::UnityEngine.Gizmos.color = global::UnityEngine.Color.yellow;
				global::UnityEngine.Gizmos.DrawWireSphere(center, num);
				global::UnityEngine.Gizmos.color = global::UnityEngine.Color.red;
				global::UnityEngine.Gizmos.DrawWireSphere(center, num2);
			}
		}
	}
}
