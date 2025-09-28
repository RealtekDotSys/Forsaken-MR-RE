namespace SRF.Service
{
	public abstract class SRServiceBase<T> : global::SRF.SRMonoBehaviourEx where T : class
	{
		protected override void Awake()
		{
			base.Awake();
			global::SRF.Service.SRServiceManager.RegisterService<T>(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			global::SRF.Service.SRServiceManager.UnRegisterService<T>();
		}
	}
}
