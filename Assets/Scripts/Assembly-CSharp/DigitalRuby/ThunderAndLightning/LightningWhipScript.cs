namespace DigitalRuby.ThunderAndLightning
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.AudioSource))]
	public class LightningWhipScript : global::UnityEngine.MonoBehaviour
	{
		public global::UnityEngine.AudioClip WhipCrack;

		public global::UnityEngine.AudioClip WhipCrackThunder;

		private global::UnityEngine.AudioSource audioSource;

		private global::UnityEngine.GameObject whipStart;

		private global::UnityEngine.GameObject whipEndStrike;

		private global::UnityEngine.GameObject whipHandle;

		private global::UnityEngine.GameObject whipSpring;

		private global::UnityEngine.Vector2 prevDrag;

		private bool dragging;

		private bool canWhip = true;

		private global::System.Collections.IEnumerator WhipForward()
		{
			if (!canWhip)
			{
				yield break;
			}
			canWhip = false;
			for (int i = 0; i < whipStart.transform.childCount; i++)
			{
				global::UnityEngine.Rigidbody2D component = whipStart.transform.GetChild(i).gameObject.GetComponent<global::UnityEngine.Rigidbody2D>();
				if (component != null)
				{
					component.drag = 0f;
				}
			}
			audioSource.PlayOneShot(WhipCrack);
			whipSpring.GetComponent<global::UnityEngine.SpringJoint2D>().enabled = true;
			whipSpring.GetComponent<global::UnityEngine.Rigidbody2D>().position = whipHandle.GetComponent<global::UnityEngine.Rigidbody2D>().position + new global::UnityEngine.Vector2(-15f, 5f);
			yield return new global::DigitalRuby.ThunderAndLightning.WaitForSecondsLightning(0.2f);
			whipSpring.GetComponent<global::UnityEngine.Rigidbody2D>().position = whipHandle.GetComponent<global::UnityEngine.Rigidbody2D>().position + new global::UnityEngine.Vector2(15f, 2.5f);
			yield return new global::DigitalRuby.ThunderAndLightning.WaitForSecondsLightning(0.15f);
			audioSource.PlayOneShot(WhipCrackThunder, 0.5f);
			yield return new global::DigitalRuby.ThunderAndLightning.WaitForSecondsLightning(0.15f);
			whipEndStrike.GetComponent<global::UnityEngine.ParticleSystem>().Play();
			whipSpring.GetComponent<global::UnityEngine.SpringJoint2D>().enabled = false;
			yield return new global::DigitalRuby.ThunderAndLightning.WaitForSecondsLightning(0.65f);
			for (int j = 0; j < whipStart.transform.childCount; j++)
			{
				global::UnityEngine.Rigidbody2D component2 = whipStart.transform.GetChild(j).gameObject.GetComponent<global::UnityEngine.Rigidbody2D>();
				if (component2 != null)
				{
					component2.velocity = global::UnityEngine.Vector2.zero;
					component2.drag = 0.5f;
				}
			}
			canWhip = true;
		}

		private void Start()
		{
			whipStart = global::UnityEngine.GameObject.Find("WhipStart");
			whipEndStrike = global::UnityEngine.GameObject.Find("WhipEndStrike");
			whipHandle = global::UnityEngine.GameObject.Find("WhipHandle");
			whipSpring = global::UnityEngine.GameObject.Find("WhipSpring");
			audioSource = GetComponent<global::UnityEngine.AudioSource>();
		}

		private void Update()
		{
			if (!dragging && global::UnityEngine.Input.GetMouseButtonDown(0))
			{
				global::UnityEngine.Vector2 point = global::UnityEngine.Camera.main.ScreenToWorldPoint(global::UnityEngine.Input.mousePosition);
				global::UnityEngine.Collider2D collider2D = global::UnityEngine.Physics2D.OverlapPoint(point);
				if (collider2D != null && collider2D.gameObject == whipHandle)
				{
					dragging = true;
					prevDrag = point;
				}
			}
			else if (dragging && global::UnityEngine.Input.GetMouseButton(0))
			{
				global::UnityEngine.Vector2 vector = global::UnityEngine.Camera.main.ScreenToWorldPoint(global::UnityEngine.Input.mousePosition);
				global::UnityEngine.Vector2 vector2 = vector - prevDrag;
				global::UnityEngine.Rigidbody2D component = whipHandle.GetComponent<global::UnityEngine.Rigidbody2D>();
				component.MovePosition(component.position + vector2);
				prevDrag = vector;
			}
			else
			{
				dragging = false;
			}
			if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Space))
			{
				StartCoroutine(WhipForward());
			}
		}
	}
}
