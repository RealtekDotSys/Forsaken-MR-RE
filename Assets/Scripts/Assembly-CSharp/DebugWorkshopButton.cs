public class DebugWorkshopButton : global::UnityEngine.MonoBehaviour
{
	public void OpenCPU()
	{
		MasterDomain.GetDomain().eventExposer.OnWorkshopModifyTabOpened(SlotDisplayButtonType.Cpu);
	}

	public void CloseCPU()
	{
		MasterDomain.GetDomain().eventExposer.OnWorkshopModifyTabClosed(SlotDisplayButtonType.Cpu);
	}

	public void SwapCPU()
	{
		MasterDomain.GetDomain().eventExposer.OnWorkshopCpuChanged();
	}
}
