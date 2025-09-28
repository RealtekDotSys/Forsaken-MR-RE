public class Discord_Controller : global::UnityEngine.MonoBehaviour
{
	public enum DiscordActivities
	{
		Encounter = 1,
		Workshop = 2
	}

	private long time;

	public static Discord_Controller Instance;

	public global::DiscordRPC.DiscordRpcClient discord;

	private Discord_Controller.DiscordActivities activeActivity;

	private MapEntity activeEntity;

	private MasterDomain domain;

	private StreakData streakData = new StreakData
	{
		wins = 0,
		currentStreak = 0
	};

	public string DiscordUserId { get; private set; }

	public string DiscordAvatarId { get; private set; }

	public string DiscordUsername { get; private set; }

	private void Awake()
	{
		activeActivity = Discord_Controller.DiscordActivities.Workshop;
		Instance = this;
		discord = new global::DiscordRPC.DiscordRpcClient("1128211537673338910");
		discord.OnReady += delegate(object sender, global::DiscordRPC.Message.ReadyMessage e)
		{
			global::UnityEngine.Debug.Log($"Received Ready from user {e.User.Username}");
		};
		discord.OnPresenceUpdate += delegate(object sender, global::DiscordRPC.Message.PresenceMessage e)
		{
			global::UnityEngine.Debug.Log($"Received Ready from user {e.Presence}");
		};
		discord.Initialize();
		discord.RegisterUriScheme();
		time = global::System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
	}

	public void ClearActivity()
	{
	}

	private void OnDisable()
	{
	}

	private void GameDisplayChanged(GameDisplayData data)
	{
		global::UnityEngine.Debug.LogWarning("encounter rich presence: game display change");
		if (data.currentDisplay == GameDisplayData.DisplayType.map)
		{
			global::UnityEngine.Debug.LogWarning("encounter rich presence: in lobby");
			activeActivity = Discord_Controller.DiscordActivities.Workshop;
			UpdateStatus();
		}
	}

	private void EncounterStarted(MapEntity entity)
	{
		global::UnityEngine.Debug.LogWarning("encounter rich presence: " + entity.CPUId);
		activeActivity = Discord_Controller.DiscordActivities.Encounter;
		activeEntity = entity;
		UpdateStatus();
	}

	private void StreakDataUpdated(StreakData data)
	{
		streakData = data;
	}

	private void Update()
	{
		if (discord == null)
		{
			return;
		}
		SendCallback();
		if (domain == null && MasterDomain.GetDomain() != null)
		{
			MasterDomain.GetDomain().eventExposer.add_GameDisplayChange(GameDisplayChanged);
			MasterDomain.GetDomain().eventExposer.add_AnimatronicEncounterStarted(EncounterStarted);
			MasterDomain.GetDomain().eventExposer.add_StreakDataUpdated(StreakDataUpdated);
			domain = MasterDomain.GetDomain();
		}
		if (!string.IsNullOrEmpty(DiscordUserId) && !string.IsNullOrEmpty(DiscordAvatarId) && !string.IsNullOrEmpty(DiscordUsername))
		{
			return;
		}
		try
		{
			DiscordUserId = discord.CurrentUser.ID.ToString();
			DiscordAvatarId = discord.CurrentUser.Avatar;
			DiscordUsername = discord.CurrentUser.Username;
		}
		catch
		{
		}
	}

	private void SendCallback()
	{
	}

	private void UpdateStatus()
	{
		switch (activeActivity)
		{
		case Discord_Controller.DiscordActivities.Workshop:
			WorkshopActivity();
			break;
		case Discord_Controller.DiscordActivities.Encounter:
			EncounterActivity();
			break;
		}
	}

	private void WorkshopActivity()
	{
		discord.SetPresence(new global::DiscordRPC.RichPresence
		{
			Details = "In Lobby",
			State = "Encounters Won: " + streakData.wins + " - Streak: " + streakData.currentStreak + " || Friend Code: " + MasterDomain.GetDomain().GameUIDomain.GameUIData.friendCodeModel.MyCurrentFriendCode,
			Assets = new global::DiscordRPC.Assets
			{
				LargeImageKey = "workshop"
			},
			Timestamps = new global::DiscordRPC.Timestamps
			{
				StartUnixMilliseconds = (ulong)time
			},
			Buttons = new global::DiscordRPC.Button[1]
			{
				new global::DiscordRPC.Button
				{
					Label = "Check This Game Out",
					Url = "https://gamejolt.com/games/Ruin2/846902"
				}
			}
		});
	}

	private void EncounterActivity()
	{
		discord.SetPresence(new global::DiscordRPC.RichPresence
		{
			Details = "In Encounter",
			State = "PlushSuit: " + activeEntity.PlushSuitId + ", CPU: " + activeEntity.CPUId + " || Friend Code: " + MasterDomain.GetDomain().GameUIDomain.GameUIData.friendCodeModel.MyCurrentFriendCode,
			Assets = new global::DiscordRPC.Assets
			{
				LargeImageKey = "encounter"
			},
			Timestamps = new global::DiscordRPC.Timestamps
			{
				StartUnixMilliseconds = (ulong)time
			},
			Buttons = new global::DiscordRPC.Button[1]
			{
				new global::DiscordRPC.Button
				{
					Label = "Check This Game Out",
					Url = "https://gamejolt.com/games/Ruin2/846902"
				}
			}
		});
	}
}
