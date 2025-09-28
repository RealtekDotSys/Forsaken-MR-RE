namespace DigitalRuby.ThunderAndLightning
{
	public class LightningBoltTransformTrackerScript : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.Tooltip("The lightning script to track.")]
		public global::DigitalRuby.ThunderAndLightning.LightningBoltPrefabScript LightningScript;

		[global::UnityEngine.Tooltip("The transform to track which will be where the bolts are emitted from.")]
		public global::UnityEngine.Transform StartTarget;

		[global::UnityEngine.Tooltip("(Optional) The transform to track which will be where the bolts are emitted to. If no end target is specified, lightning will simply move to stay on top of the start target.")]
		public global::UnityEngine.Transform EndTarget;

		[global::DigitalRuby.ThunderAndLightning.SingleLine("Scaling limits.")]
		public global::DigitalRuby.ThunderAndLightning.RangeOfFloats ScaleLimit = new global::DigitalRuby.ThunderAndLightning.RangeOfFloats
		{
			Minimum = 0.1f,
			Maximum = 10f
		};

		private readonly global::System.Collections.Generic.Dictionary<global::UnityEngine.Transform, global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo> transformStartPositions = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Transform, global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo>();

		private void Start()
		{
			if (LightningScript != null)
			{
				LightningScript.CustomTransformHandler.RemoveAllListeners();
				LightningScript.CustomTransformHandler.AddListener(CustomTransformHandler);
			}
		}

		private static float AngleBetweenVector2(global::UnityEngine.Vector2 vec1, global::UnityEngine.Vector2 vec2)
		{
			global::UnityEngine.Vector2 normalized = (vec2 - vec1).normalized;
			return global::UnityEngine.Vector2.Angle(global::UnityEngine.Vector2.right, normalized) * global::UnityEngine.Mathf.Sign(vec2.y - vec1.y);
		}

		private static void UpdateTransform(global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo state, global::DigitalRuby.ThunderAndLightning.LightningBoltPrefabScript script, global::DigitalRuby.ThunderAndLightning.RangeOfFloats scaleLimit)
		{
			if (state.Transform == null || state.StartTransform == null)
			{
				return;
			}
			if (state.EndTransform == null)
			{
				state.Transform.position = state.StartTransform.position - state.BoltStartPosition;
				return;
			}
			global::UnityEngine.Quaternion quaternion;
			if ((script.CameraMode == global::DigitalRuby.ThunderAndLightning.CameraMode.Auto && script.Camera.orthographic) || script.CameraMode == global::DigitalRuby.ThunderAndLightning.CameraMode.OrthographicXY)
			{
				float num = AngleBetweenVector2(state.BoltStartPosition, state.BoltEndPosition);
				quaternion = global::UnityEngine.Quaternion.AngleAxis(AngleBetweenVector2(state.StartTransform.position, state.EndTransform.position) - num, global::UnityEngine.Vector3.forward);
			}
			if (script.CameraMode == global::DigitalRuby.ThunderAndLightning.CameraMode.OrthographicXZ)
			{
				float num2 = AngleBetweenVector2(new global::UnityEngine.Vector2(state.BoltStartPosition.x, state.BoltStartPosition.z), new global::UnityEngine.Vector2(state.BoltEndPosition.x, state.BoltEndPosition.z));
				quaternion = global::UnityEngine.Quaternion.AngleAxis(AngleBetweenVector2(new global::UnityEngine.Vector2(state.StartTransform.position.x, state.StartTransform.position.z), new global::UnityEngine.Vector2(state.EndTransform.position.x, state.EndTransform.position.z)) - num2, global::UnityEngine.Vector3.up);
			}
			else
			{
				global::UnityEngine.Quaternion rotation = global::UnityEngine.Quaternion.LookRotation((state.BoltEndPosition - state.BoltStartPosition).normalized);
				quaternion = global::UnityEngine.Quaternion.LookRotation((state.EndTransform.position - state.StartTransform.position).normalized) * global::UnityEngine.Quaternion.Inverse(rotation);
			}
			state.Transform.rotation = quaternion;
			float num3 = global::UnityEngine.Vector3.Distance(state.BoltStartPosition, state.BoltEndPosition);
			float num4 = global::UnityEngine.Vector3.Distance(state.EndTransform.position, state.StartTransform.position);
			float num5 = global::UnityEngine.Mathf.Clamp((num3 < global::UnityEngine.Mathf.Epsilon) ? 1f : (num4 / num3), scaleLimit.Minimum, scaleLimit.Maximum);
			state.Transform.localScale = new global::UnityEngine.Vector3(num5, num5, num5);
			global::UnityEngine.Vector3 vector = quaternion * (num5 * state.BoltStartPosition);
			state.Transform.position = state.StartTransform.position - vector;
		}

		public void CustomTransformHandler(global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo state)
		{
			if (base.enabled)
			{
				if (LightningScript == null)
				{
					global::UnityEngine.Debug.LogError("LightningScript property must be set to non-null.");
				}
				else if (state.State == global::DigitalRuby.ThunderAndLightning.LightningCustomTransformState.Executing)
				{
					UpdateTransform(state, LightningScript, ScaleLimit);
				}
				else if (state.State == global::DigitalRuby.ThunderAndLightning.LightningCustomTransformState.Started)
				{
					state.StartTransform = StartTarget;
					state.EndTransform = EndTarget;
					transformStartPositions[base.transform] = state;
				}
				else
				{
					transformStartPositions.Remove(base.transform);
				}
			}
		}
	}
}
