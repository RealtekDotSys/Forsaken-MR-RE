namespace DigitalRuby.ThunderAndLightning
{
	public class LightningWhipSpell : global::DigitalRuby.ThunderAndLightning.LightningSpellScript
	{
		[global::UnityEngine.Header("Whip")]
		[global::UnityEngine.Tooltip("Attach the whip to what object")]
		public global::UnityEngine.GameObject AttachTo;

		[global::UnityEngine.Tooltip("Rotate the whip with this object")]
		public global::UnityEngine.GameObject RotateWith;

		[global::UnityEngine.Tooltip("Whip handle")]
		public global::UnityEngine.GameObject WhipHandle;

		[global::UnityEngine.Tooltip("Whip start")]
		public global::UnityEngine.GameObject WhipStart;

		[global::UnityEngine.Tooltip("Whip spring")]
		public global::UnityEngine.GameObject WhipSpring;

		[global::UnityEngine.Tooltip("Whip crack audio source")]
		public global::UnityEngine.AudioSource WhipCrackAudioSource;

		[global::UnityEngine.HideInInspector]
		public global::System.Action<global::UnityEngine.Vector3> CollisionCallback;

		private global::System.Collections.IEnumerator WhipForward()
		{
			for (int i = 0; i < WhipStart.transform.childCount; i++)
			{
				global::UnityEngine.Rigidbody component = WhipStart.transform.GetChild(i).gameObject.GetComponent<global::UnityEngine.Rigidbody>();
				if (component != null)
				{
					component.drag = 0f;
					component.velocity = global::UnityEngine.Vector3.zero;
					component.angularVelocity = global::UnityEngine.Vector3.zero;
				}
			}
			WhipSpring.SetActive(value: true);
			global::UnityEngine.Vector3 position = WhipStart.GetComponent<global::UnityEngine.Rigidbody>().position;
			global::UnityEngine.Vector3 whipPositionForwards;
			global::UnityEngine.Vector3 position2;
			if (global::UnityEngine.Physics.Raycast(position, Direction, out var hitInfo, MaxDistance, CollisionMask))
			{
				global::UnityEngine.Vector3 normalized = (hitInfo.point - position).normalized;
				whipPositionForwards = position + normalized * MaxDistance;
				position2 = position - normalized * 25f;
			}
			else
			{
				whipPositionForwards = position + Direction * MaxDistance;
				position2 = position - Direction * 25f;
			}
			WhipSpring.GetComponent<global::UnityEngine.Rigidbody>().position = position2;
			yield return new global::DigitalRuby.ThunderAndLightning.WaitForSecondsLightning(0.25f);
			WhipSpring.GetComponent<global::UnityEngine.Rigidbody>().position = whipPositionForwards;
			yield return new global::DigitalRuby.ThunderAndLightning.WaitForSecondsLightning(0.1f);
			if (WhipCrackAudioSource != null)
			{
				WhipCrackAudioSource.Play();
			}
			yield return new global::DigitalRuby.ThunderAndLightning.WaitForSecondsLightning(0.1f);
			if (CollisionParticleSystem != null)
			{
				CollisionParticleSystem.Play();
			}
			ApplyCollisionForce(SpellEnd.transform.position);
			WhipSpring.SetActive(value: false);
			if (CollisionCallback != null)
			{
				CollisionCallback(SpellEnd.transform.position);
			}
			yield return new global::DigitalRuby.ThunderAndLightning.WaitForSecondsLightning(0.1f);
			for (int j = 0; j < WhipStart.transform.childCount; j++)
			{
				global::UnityEngine.Rigidbody component2 = WhipStart.transform.GetChild(j).gameObject.GetComponent<global::UnityEngine.Rigidbody>();
				if (component2 != null)
				{
					component2.velocity = global::UnityEngine.Vector3.zero;
					component2.angularVelocity = global::UnityEngine.Vector3.zero;
					component2.drag = 0.5f;
				}
			}
		}

		protected override void Start()
		{
			base.Start();
			WhipSpring.SetActive(value: false);
			WhipHandle.SetActive(value: false);
		}

		protected override void Update()
		{
			base.Update();
			base.gameObject.transform.position = AttachTo.transform.position;
			base.gameObject.transform.rotation = RotateWith.transform.rotation;
		}

		protected override void OnCastSpell()
		{
			StartCoroutine(WhipForward());
		}

		protected override void OnStopSpell()
		{
		}

		protected override void OnActivated()
		{
			base.OnActivated();
			WhipHandle.SetActive(value: true);
		}

		protected override void OnDeactivated()
		{
			base.OnDeactivated();
			WhipHandle.SetActive(value: false);
		}
	}
}
