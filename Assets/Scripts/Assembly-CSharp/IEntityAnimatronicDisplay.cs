public interface IEntityAnimatronicDisplay
{
	MapEntity Entity { get; }

	CONFIG_DATA.MapEntities EntityConfigData { get; }

	global::TMPro.TextMeshProUGUI EncounterButtonText { get; }

	global::UnityEngine.UI.Button JammerButton { get; }

	global::TMPro.TextMeshProUGUI JammerButtonText { get; }

	global::UnityEngine.UI.Button CloseButton { get; }

	global::UnityEngine.MonoBehaviour CoroutineRunner { get; }

	void Setup(EntityDisplayData data, IAnimatronicDisplayController controller);

	void Teardown();

	void ForceClose();

	void Encounter();

	void Jam();

	void Flee();

	void ShowAlert(string locKey, EntityAnimatronicAlertStyle alertStyle);

	void SetButtonVisibility(bool canJam, bool canEncounter, bool canClose);

	global::System.Collections.IEnumerator AnimateStatBarFill();

	float CalculateDrawerHeight();

	void ActivateLoadout(bool activate);
}
