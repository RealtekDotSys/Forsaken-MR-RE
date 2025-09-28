namespace DigitalRuby.ThunderAndLightning
{
	public class LightningBeamSpellScript : global::DigitalRuby.ThunderAndLightning.LightningSpellScript
	{
		[global::UnityEngine.Header("Beam")]
		[global::UnityEngine.Tooltip("The lightning path script creating the beam of lightning")]
		public global::DigitalRuby.ThunderAndLightning.LightningBoltPathScriptBase LightningPathScript;

		[global::UnityEngine.Tooltip("Give the end point some randomization")]
		public float EndPointRandomization = 1.5f;

		[global::UnityEngine.HideInInspector]
		public global::System.Action<global::UnityEngine.RaycastHit> CollisionCallback;

		private void CheckCollision()
		{
			if (global::UnityEngine.Physics.Raycast(SpellStart.transform.position, Direction, out var hitInfo, MaxDistance, CollisionMask))
			{
				SpellEnd.transform.position = hitInfo.point;
				SpellEnd.transform.position += global::UnityEngine.Random.insideUnitSphere * EndPointRandomization;
				PlayCollisionSound(SpellEnd.transform.position);
				if (CollisionParticleSystem != null)
				{
					CollisionParticleSystem.transform.position = hitInfo.point;
					CollisionParticleSystem.Play();
				}
				ApplyCollisionForce(hitInfo.point);
				if (CollisionCallback != null)
				{
					CollisionCallback(hitInfo);
				}
			}
			else
			{
				if (CollisionParticleSystem != null)
				{
					CollisionParticleSystem.Stop();
				}
				SpellEnd.transform.position = SpellStart.transform.position + Direction * MaxDistance;
				SpellEnd.transform.position += global::UnityEngine.Random.insideUnitSphere * EndPointRandomization;
			}
		}

		protected override void Start()
		{
			base.Start();
			LightningPathScript.ManualMode = true;
		}

		protected override void LateUpdate()
		{
			base.LateUpdate();
			if (base.Casting)
			{
				CheckCollision();
			}
		}

		protected override void OnCastSpell()
		{
			LightningPathScript.ManualMode = false;
		}

		protected override void OnStopSpell()
		{
			LightningPathScript.ManualMode = true;
		}
	}
}
