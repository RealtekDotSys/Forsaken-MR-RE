public interface IWorkshopSlotButton
{
	global::UnityEngine.GameObject gameObject { get; }

	WorkshopSlotData GetWorkshopSlotData();

	void SetSelectedUI(bool value);

	int GetSortWeight();

	bool IsReadyToCollect();

	void OverrideSlotDataState(WorkshopEntry.Status status);

	void TearDown();
}
