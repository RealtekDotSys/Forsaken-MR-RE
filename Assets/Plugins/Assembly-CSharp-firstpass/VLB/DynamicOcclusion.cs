namespace VLB
{
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.RequireComponent(typeof(global::VLB.VolumetricLightBeam))]
	[global::UnityEngine.HelpURL("http://saladgamer.com/vlb-doc/comp-dynocclusion/")]
	public class DynamicOcclusion : global::UnityEngine.MonoBehaviour
	{
		public class HitResult
		{
			public global::UnityEngine.Vector3 point;

			public global::UnityEngine.Vector3 normal;

			public float distance;

			private global::UnityEngine.Collider2D collider2D;

			private global::UnityEngine.Collider collider3D;

			public bool hasCollider
			{
				get
				{
					if (!collider2D)
					{
						return collider3D;
					}
					return true;
				}
			}

			public HitResult(global::UnityEngine.RaycastHit hit3D)
			{
				point = hit3D.point;
				normal = hit3D.normal;
				distance = hit3D.distance;
				collider3D = hit3D.collider;
				collider2D = null;
			}

			public HitResult(global::UnityEngine.RaycastHit2D hit2D)
			{
				point = hit2D.point;
				normal = hit2D.normal;
				distance = hit2D.distance;
				collider2D = hit2D.collider;
				collider3D = null;
			}

			public HitResult()
			{
				point = global::UnityEngine.Vector3.zero;
				normal = global::UnityEngine.Vector3.zero;
				distance = 0f;
				collider2D = null;
				collider3D = null;
			}
		}

		private enum Direction
		{
			Up = 0,
			Right = 1,
			Down = 2,
			Left = 3
		}

		public global::VLB.OccluderDimensions dimensions;

		public global::UnityEngine.LayerMask layerMask = global::VLB.Consts.DynOcclusionLayerMaskDefault;

		public bool considerTriggers;

		public float minOccluderArea;

		public int waitFrameCount = 3;

		public float minSurfaceRatio = 0.5f;

		public float maxSurfaceDot = 0.25f;

		public global::VLB.PlaneAlignment planeAlignment;

		public float planeOffset = 0.1f;

		private global::VLB.VolumetricLightBeam m_Master;

		private int m_FrameCountToWait;

		private float m_RangeMultiplier = 1f;

		private uint m_PrevNonSubHitDirectionId;

		private global::UnityEngine.QueryTriggerInteraction queryTriggerInteraction
		{
			get
			{
				if (!considerTriggers)
				{
					return global::UnityEngine.QueryTriggerInteraction.Ignore;
				}
				return global::UnityEngine.QueryTriggerInteraction.Collide;
			}
		}

		private float raycastMaxDistance => m_Master.fallOffEnd * m_RangeMultiplier * base.transform.lossyScale.z;

		private void OnValidate()
		{
			minOccluderArea = global::UnityEngine.Mathf.Max(minOccluderArea, 0f);
			waitFrameCount = global::UnityEngine.Mathf.Clamp(waitFrameCount, 1, 60);
		}

		private void OnEnable()
		{
			m_Master = GetComponent<global::VLB.VolumetricLightBeam>();
		}

		private void OnDisable()
		{
			SetHitNull();
		}

		private void Start()
		{
			if (global::UnityEngine.Application.isPlaying)
			{
				global::VLB.TriggerZone component = GetComponent<global::VLB.TriggerZone>();
				if ((bool)component)
				{
					m_RangeMultiplier = global::UnityEngine.Mathf.Max(1f, component.rangeMultiplier);
				}
			}
		}

		private void LateUpdate()
		{
			if (m_FrameCountToWait <= 0)
			{
				ProcessRaycasts();
				m_FrameCountToWait = waitFrameCount;
			}
			m_FrameCountToWait--;
		}

		private global::UnityEngine.Vector3 GetRandomVectorAround(global::UnityEngine.Vector3 direction, float angleDiff)
		{
			float num = angleDiff * 0.5f;
			return global::UnityEngine.Quaternion.Euler(global::UnityEngine.Random.Range(0f - num, num), global::UnityEngine.Random.Range(0f - num, num), global::UnityEngine.Random.Range(0f - num, num)) * direction;
		}

		private global::VLB.DynamicOcclusion.HitResult GetBestHit(global::UnityEngine.Vector3 rayPos, global::UnityEngine.Vector3 rayDir)
		{
			if (dimensions != global::VLB.OccluderDimensions.Occluders2D)
			{
				return GetBestHit3D(rayPos, rayDir);
			}
			return GetBestHit2D(rayPos, rayDir);
		}

		private global::VLB.DynamicOcclusion.HitResult GetBestHit3D(global::UnityEngine.Vector3 rayPos, global::UnityEngine.Vector3 rayDir)
		{
			global::UnityEngine.RaycastHit[] array = global::UnityEngine.Physics.RaycastAll(rayPos, rayDir, raycastMaxDistance, layerMask.value, queryTriggerInteraction);
			int num = -1;
			float num2 = float.MaxValue;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].collider.bounds.GetMaxArea2D() >= minOccluderArea && array[i].distance < num2)
				{
					num2 = array[i].distance;
					num = i;
				}
			}
			if (num != -1)
			{
				return new global::VLB.DynamicOcclusion.HitResult(array[num]);
			}
			return new global::VLB.DynamicOcclusion.HitResult();
		}

		private global::VLB.DynamicOcclusion.HitResult GetBestHit2D(global::UnityEngine.Vector3 rayPos, global::UnityEngine.Vector3 rayDir)
		{
			global::UnityEngine.RaycastHit2D[] array = global::UnityEngine.Physics2D.RaycastAll(new global::UnityEngine.Vector2(rayPos.x, rayPos.y), new global::UnityEngine.Vector2(rayDir.x, rayDir.y), raycastMaxDistance, layerMask.value);
			int num = -1;
			float num2 = float.MaxValue;
			for (int i = 0; i < array.Length; i++)
			{
				if ((considerTriggers || !array[i].collider.isTrigger) && array[i].collider.bounds.GetMaxArea2D() >= minOccluderArea && array[i].distance < num2)
				{
					num2 = array[i].distance;
					num = i;
				}
			}
			if (num != -1)
			{
				return new global::VLB.DynamicOcclusion.HitResult(array[num]);
			}
			return new global::VLB.DynamicOcclusion.HitResult();
		}

		private global::UnityEngine.Vector3 GetDirection(uint dirInt)
		{
			dirInt %= (uint)global::System.Enum.GetValues(typeof(global::VLB.DynamicOcclusion.Direction)).Length;
			return dirInt switch
			{
				0u => base.transform.up, 
				1u => base.transform.right, 
				2u => -base.transform.up, 
				3u => -base.transform.right, 
				_ => global::UnityEngine.Vector3.zero, 
			};
		}

		private bool IsHitValid(global::VLB.DynamicOcclusion.HitResult hit)
		{
			if (hit.hasCollider)
			{
				return global::UnityEngine.Vector3.Dot(hit.normal, -base.transform.forward) >= maxSurfaceDot;
			}
			return false;
		}

		private void ProcessRaycasts()
		{
			global::VLB.DynamicOcclusion.HitResult hitResult = GetBestHit(base.transform.position, base.transform.forward);
			if (IsHitValid(hitResult))
			{
				if (minSurfaceRatio > 0.5f)
				{
					for (uint num = 0u; num < (uint)global::System.Enum.GetValues(typeof(global::VLB.DynamicOcclusion.Direction)).Length; num++)
					{
						global::UnityEngine.Vector3 direction = GetDirection(num + m_PrevNonSubHitDirectionId);
						global::UnityEngine.Vector3 vector = base.transform.position + direction * m_Master.coneRadiusStart * (minSurfaceRatio * 2f - 1f);
						global::UnityEngine.Vector3 vector2 = base.transform.position + base.transform.forward * m_Master.fallOffEnd + direction * m_Master.coneRadiusEnd * (minSurfaceRatio * 2f - 1f);
						global::VLB.DynamicOcclusion.HitResult bestHit = GetBestHit(vector, vector2 - vector);
						if (IsHitValid(bestHit))
						{
							if (bestHit.distance > hitResult.distance)
							{
								hitResult = bestHit;
							}
							continue;
						}
						m_PrevNonSubHitDirectionId = num;
						SetHitNull();
						return;
					}
				}
				SetHit(hitResult);
			}
			else
			{
				SetHitNull();
			}
		}

		private void SetHit(global::VLB.DynamicOcclusion.HitResult hit)
		{
			global::VLB.PlaneAlignment planeAlignment = this.planeAlignment;
			if (planeAlignment != global::VLB.PlaneAlignment.Surface && planeAlignment == global::VLB.PlaneAlignment.Beam)
			{
				SetClippingPlane(new global::UnityEngine.Plane(-base.transform.forward, hit.point));
			}
			else
			{
				SetClippingPlane(new global::UnityEngine.Plane(hit.normal, hit.point));
			}
		}

		private void SetHitNull()
		{
			SetClippingPlaneOff();
		}

		private void SetClippingPlane(global::UnityEngine.Plane planeWS)
		{
			planeWS = planeWS.TranslateCustom(planeWS.normal * planeOffset);
			m_Master.SetClippingPlane(planeWS);
		}

		private void SetClippingPlaneOff()
		{
			m_Master.SetClippingPlaneOff();
		}
	}
}
