namespace DigitalRuby.ThunderAndLightning
{
	public class LightningCustomTransformStateInfo
	{
		public global::UnityEngine.Vector3 BoltStartPosition;

		public global::UnityEngine.Vector3 BoltEndPosition;

		public global::UnityEngine.Transform Transform;

		public global::UnityEngine.Transform StartTransform;

		public global::UnityEngine.Transform EndTransform;

		public object UserInfo;

		private static readonly global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo> cache = new global::System.Collections.Generic.List<global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo>();

		public global::DigitalRuby.ThunderAndLightning.LightningCustomTransformState State { get; set; }

		public global::DigitalRuby.ThunderAndLightning.LightningBoltParameters Parameters { get; set; }

		public static global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo GetOrCreateStateInfo()
		{
			if (cache.Count == 0)
			{
				return new global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo();
			}
			int index = cache.Count - 1;
			global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo result = cache[index];
			cache.RemoveAt(index);
			return result;
		}

		public static void ReturnStateInfoToCache(global::DigitalRuby.ThunderAndLightning.LightningCustomTransformStateInfo info)
		{
			if (info != null)
			{
				info.Transform = (info.StartTransform = (info.EndTransform = null));
				info.UserInfo = null;
				cache.Add(info);
			}
		}
	}
}
