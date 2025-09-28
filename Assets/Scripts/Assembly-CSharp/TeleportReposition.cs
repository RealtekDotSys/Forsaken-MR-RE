public static class TeleportReposition
{
	public static void NormalTeleport(Blackboard blackboard)
	{
		global::UnityEngine.Debug.LogWarning("Doing a normal teleport!");
		if (blackboard == null)
		{
			global::UnityEngine.Debug.LogWarning("normal teleport blackboard null");
		}
		if (blackboard.Model == null)
		{
			global::UnityEngine.Debug.LogWarning("normal teleport model null");
		}
		blackboard.Model.Teleport(global::UnityEngine.Random.Range(blackboard.AttackProfile.TeleportReposition.AngleFromCamera.Min, blackboard.AttackProfile.TeleportReposition.AngleFromCamera.Max), global::UnityEngine.Random.Range(blackboard.AttackProfile.TeleportReposition.DistanceFromCamera.Min, blackboard.AttackProfile.TeleportReposition.DistanceFromCamera.Max), faceCamera: false);
		blackboard.ShouldCheckForDirectionChange = false;
	}

	public static void PhantomTeleport(Blackboard blackboard, float distance)
	{
		blackboard.Model.Teleport(global::UnityEngine.Random.Range(blackboard.AttackProfile.TeleportReposition.AngleFromCamera.Min, blackboard.AttackProfile.TeleportReposition.AngleFromCamera.Max), distance, faceCamera: false);
	}
}
