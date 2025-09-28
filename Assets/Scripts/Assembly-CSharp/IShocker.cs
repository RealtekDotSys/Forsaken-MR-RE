public interface IShocker
{
	void SetShockerData(float cooldownSeconds, bool isContinuous);

	float GetTotalMissCooldownTime();

	float GetTotalHitCooldownTime();

	float GetShockerAttackPower();

	ShockerStatus GetStatus();

	float GetRemainingCooldownTime();

	float GetCooldownPercent();

	void Activate(bool didHitAnimatronic, bool isDisruptionFullyActive);

	void Deactivate();

	void SetShockerOffset(global::UnityEngine.Vector3 startPosition);

	void EnableShocker();

	void DisableShocker();
}
