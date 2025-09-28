public class EventExposer
{
	private global::System.Action<FastForwardCompleteArgs> EntityFastForwardComplete;

	private global::System.Action<Container.EntityAddedRemovedArgs> EntityAddedEvent;

	private global::System.Action<Container.EntityAddedRemovedArgs> EntityRemovedEvent;

	private global::System.Action<Container.EntitiesClearedArgs> EntitiesClearedEvent;

	private global::System.Action<global::System.Collections.Generic.IEnumerable<MapEntitySynchronizeableState>> MapEntitiesReceivedFromServer;

	private global::System.Action<global::System.Collections.Generic.IEnumerable<MapEntity>> MapEntitiesModelsUpdated;

	private global::System.Action NewAnimatronicSpawned;

	private global::System.Action<global::System.Collections.Generic.Dictionary<string, int>> PlayerCurrencyRefreshed;

	private global::System.Action<RewardData> RewardsReceived;

	private global::System.Action<PlayerInventory> InventoryUpdated;

	private global::System.Action<bool> PurchaseRequestAudioInvoked;

	private global::System.Action<StreakData> StreakDataUpdated;

	private global::System.Action<global::System.Collections.Generic.Dictionary<string, int>> PlayerGoodsUpdated;

	private global::System.Action<global::System.Collections.Generic.List<global::UnityEngine.Vector2>, global::System.Collections.Generic.List<global::UnityEngine.Vector2>> GestureTouchEvent;

	private global::System.Action GestureResetEvent;

	private global::System.Action<global::System.Collections.Generic.List<StoreContainer.StorefrontData>> VirtualGoodsDataReceived;

	private global::System.Action<ModInventory> ModInventoryUpdated;

	private global::System.Action<WorkshopData> WorkshopDataV2Updated;

	private global::System.Action<CPUInventory> CPUInventoryUpdated;

	private global::System.Action<GenericDialogData> GenericDialogRequestReceived;

	private global::System.Action<StoreDisplayData> StoreDialogRequestReceived;

	private global::System.Action<LootRewardDisplayData> LootRewardProcessed;

	private global::System.Action<LootRewardDisplayData> LootRewardDisplayRequestReceived;

	private global::System.Action<ExitAttackSequenceDialogData> ExitAttackSequenceReceived;

	private global::System.Action<EntityDisplayData> EntityWanderingAnimatronicDisplayRequestReceived;

	private global::System.Action<EntityDisplayData> EntitySpecialDeliveryDisplayRequestReceived;

	private global::System.Action<MapEntity> AnimatronicEncounterStarted;

	private global::System.Action<MapEntity> OrtonEncounterMapEntityChosen;

	private global::System.Action<MapEntity, bool> MapEntityInteractionFinished;

	private global::System.Action BuyMoreCoinsDialogRequest;

	private global::System.Action<MapEntity> MapEntityScanned;

	private global::System.Action StoreOpened;

	private global::System.Action StoreClosed;

	private global::System.Action<bool> HandleApplicationFocus;

	private global::System.Action<GenericDialogData> NetworkDialogRequestReceived;

	private global::System.Action NetworkDialogRequestRemoved;

	private global::System.Action RewardDialogOpened;

	private global::System.Action RewardDialogClosed;

	private global::System.Action RecallButtonTapped;

	private global::System.Action<global::UnityEngine.Vector2> TouchDetected;

	private global::System.Action<global::System.Collections.Generic.List<WorkshopSlotData>> WorkshopSlotDataUpdated;

	private global::System.Action<WorkshopSlotData, WorkshopEntry.Status, WorkshopEntry.Status> WorkshopButtonStateOverriden;

	private global::System.Action AnimatronicCreationRequestStarted;

	private global::System.Action AnimatronicCreationRequestComplete;

	private global::System.Action<float> UnityFrameUpdate;

	private global::System.Action<global::System.Collections.Generic.List<AnimatronicEntity>> MapEntityHasSpawnedAnimatronics;

	private global::System.Action WorkshopCpuChanged;

	private global::System.Action<SlotDisplayButtonType> WorkshopModifyTabOpened;

	private global::System.Action<SlotDisplayButtonType> WorkshopModifyTabClosed;

	private global::System.Action WorkshopRepairSuccess;

	private global::System.Action<ExtraBatteryStateChange> ExtraBatteryStateChanged;

	private FlashlightState.StateChanged FlashlightStateChanged;

	private global::System.Action FlashlightTriedToActivate;

	private global::System.Action FlashlightCooldownComplete;

	private global::System.Action<ShockerActivation> ShockerActivated;

	private global::System.Action ShockerCooldownComplete;

	private global::System.Action MaskForcedOff;

	private MaskState.StateChanged MaskStateChanged;

	private AttackDisruption.StateChanged AttackDisruptionStateChanged;

	private AttackSurge.StateChanged AttackSurgeStateChanged;

	private global::System.Action<Encounter> StartAttackEncounter;

	private global::System.Action<EncounterType> AttackEncounterStarted;

	private global::System.Action<string> EncounterAnimatronicInitialized;

	private global::System.Action<EncounterResult> AttackSequenceReadyForUi;

	private global::System.Action AttackEncounterEnded;

	private global::System.Action AttackSequenceEnded;

	private global::System.Action<StaticSettings> StaticSettingsUpdated;

	private global::System.Action RewardsFlowCompleted;

	private global::System.Action<PlayerProfile> PlayerProfileUpdated;

	private global::System.Action PlayerAvatarUpdated;

	private global::System.Action<UserNameSetError> DisplayNameObscenityFound;

	private global::System.Action<global::System.Collections.Generic.List<PlayerFriendsEntry>> FriendListUpdated;

	private global::System.Action<GameDisplayData> GameDisplayChange;

	private global::System.Action PrepareForSceneUnload;

	private global::System.Action<global::UnityEngine.AsyncOperation> SceneLoading;

	private global::System.Action<GameDisplayData.DisplayType> SceneLoaded;

	private global::System.Action<GameDisplayData.DisplayType> UICanvasDidAppear;

	private global::System.Action<GameDisplayData.DisplayType> UICanvasClosed;

	private global::System.Action<GameDisplayData.DisplayType> UIChangeRequest;

	private global::System.Action<string> PersonalFriendCodeUpdated;

	private global::System.Action<FriendCodeResponseHandler.FriendLookupResponse> FriendCodeLookedUp;

	private global::System.Action<bool> GeneratePlayStreamEventReceived;

	private global::System.Action<global::System.Collections.Generic.List<string>> PlayerAvatarUnlockedListReceived;

	private global::System.Action<long> PlayerStoreDataUpdated;

	private global::System.Action<AssemblyButtonPressedPayload> WorkshopModifyAssemblyButtonPressed;

	private global::System.Action AllOrtonBundlesDownloaded;

	private global::System.Action StopDisruptionButtonPressed;

	private global::System.Action<TrophyInventory> TrophyInventoryUpdated;

	private global::System.Action<TrophyData> TrophyChanged;

	private global::System.Action<IntroScreen.IntroScreenDialogData> EntityIntroDisplayRequestReceived;

	private global::System.Action<ScavengingEntity> AnimatronicScavengingEncounterStarted;

	private global::System.Action<ScavengingEntity> OrtonScavengingEncounterMapEntityChosen;

	private global::System.Action<ScavengingEncounter> StartAttackScavengingEncounter;

	private global::System.Action<EncounterType> AttackScavengingEncounterStarted;

	private global::System.Action AttackScavengingEncounterEnded;

	private global::System.Action<string> ScavengingEncounterAnimatronicInitialized;

	private global::System.Action<global::System.Collections.Generic.IEnumerable<ScavengingEntitySynchronizeableState>> ScavengingEntitiesReceivedFromServer;

	private global::System.Action<global::System.Collections.Generic.IEnumerable<ScavengingEntity>> ScavengingEntitiesModelsUpdated;

	public void add_GestureTouchEvent(global::System.Action<global::System.Collections.Generic.List<global::UnityEngine.Vector2>, global::System.Collections.Generic.List<global::UnityEngine.Vector2>> value)
	{
		GestureTouchEvent = (global::System.Action<global::System.Collections.Generic.List<global::UnityEngine.Vector2>, global::System.Collections.Generic.List<global::UnityEngine.Vector2>>)global::System.Delegate.Combine(GestureTouchEvent, value);
	}

	public void remove_GestureTouchEvent(global::System.Action<global::System.Collections.Generic.List<global::UnityEngine.Vector2>, global::System.Collections.Generic.List<global::UnityEngine.Vector2>> value)
	{
		GestureTouchEvent = (global::System.Action<global::System.Collections.Generic.List<global::UnityEngine.Vector2>, global::System.Collections.Generic.List<global::UnityEngine.Vector2>>)global::System.Delegate.Remove(GestureTouchEvent, value);
	}

	public void add_GestureResetEvent(global::System.Action value)
	{
		GestureResetEvent = (global::System.Action)global::System.Delegate.Combine(GestureResetEvent, value);
	}

	public void remove_GestureResetEvent(global::System.Action value)
	{
		GestureResetEvent = (global::System.Action)global::System.Delegate.Remove(GestureResetEvent, value);
	}

	public void OnGestureTouchEvent(global::System.Collections.Generic.List<global::UnityEngine.Vector2> prev, global::System.Collections.Generic.List<global::UnityEngine.Vector2> curr)
	{
		if (GestureTouchEvent != null)
		{
			GestureTouchEvent(prev, curr);
		}
	}

	public void add_EntityFastForwardComplete(global::System.Action<FastForwardCompleteArgs> value)
	{
		EntityFastForwardComplete = (global::System.Action<FastForwardCompleteArgs>)global::System.Delegate.Combine(EntityFastForwardComplete, value);
	}

	public void remove_EntityFastForwardComplete(global::System.Action<FastForwardCompleteArgs> value)
	{
		EntityFastForwardComplete = (global::System.Action<FastForwardCompleteArgs>)global::System.Delegate.Remove(EntityFastForwardComplete, value);
	}

	public void add_EntityAddedEvent(global::System.Action<Container.EntityAddedRemovedArgs> value)
	{
		EntityAddedEvent = (global::System.Action<Container.EntityAddedRemovedArgs>)global::System.Delegate.Combine(EntityAddedEvent, value);
	}

	public void remove_EntityAddedEvent(global::System.Action<Container.EntityAddedRemovedArgs> value)
	{
		EntityAddedEvent = (global::System.Action<Container.EntityAddedRemovedArgs>)global::System.Delegate.Remove(EntityAddedEvent, value);
	}

	public void add_EntityRemovedEvent(global::System.Action<Container.EntityAddedRemovedArgs> value)
	{
		EntityRemovedEvent = (global::System.Action<Container.EntityAddedRemovedArgs>)global::System.Delegate.Combine(EntityRemovedEvent, value);
	}

	public void remove_EntityRemovedEvent(global::System.Action<Container.EntityAddedRemovedArgs> value)
	{
		EntityRemovedEvent = (global::System.Action<Container.EntityAddedRemovedArgs>)global::System.Delegate.Remove(EntityRemovedEvent, value);
	}

	public void add_EntitiesClearedEvent(global::System.Action<Container.EntitiesClearedArgs> value)
	{
		EntitiesClearedEvent = (global::System.Action<Container.EntitiesClearedArgs>)global::System.Delegate.Combine(EntitiesClearedEvent, value);
	}

	public void remove_EntitiesClearedEvent(global::System.Action<Container.EntitiesClearedArgs> value)
	{
		EntitiesClearedEvent = (global::System.Action<Container.EntitiesClearedArgs>)global::System.Delegate.Remove(EntitiesClearedEvent, value);
	}

	public void OnEntityFastForwardComplete(FastForwardCompleteArgs args)
	{
		if (EntityFastForwardComplete != null)
		{
			EntityFastForwardComplete(args);
		}
	}

	public void OnEntityAddedEvent(Container.EntityAddedRemovedArgs e)
	{
		if (EntityAddedEvent == null)
		{
			global::UnityEngine.Debug.LogError("ENTITY ADDED EVENT FOR EVENT EXPOSER IS NULL!");
		}
		else
		{
			EntityAddedEvent(e);
		}
	}

	public void OnEntityRemovedEvent(Container.EntityAddedRemovedArgs e)
	{
		if (EntityRemovedEvent != null)
		{
			EntityRemovedEvent(e);
		}
	}

	public void OnEntitiesClearedEvent(Container.EntitiesClearedArgs e)
	{
		if (EntitiesClearedEvent != null)
		{
			EntitiesClearedEvent(e);
		}
	}

	public void add_NewAnimatronicSpawned(global::System.Action value)
	{
		NewAnimatronicSpawned = (global::System.Action)global::System.Delegate.Combine(NewAnimatronicSpawned, value);
	}

	public void remove_NewAnimatronicSpawned(global::System.Action value)
	{
		NewAnimatronicSpawned = (global::System.Action)global::System.Delegate.Remove(NewAnimatronicSpawned, value);
	}

	public void OnNewAnimatronicSpawned()
	{
		if (NewAnimatronicSpawned != null)
		{
			NewAnimatronicSpawned();
		}
	}

	public void add_AnimatronicEncounterStarted(global::System.Action<MapEntity> value)
	{
		AnimatronicEncounterStarted = (global::System.Action<MapEntity>)global::System.Delegate.Combine(AnimatronicEncounterStarted, value);
	}

	public void remove_AnimatronicEncounterStarted(global::System.Action<MapEntity> value)
	{
		AnimatronicEncounterStarted = (global::System.Action<MapEntity>)global::System.Delegate.Remove(AnimatronicEncounterStarted, value);
	}

	public void OnAnimatronicEncounterStarted(MapEntity entity)
	{
		if (AnimatronicEncounterStarted == null)
		{
			global::UnityEngine.Debug.Log("AnimatronicEncounterStarted is NULL.");
		}
		else
		{
			AnimatronicEncounterStarted(entity);
		}
	}

	public void add_MapEntityHasSpawnedAnimatronics(global::System.Action<global::System.Collections.Generic.List<AnimatronicEntity>> value)
	{
		MapEntityHasSpawnedAnimatronics = (global::System.Action<global::System.Collections.Generic.List<AnimatronicEntity>>)global::System.Delegate.Combine(MapEntityHasSpawnedAnimatronics, value);
	}

	public void remove_MapEntityHasSpawnedAnimatronics(global::System.Action<global::System.Collections.Generic.List<AnimatronicEntity>> value)
	{
		MapEntityHasSpawnedAnimatronics = (global::System.Action<global::System.Collections.Generic.List<AnimatronicEntity>>)global::System.Delegate.Remove(MapEntityHasSpawnedAnimatronics, value);
	}

	public void OnMapEntityHasSpawnedAnimatronics(global::System.Collections.Generic.List<AnimatronicEntity> entities)
	{
		if (MapEntityHasSpawnedAnimatronics != null)
		{
			MapEntityHasSpawnedAnimatronics(entities);
		}
	}

	public void add_ShockerActivated(global::System.Action<ShockerActivation> value)
	{
		ShockerActivated = (global::System.Action<ShockerActivation>)global::System.Delegate.Combine(ShockerActivated, value);
	}

	public void remove_ShockerActivated(global::System.Action<ShockerActivation> value)
	{
		ShockerActivated = (global::System.Action<ShockerActivation>)global::System.Delegate.Remove(ShockerActivated, value);
	}

	public void add_ShockerCooldownComplete(global::System.Action value)
	{
		ShockerCooldownComplete = (global::System.Action)global::System.Delegate.Combine(ShockerCooldownComplete, value);
	}

	public void remove_ShockerCooldownComplete(global::System.Action value)
	{
		ShockerCooldownComplete = (global::System.Action)global::System.Delegate.Remove(ShockerCooldownComplete, value);
	}

	public void OnShockerActivated(ShockerActivation shockerActivation)
	{
		if (ShockerActivated != null)
		{
			ShockerActivated(shockerActivation);
		}
	}

	public void OnShockerCooldownComplete()
	{
		if (ShockerCooldownComplete != null)
		{
			ShockerCooldownComplete();
		}
	}

	public void add_AttackDisruptionStateChanged(AttackDisruption.StateChanged value)
	{
		AttackDisruptionStateChanged = (AttackDisruption.StateChanged)global::System.Delegate.Combine(AttackDisruptionStateChanged, value);
	}

	public void remove_AttackDisruptionStateChanged(AttackDisruption.StateChanged value)
	{
		AttackDisruptionStateChanged = (AttackDisruption.StateChanged)global::System.Delegate.Remove(AttackDisruptionStateChanged, value);
	}

	public void OnAttackDisruptionStateChanged(bool isDisruptionActive, DisruptionStyle style)
	{
		if (AttackDisruptionStateChanged != null)
		{
			AttackDisruptionStateChanged(isDisruptionActive, style);
		}
	}

	public void add_StartAttackEncounter(global::System.Action<Encounter> value)
	{
		StartAttackEncounter = (global::System.Action<Encounter>)global::System.Delegate.Combine(StartAttackEncounter, value);
	}

	public void remove_StartAttackEncounter(global::System.Action<Encounter> value)
	{
		StartAttackEncounter = (global::System.Action<Encounter>)global::System.Delegate.Remove(StartAttackEncounter, value);
	}

	public void add_AttackEncounterStarted(global::System.Action<EncounterType> value)
	{
		AttackEncounterStarted = (global::System.Action<EncounterType>)global::System.Delegate.Combine(AttackEncounterStarted, value);
	}

	public void remove_AttackEncounterStarted(global::System.Action<EncounterType> value)
	{
		AttackEncounterStarted = (global::System.Action<EncounterType>)global::System.Delegate.Remove(AttackEncounterStarted, value);
	}

	public void add_EncounterAnimatronicInitialized(global::System.Action<string> value)
	{
		EncounterAnimatronicInitialized = (global::System.Action<string>)global::System.Delegate.Combine(EncounterAnimatronicInitialized, value);
	}

	public void remove_EncounterAnimatronicInitialized(global::System.Action<string> value)
	{
		EncounterAnimatronicInitialized = (global::System.Action<string>)global::System.Delegate.Remove(EncounterAnimatronicInitialized, value);
	}

	public void add_AttackSequenceReadyForUi(global::System.Action<EncounterResult> value)
	{
		AttackSequenceReadyForUi = (global::System.Action<EncounterResult>)global::System.Delegate.Combine(AttackSequenceReadyForUi, value);
	}

	public void remove_AttackSequenceReadyForUi(global::System.Action<EncounterResult> value)
	{
		AttackSequenceReadyForUi = (global::System.Action<EncounterResult>)global::System.Delegate.Remove(AttackSequenceReadyForUi, value);
	}

	public void add_AttackEncounterEnded(global::System.Action value)
	{
		AttackEncounterEnded = (global::System.Action)global::System.Delegate.Combine(AttackEncounterEnded, value);
	}

	public void remove_AttackEncounterEnded(global::System.Action value)
	{
		AttackEncounterEnded = (global::System.Action)global::System.Delegate.Remove(AttackEncounterEnded, value);
	}

	public void add_AttackSequenceEnded(global::System.Action value)
	{
		AttackSequenceEnded = (global::System.Action)global::System.Delegate.Combine(AttackSequenceEnded, value);
	}

	public void remove_AttackSequenceEnded(global::System.Action value)
	{
		AttackSequenceEnded = (global::System.Action)global::System.Delegate.Remove(AttackSequenceEnded, value);
	}

	public void add_StaticSettingsUpdated(global::System.Action<StaticSettings> value)
	{
		StaticSettingsUpdated = (global::System.Action<StaticSettings>)global::System.Delegate.Combine(StaticSettingsUpdated, value);
	}

	public void remove_StaticSettingsUpdated(global::System.Action<StaticSettings> value)
	{
		StaticSettingsUpdated = (global::System.Action<StaticSettings>)global::System.Delegate.Remove(StaticSettingsUpdated, value);
	}

	public void OnStartAttackEncounter(Encounter encounter)
	{
		if (StartAttackEncounter != null)
		{
			StartAttackEncounter(encounter);
		}
	}

	public void OnAttackEncounterStarted(EncounterType type)
	{
		if (AttackEncounterStarted != null)
		{
			AttackEncounterStarted(type);
		}
	}

	public void OnEncounterAnimatronicInitialized(string cpuId)
	{
		if (EncounterAnimatronicInitialized != null)
		{
			EncounterAnimatronicInitialized(cpuId);
		}
	}

	public void OnAttackSequenceReadyForUi(EncounterResult result)
	{
		if (AttackSequenceReadyForUi != null)
		{
			AttackSequenceReadyForUi(result);
		}
	}

	public void OnAttackSequenceEnded()
	{
		if (AttackSequenceEnded != null)
		{
			AttackSequenceEnded();
		}
	}

	public void OnAttackEncounterEnded()
	{
		if (AttackEncounterEnded != null)
		{
			AttackEncounterEnded();
		}
	}

	public void OnStaticSettingsUpdated(StaticSettings staticSettings)
	{
		if (StaticSettingsUpdated != null)
		{
			StaticSettingsUpdated(staticSettings);
		}
	}

	public void add_GameDisplayChange(global::System.Action<GameDisplayData> value)
	{
		GameDisplayChange = (global::System.Action<GameDisplayData>)global::System.Delegate.Combine(GameDisplayChange, value);
	}

	public void remove_GameDisplayChange(global::System.Action<GameDisplayData> value)
	{
		GameDisplayChange = (global::System.Action<GameDisplayData>)global::System.Delegate.Remove(GameDisplayChange, value);
	}

	public void add_PrepareForSceneUnload(global::System.Action value)
	{
		PrepareForSceneUnload = (global::System.Action)global::System.Delegate.Combine(PrepareForSceneUnload, value);
	}

	public void remove_PrepareForSceneUnload(global::System.Action value)
	{
		PrepareForSceneUnload = (global::System.Action)global::System.Delegate.Remove(PrepareForSceneUnload, value);
	}

	public void OnGameDisplayChange(GameDisplayData e)
	{
		if (GameDisplayChange != null)
		{
			GameDisplayChange(e);
		}
	}

	public void OnPrepareForSceneUnload()
	{
		if (PrepareForSceneUnload != null)
		{
			PrepareForSceneUnload();
		}
	}

	public void add_UICanvasDidAppear(global::System.Action<GameDisplayData.DisplayType> value)
	{
		UICanvasDidAppear = (global::System.Action<GameDisplayData.DisplayType>)global::System.Delegate.Combine(UICanvasDidAppear, value);
	}

	public void remove_UICanvasDidAppear(global::System.Action<GameDisplayData.DisplayType> value)
	{
		UICanvasDidAppear = (global::System.Action<GameDisplayData.DisplayType>)global::System.Delegate.Remove(UICanvasDidAppear, value);
	}

	public void add_UICanvasClosed(global::System.Action<GameDisplayData.DisplayType> value)
	{
		UICanvasClosed = (global::System.Action<GameDisplayData.DisplayType>)global::System.Delegate.Combine(UICanvasClosed, value);
	}

	public void remove_UICanvasClosed(global::System.Action<GameDisplayData.DisplayType> value)
	{
		UICanvasClosed = (global::System.Action<GameDisplayData.DisplayType>)global::System.Delegate.Remove(UICanvasClosed, value);
	}

	public void add_UIChangeRequest(global::System.Action<GameDisplayData.DisplayType> value)
	{
		UIChangeRequest = (global::System.Action<GameDisplayData.DisplayType>)global::System.Delegate.Combine(UIChangeRequest, value);
	}

	public void remove_UIChangeRequest(global::System.Action<GameDisplayData.DisplayType> value)
	{
		UIChangeRequest = (global::System.Action<GameDisplayData.DisplayType>)global::System.Delegate.Remove(UIChangeRequest, value);
	}

	public void OnUICanvasDidAppear(GameDisplayData.DisplayType displayType)
	{
		if (UICanvasDidAppear != null)
		{
			UICanvasDidAppear(displayType);
		}
	}

	public void OnUIChangeRequest(GameDisplayData.DisplayType obj)
	{
		if (UIChangeRequest != null)
		{
			UIChangeRequest(obj);
		}
	}

	public void OnUICanvasClosed(GameDisplayData.DisplayType displayType)
	{
		if (UICanvasClosed != null)
		{
			UICanvasClosed(displayType);
		}
	}

	public void add_RewardsFlowCompleted(global::System.Action value)
	{
		RewardsFlowCompleted = (global::System.Action)global::System.Delegate.Combine(RewardsFlowCompleted, value);
	}

	public void remove_RewardsFlowCompleted(global::System.Action value)
	{
		RewardsFlowCompleted = (global::System.Action)global::System.Delegate.Remove(RewardsFlowCompleted, value);
	}

	public void OnRewardsFlowCompleted()
	{
		if (RewardsFlowCompleted != null)
		{
			RewardsFlowCompleted();
		}
	}

	public void add_HandleApplicationFocus(global::System.Action<bool> value)
	{
		HandleApplicationFocus = (global::System.Action<bool>)global::System.Delegate.Combine(HandleApplicationFocus, value);
	}

	public void remove_HandleApplicationFocus(global::System.Action<bool> value)
	{
		HandleApplicationFocus = (global::System.Action<bool>)global::System.Delegate.Remove(HandleApplicationFocus, value);
	}

	public void OnApplicationFocus(bool focus)
	{
		if (HandleApplicationFocus != null)
		{
			HandleApplicationFocus(focus);
		}
	}

	public void add_UnityFrameUpdate(global::System.Action<float> value)
	{
		UnityFrameUpdate = (global::System.Action<float>)global::System.Delegate.Combine(UnityFrameUpdate, value);
	}

	public void remove_UnityFrameUpdate(global::System.Action<float> value)
	{
		UnityFrameUpdate = (global::System.Action<float>)global::System.Delegate.Remove(UnityFrameUpdate, value);
	}

	public void OnUnityFrameUpdate(float dt)
	{
		if (UnityFrameUpdate != null)
		{
			UnityFrameUpdate(dt);
		}
	}

	public void add_FlashlightStateChanged(FlashlightState.StateChanged value)
	{
		FlashlightStateChanged = (FlashlightState.StateChanged)global::System.Delegate.Combine(FlashlightStateChanged, value);
	}

	public void remove_FlashlightStateChanged(FlashlightState.StateChanged value)
	{
		FlashlightStateChanged = (FlashlightState.StateChanged)global::System.Delegate.Remove(FlashlightStateChanged, value);
	}

	public void add_FlashlightTriedToActivate(global::System.Action value)
	{
		FlashlightTriedToActivate = (global::System.Action)global::System.Delegate.Combine(FlashlightTriedToActivate, value);
	}

	public void remove_FlashlightTriedToActivate(global::System.Action value)
	{
		FlashlightTriedToActivate = (global::System.Action)global::System.Delegate.Remove(FlashlightTriedToActivate, value);
	}

	public void add_FlashlightCooldownComplete(global::System.Action value)
	{
		FlashlightCooldownComplete = (global::System.Action)global::System.Delegate.Combine(FlashlightCooldownComplete, value);
	}

	public void remove_FlashlightCooldownComplete(global::System.Action value)
	{
		FlashlightCooldownComplete = (global::System.Action)global::System.Delegate.Remove(FlashlightCooldownComplete, value);
	}

	public void OnFlashlightStateChanged(bool isFlashlightOn, bool shouldPlayAudio)
	{
		if (FlashlightStateChanged != null)
		{
			FlashlightStateChanged(isFlashlightOn, shouldPlayAudio);
		}
	}

	public void OnFlashlightTriedToActivate()
	{
		if (FlashlightTriedToActivate != null)
		{
			FlashlightTriedToActivate();
		}
	}

	public void OnFlashlightCooldownComplete()
	{
		if (FlashlightCooldownComplete != null)
		{
			FlashlightCooldownComplete();
		}
	}

	public void add_AllOrtonBundlesDownloaded(global::System.Action value)
	{
		AllOrtonBundlesDownloaded = (global::System.Action)global::System.Delegate.Combine(AllOrtonBundlesDownloaded, value);
	}

	public void remove_AllOrtonBundlesDownloaded(global::System.Action value)
	{
		AllOrtonBundlesDownloaded = (global::System.Action)global::System.Delegate.Remove(AllOrtonBundlesDownloaded, value);
	}

	public void OnAllOrtonBundlesDownloaded()
	{
		if (AllOrtonBundlesDownloaded != null)
		{
			AllOrtonBundlesDownloaded();
		}
	}

	public void add_WorkshopDataV2Updated(global::System.Action<WorkshopData> value)
	{
		WorkshopDataV2Updated = (global::System.Action<WorkshopData>)global::System.Delegate.Combine(WorkshopDataV2Updated, value);
	}

	public void remove_WorkshopDataV2Updated(global::System.Action<WorkshopData> value)
	{
		WorkshopDataV2Updated = (global::System.Action<WorkshopData>)global::System.Delegate.Remove(WorkshopDataV2Updated, value);
	}

	public void OnWorkshopDataV2Updated(WorkshopData data)
	{
		if (WorkshopDataV2Updated != null)
		{
			global::UnityEngine.Debug.LogError("INVOKED ON WORKSHOPDATA v2 UPDATED");
			WorkshopDataV2Updated(data);
		}
	}

	public void add_WorkshopSlotDataUpdated(global::System.Action<global::System.Collections.Generic.List<WorkshopSlotData>> value)
	{
		WorkshopSlotDataUpdated = (global::System.Action<global::System.Collections.Generic.List<WorkshopSlotData>>)global::System.Delegate.Combine(WorkshopSlotDataUpdated, value);
	}

	public void remove_WorkshopSlotDataUpdated(global::System.Action<global::System.Collections.Generic.List<WorkshopSlotData>> value)
	{
		WorkshopSlotDataUpdated = (global::System.Action<global::System.Collections.Generic.List<WorkshopSlotData>>)global::System.Delegate.Remove(WorkshopSlotDataUpdated, value);
	}

	public void OnWorkshopSlotDataUpdated(global::System.Collections.Generic.List<WorkshopSlotData> datas)
	{
		if (WorkshopSlotDataUpdated != null)
		{
			WorkshopSlotDataUpdated(datas);
		}
	}

	public void add_WorkshopCpuChanged(global::System.Action value)
	{
		WorkshopCpuChanged = (global::System.Action)global::System.Delegate.Combine(WorkshopCpuChanged, value);
	}

	public void remove_WorkshopCpuChanged(global::System.Action value)
	{
		WorkshopCpuChanged = (global::System.Action)global::System.Delegate.Remove(WorkshopCpuChanged, value);
	}

	public void add_WorkshopModifyTabOpened(global::System.Action<SlotDisplayButtonType> value)
	{
		WorkshopModifyTabOpened = (global::System.Action<SlotDisplayButtonType>)global::System.Delegate.Combine(WorkshopModifyTabOpened, value);
	}

	public void remove_WorkshopModifyTabOpened(global::System.Action<SlotDisplayButtonType> value)
	{
		WorkshopModifyTabOpened = (global::System.Action<SlotDisplayButtonType>)global::System.Delegate.Remove(WorkshopModifyTabOpened, value);
	}

	public void add_WorkshopModifyTabClosed(global::System.Action<SlotDisplayButtonType> value)
	{
		WorkshopModifyTabClosed = (global::System.Action<SlotDisplayButtonType>)global::System.Delegate.Combine(WorkshopModifyTabClosed, value);
	}

	public void remove_WorkshopModifyTabClosed(global::System.Action<SlotDisplayButtonType> value)
	{
		WorkshopModifyTabClosed = (global::System.Action<SlotDisplayButtonType>)global::System.Delegate.Remove(WorkshopModifyTabClosed, value);
	}

	public void add_WorkshopRepairSuccess(global::System.Action value)
	{
		WorkshopRepairSuccess = (global::System.Action)global::System.Delegate.Combine(WorkshopRepairSuccess, value);
	}

	public void remove_WorkshopRepairSuccess(global::System.Action value)
	{
		WorkshopRepairSuccess = (global::System.Action)global::System.Delegate.Remove(WorkshopRepairSuccess, value);
	}

	public void OnWorkshopCpuChanged()
	{
		if (WorkshopCpuChanged != null)
		{
			WorkshopCpuChanged();
		}
	}

	public void OnWorkshopModifyTabOpened(SlotDisplayButtonType tab)
	{
		if (WorkshopModifyTabOpened != null)
		{
			WorkshopModifyTabOpened(tab);
		}
	}

	public void OnWorkshopModifyTabClosed(SlotDisplayButtonType tab)
	{
		if (WorkshopModifyTabClosed != null)
		{
			WorkshopModifyTabClosed(tab);
		}
	}

	public void OnWorkshopRepairSuccess()
	{
		if (WorkshopRepairSuccess != null)
		{
			WorkshopRepairSuccess();
		}
	}

	public void add_StopDisruptionButtonPressed(global::System.Action value)
	{
		StopDisruptionButtonPressed = (global::System.Action)global::System.Delegate.Combine(StopDisruptionButtonPressed, value);
	}

	public void remove_StopDisruptionButtonPressed(global::System.Action value)
	{
		StopDisruptionButtonPressed = (global::System.Action)global::System.Delegate.Remove(StopDisruptionButtonPressed, value);
	}

	public void OnStopDisruptionButtonPressed()
	{
		if (StopDisruptionButtonPressed != null)
		{
			StopDisruptionButtonPressed();
		}
	}

	public void add_WorkshopButtonStateOverriden(global::System.Action<WorkshopSlotData, WorkshopEntry.Status, WorkshopEntry.Status> value)
	{
		WorkshopButtonStateOverriden = (global::System.Action<WorkshopSlotData, WorkshopEntry.Status, WorkshopEntry.Status>)global::System.Delegate.Combine(WorkshopButtonStateOverriden, value);
	}

	public void remove_WorkshopButtonStateOverriden(global::System.Action<WorkshopSlotData, WorkshopEntry.Status, WorkshopEntry.Status> value)
	{
		WorkshopButtonStateOverriden = (global::System.Action<WorkshopSlotData, WorkshopEntry.Status, WorkshopEntry.Status>)global::System.Delegate.Remove(WorkshopButtonStateOverriden, value);
	}

	public void OnWorkshopButtonStateOverride(WorkshopSlotData workshopEntry, WorkshopEntry.Status oldStatus, WorkshopEntry.Status newStatus)
	{
		if (WorkshopButtonStateOverriden != null)
		{
			WorkshopButtonStateOverriden(workshopEntry, oldStatus, newStatus);
		}
	}

	public void add_RecallButtonTapped(global::System.Action value)
	{
		RecallButtonTapped = (global::System.Action)global::System.Delegate.Combine(RecallButtonTapped, value);
	}

	public void remove_RecallButtonTapped(global::System.Action value)
	{
		RecallButtonTapped = (global::System.Action)global::System.Delegate.Remove(RecallButtonTapped, value);
	}

	public void OnRecallButtonTapped()
	{
		if (RecallButtonTapped != null)
		{
			RecallButtonTapped();
		}
	}

	public void add_GenericDialogRequestReceived(global::System.Action<GenericDialogData> value)
	{
		GenericDialogRequestReceived = (global::System.Action<GenericDialogData>)global::System.Delegate.Combine(GenericDialogRequestReceived, value);
	}

	public void remove_GenericDialogRequestReceived(global::System.Action<GenericDialogData> value)
	{
		GenericDialogRequestReceived = (global::System.Action<GenericDialogData>)global::System.Delegate.Remove(GenericDialogRequestReceived, value);
	}

	public void GenericDialogRequest(GenericDialogData genericDialogData)
	{
		if (GenericDialogRequestReceived != null)
		{
			GenericDialogRequestReceived(genericDialogData);
		}
	}

	public void add_FriendListUpdated(global::System.Action<global::System.Collections.Generic.List<PlayerFriendsEntry>> value)
	{
		FriendListUpdated = (global::System.Action<global::System.Collections.Generic.List<PlayerFriendsEntry>>)global::System.Delegate.Combine(FriendListUpdated, value);
	}

	public void remove_FriendListUpdated(global::System.Action<global::System.Collections.Generic.List<PlayerFriendsEntry>> value)
	{
		FriendListUpdated = (global::System.Action<global::System.Collections.Generic.List<PlayerFriendsEntry>>)global::System.Delegate.Remove(FriendListUpdated, value);
	}

	public void OnFriendListUpdated(global::System.Collections.Generic.List<PlayerFriendsEntry> data)
	{
		if (FriendListUpdated != null)
		{
			FriendListUpdated(data);
		}
	}

	public void add_WorkshopModifyAssemblyButtonPressed(global::System.Action<AssemblyButtonPressedPayload> value)
	{
		WorkshopModifyAssemblyButtonPressed = (global::System.Action<AssemblyButtonPressedPayload>)global::System.Delegate.Combine(WorkshopModifyAssemblyButtonPressed, value);
	}

	public void remove_WorkshopModifyAssemblyButtonPressed(global::System.Action<AssemblyButtonPressedPayload> value)
	{
		WorkshopModifyAssemblyButtonPressed = (global::System.Action<AssemblyButtonPressedPayload>)global::System.Delegate.Remove(WorkshopModifyAssemblyButtonPressed, value);
	}

	public void OnWorkshopModifyAssemblyButtonPressed(AssemblyButtonPressedPayload payload)
	{
		if (WorkshopModifyAssemblyButtonPressed != null)
		{
			WorkshopModifyAssemblyButtonPressed(payload);
		}
	}

	public void add_MaskForcedOff(global::System.Action value)
	{
		MaskForcedOff = (global::System.Action)global::System.Delegate.Combine(MaskForcedOff, value);
	}

	public void remove_MaskForcedOff(global::System.Action value)
	{
		MaskForcedOff = (global::System.Action)global::System.Delegate.Remove(MaskForcedOff, value);
	}

	public void add_MaskStateChanged(MaskState.StateChanged value)
	{
		MaskStateChanged = (MaskState.StateChanged)global::System.Delegate.Combine(MaskStateChanged, value);
	}

	public void remove_MaskStateChanged(MaskState.StateChanged value)
	{
		MaskStateChanged = (MaskState.StateChanged)global::System.Delegate.Remove(MaskStateChanged, value);
	}

	public void OnMaskForcedOff()
	{
		if (MaskForcedOff != null)
		{
			MaskForcedOff();
		}
	}

	public void OnMaskStateChanged(bool isMaskGoingOn, bool isMaskTransitionBeginning)
	{
		if (MaskStateChanged != null)
		{
			MaskStateChanged(isMaskGoingOn, isMaskTransitionBeginning);
		}
	}

	public void add_AnimatronicCreationRequestStarted(global::System.Action value)
	{
		AnimatronicCreationRequestStarted = (global::System.Action)global::System.Delegate.Combine(AnimatronicCreationRequestStarted, value);
	}

	public void remove_AnimatronicCreationRequestStarted(global::System.Action value)
	{
		AnimatronicCreationRequestStarted = (global::System.Action)global::System.Delegate.Remove(AnimatronicCreationRequestStarted, value);
	}

	public void OnAnimatronicCreationRequestStarted()
	{
		if (AnimatronicCreationRequestStarted != null)
		{
			AnimatronicCreationRequestStarted();
		}
	}

	public void add_TouchDetected(global::System.Action<global::UnityEngine.Vector2> value)
	{
		TouchDetected = (global::System.Action<global::UnityEngine.Vector2>)global::System.Delegate.Combine(TouchDetected, value);
	}

	public void remove_TouchDetected(global::System.Action<global::UnityEngine.Vector2> value)
	{
		TouchDetected = (global::System.Action<global::UnityEngine.Vector2>)global::System.Delegate.Remove(TouchDetected, value);
	}

	public void OnTouchDetected(global::UnityEngine.Vector2 position)
	{
		if (TouchDetected != null)
		{
			TouchDetected(position);
		}
	}

	public void add_ExtraBatteryStateChanged(global::System.Action<ExtraBatteryStateChange> value)
	{
		ExtraBatteryStateChanged = (global::System.Action<ExtraBatteryStateChange>)global::System.Delegate.Combine(ExtraBatteryStateChanged, value);
	}

	public void remove_ExtraBatteryStateChanged(global::System.Action<ExtraBatteryStateChange> value)
	{
		ExtraBatteryStateChanged = (global::System.Action<ExtraBatteryStateChange>)global::System.Delegate.Combine(ExtraBatteryStateChanged, value);
	}

	public void OnExtraBatteryStateChanged(ExtraBatteryStateChange extraBatteryStateChange)
	{
		if (ExtraBatteryStateChanged != null)
		{
			ExtraBatteryStateChanged(extraBatteryStateChange);
		}
	}

	public void add_AttackSurgeStateChanged(AttackSurge.StateChanged value)
	{
		AttackSurgeStateChanged = (AttackSurge.StateChanged)global::System.Delegate.Combine(AttackSurgeStateChanged, value);
	}

	public void remove_AttackSurgeStateChanged(AttackSurge.StateChanged value)
	{
		AttackSurgeStateChanged = (AttackSurge.StateChanged)global::System.Delegate.Remove(AttackSurgeStateChanged, value);
	}

	public void OnAttackSurgeStateChanged(bool isSurgeActive, SurgeData surgeSettings)
	{
		if (AttackSurgeStateChanged != null)
		{
			AttackSurgeStateChanged(isSurgeActive, surgeSettings);
		}
	}

	public void add_RewardsReceived(global::System.Action<RewardData> value)
	{
		RewardsReceived = (global::System.Action<RewardData>)global::System.Delegate.Combine(RewardsReceived, value);
	}

	public void remove_RewardsReceived(global::System.Action<RewardData> value)
	{
		RewardsReceived = (global::System.Action<RewardData>)global::System.Delegate.Remove(RewardsReceived, value);
	}

	public void OnRewardsReceived(RewardData e)
	{
		if (RewardsReceived != null)
		{
			RewardsReceived(e);
		}
	}

	public void add_LootRewardProcessed(global::System.Action<LootRewardDisplayData> value)
	{
		LootRewardProcessed = (global::System.Action<LootRewardDisplayData>)global::System.Delegate.Combine(LootRewardProcessed, value);
	}

	public void remove_LootRewardProcessed(global::System.Action<LootRewardDisplayData> value)
	{
		LootRewardProcessed = (global::System.Action<LootRewardDisplayData>)global::System.Delegate.Remove(LootRewardProcessed, value);
	}

	public void add_LootRewardDisplayRequestReceived(global::System.Action<LootRewardDisplayData> value)
	{
		LootRewardDisplayRequestReceived = (global::System.Action<LootRewardDisplayData>)global::System.Delegate.Combine(LootRewardDisplayRequestReceived, value);
	}

	public void remove_LootRewardDisplayRequestReceived(global::System.Action<LootRewardDisplayData> value)
	{
		LootRewardDisplayRequestReceived = (global::System.Action<LootRewardDisplayData>)global::System.Delegate.Remove(LootRewardDisplayRequestReceived, value);
	}

	public void OnLootRewardProcessed(LootRewardDisplayData data)
	{
		if (LootRewardProcessed != null)
		{
			LootRewardProcessed(data);
		}
	}

	public void OnLootRewardDisplayRequestReceived(LootRewardDisplayData data)
	{
		if (LootRewardDisplayRequestReceived != null)
		{
			LootRewardDisplayRequestReceived(data);
		}
	}

	public void add_NetworkDialogRequestReceived(global::System.Action<GenericDialogData> value)
	{
		NetworkDialogRequestReceived = (global::System.Action<GenericDialogData>)global::System.Delegate.Combine(NetworkDialogRequestReceived, value);
	}

	public void remove_NetworkDialogRequestReceived(global::System.Action<GenericDialogData> value)
	{
		NetworkDialogRequestReceived = (global::System.Action<GenericDialogData>)global::System.Delegate.Remove(NetworkDialogRequestReceived, value);
	}

	public void add_NetworkDialogRequestRemoved(global::System.Action value)
	{
		NetworkDialogRequestRemoved = (global::System.Action)global::System.Delegate.Combine(NetworkDialogRequestRemoved, value);
	}

	public void remove_NetworkDialogRequestRemoved(global::System.Action value)
	{
		NetworkDialogRequestRemoved = (global::System.Action)global::System.Delegate.Remove(NetworkDialogRequestRemoved, value);
	}

	public void OnNetworkDialogRequestReceived(GenericDialogData data)
	{
		if (NetworkDialogRequestReceived != null)
		{
			NetworkDialogRequestReceived(data);
		}
	}

	public void OnNetworkDialogRequestRemoved()
	{
		if (NetworkDialogRequestRemoved != null)
		{
			NetworkDialogRequestRemoved();
		}
	}

	public void add_AnimatronicCreationRequestComplete(global::System.Action value)
	{
		AnimatronicCreationRequestComplete = (global::System.Action)global::System.Delegate.Combine(AnimatronicCreationRequestComplete, value);
	}

	public void remove_AnimatronicCreationRequestComplete(global::System.Action value)
	{
		AnimatronicCreationRequestComplete = (global::System.Action)global::System.Delegate.Remove(AnimatronicCreationRequestComplete, value);
	}

	public void OnAnimatronicCreationRequestCompleted()
	{
		if (AnimatronicCreationRequestComplete != null)
		{
			AnimatronicCreationRequestComplete();
		}
	}

	public void add_PlayerCurrencyRefreshed(global::System.Action<global::System.Collections.Generic.Dictionary<string, int>> value)
	{
		PlayerCurrencyRefreshed = (global::System.Action<global::System.Collections.Generic.Dictionary<string, int>>)global::System.Delegate.Combine(PlayerCurrencyRefreshed, value);
	}

	public void remove_PlayerCurrencyRefreshed(global::System.Action<global::System.Collections.Generic.Dictionary<string, int>> value)
	{
		PlayerCurrencyRefreshed = (global::System.Action<global::System.Collections.Generic.Dictionary<string, int>>)global::System.Delegate.Remove(PlayerCurrencyRefreshed, value);
	}

	public void OnPlayerCurrencyRefreshed(global::System.Collections.Generic.Dictionary<string, int> e)
	{
		if (PlayerCurrencyRefreshed != null)
		{
			PlayerCurrencyRefreshed(e);
		}
	}

	public void add_CPUInventoryUpdated(global::System.Action<CPUInventory> value)
	{
		CPUInventoryUpdated = (global::System.Action<CPUInventory>)global::System.Delegate.Combine(CPUInventoryUpdated, value);
	}

	public void remove_CPUInventoryUpdated(global::System.Action<CPUInventory> value)
	{
		CPUInventoryUpdated = (global::System.Action<CPUInventory>)global::System.Delegate.Remove(CPUInventoryUpdated, value);
	}

	public void OnCPUInventoryUpdated(CPUInventory data)
	{
		if (CPUInventoryUpdated != null)
		{
			CPUInventoryUpdated(data);
		}
	}

	public void add_SceneLoading(global::System.Action<global::UnityEngine.AsyncOperation> value)
	{
		SceneLoading = (global::System.Action<global::UnityEngine.AsyncOperation>)global::System.Delegate.Combine(SceneLoading, value);
	}

	public void remove_SceneLoading(global::System.Action<global::UnityEngine.AsyncOperation> value)
	{
		SceneLoading = (global::System.Action<global::UnityEngine.AsyncOperation>)global::System.Delegate.Remove(SceneLoading, value);
	}

	public void OnLoadScene(global::UnityEngine.AsyncOperation operation)
	{
		if (SceneLoading != null)
		{
			SceneLoading(operation);
		}
	}

	public void add_MapEntitiesReceivedFromServer(global::System.Action<global::System.Collections.Generic.IEnumerable<MapEntitySynchronizeableState>> value)
	{
		MapEntitiesReceivedFromServer = (global::System.Action<global::System.Collections.Generic.IEnumerable<MapEntitySynchronizeableState>>)global::System.Delegate.Combine(MapEntitiesReceivedFromServer, value);
	}

	public void remove_MapEntitiesReceivedFromServer(global::System.Action<global::System.Collections.Generic.IEnumerable<MapEntitySynchronizeableState>> value)
	{
		MapEntitiesReceivedFromServer = (global::System.Action<global::System.Collections.Generic.IEnumerable<MapEntitySynchronizeableState>>)global::System.Delegate.Remove(MapEntitiesReceivedFromServer, value);
	}

	public void OnMapEntitiesReceivedFromServer(global::System.Collections.Generic.IEnumerable<MapEntitySynchronizeableState> e)
	{
		global::UnityEngine.Debug.Log("GOT MAP ENTITIES FROM SERVER! IT WORKS!");
		foreach (MapEntitySynchronizeableState item in e)
		{
			global::UnityEngine.Debug.Log("SUIT - " + item.parts["PlushSuit"]);
			global::UnityEngine.Debug.Log("CPU - " + item.parts["Cpu"]);
		}
		if (MapEntitiesReceivedFromServer != null)
		{
			MapEntitiesReceivedFromServer(e);
		}
	}

	public void add_OrtonEncounterMapEntityChosen(global::System.Action<MapEntity> value)
	{
		OrtonEncounterMapEntityChosen = (global::System.Action<MapEntity>)global::System.Delegate.Combine(OrtonEncounterMapEntityChosen, value);
	}

	public void remove_OrtonEncounterMapEntityChosen(global::System.Action<MapEntity> value)
	{
		OrtonEncounterMapEntityChosen = (global::System.Action<MapEntity>)global::System.Delegate.Remove(OrtonEncounterMapEntityChosen, value);
	}

	public void OnOrtonEncounterMapEntityChosen(MapEntity entity)
	{
		if (OrtonEncounterMapEntityChosen != null)
		{
			OrtonEncounterMapEntityChosen(entity);
		}
	}

	public void add_MapEntityInteractionFinished(global::System.Action<MapEntity, bool> value)
	{
		MapEntityInteractionFinished = (global::System.Action<MapEntity, bool>)global::System.Delegate.Combine(MapEntityInteractionFinished, value);
	}

	public void remove_MapEntityInteractionFinished(global::System.Action<MapEntity, bool> value)
	{
		MapEntityInteractionFinished = (global::System.Action<MapEntity, bool>)global::System.Delegate.Remove(MapEntityInteractionFinished, value);
	}

	public void OnMapEntityInteractionFinished(MapEntity entity, bool giveRewards)
	{
		if (MapEntityInteractionFinished != null)
		{
			MapEntityInteractionFinished(entity, giveRewards);
		}
	}

	public void add_MapEntityScanned(global::System.Action<MapEntity> value)
	{
		MapEntityScanned = (global::System.Action<MapEntity>)global::System.Delegate.Combine(MapEntityScanned, value);
	}

	public void remove_MapEntityScanned(global::System.Action<MapEntity> value)
	{
		MapEntityScanned = (global::System.Action<MapEntity>)global::System.Delegate.Remove(MapEntityScanned, value);
	}

	public void OnMapEntityScanned(MapEntity entity)
	{
		if (MapEntityScanned != null)
		{
			MapEntityScanned(entity);
		}
	}

	public void add_MapEntitiesModelsUpdated(global::System.Action<global::System.Collections.Generic.IEnumerable<MapEntity>> value)
	{
		MapEntitiesModelsUpdated = (global::System.Action<global::System.Collections.Generic.IEnumerable<MapEntity>>)global::System.Delegate.Combine(MapEntitiesModelsUpdated, value);
	}

	public void remove_MapEntitiesModelsUpdated(global::System.Action<global::System.Collections.Generic.IEnumerable<MapEntity>> value)
	{
		MapEntitiesModelsUpdated = (global::System.Action<global::System.Collections.Generic.IEnumerable<MapEntity>>)global::System.Delegate.Remove(MapEntitiesModelsUpdated, value);
	}

	public void OnMapEntitiesModelsUpdated(global::System.Collections.Generic.IEnumerable<MapEntity> e)
	{
		if (MapEntitiesModelsUpdated != null)
		{
			MapEntitiesModelsUpdated(e);
		}
	}

	public void add_EntityWanderingAnimatronicDisplayRequestReceived(global::System.Action<EntityDisplayData> value)
	{
		EntityWanderingAnimatronicDisplayRequestReceived = (global::System.Action<EntityDisplayData>)global::System.Delegate.Combine(EntityWanderingAnimatronicDisplayRequestReceived, value);
	}

	public void remove_EntityWanderingAnimatronicDisplayRequestReceived(global::System.Action<EntityDisplayData> value)
	{
		EntityWanderingAnimatronicDisplayRequestReceived = (global::System.Action<EntityDisplayData>)global::System.Delegate.Remove(EntityWanderingAnimatronicDisplayRequestReceived, value);
	}

	public void OnEntityWanderingAnimatronicDisplayRequestReceived(EntityDisplayData data)
	{
		if (EntityWanderingAnimatronicDisplayRequestReceived != null)
		{
			EntityWanderingAnimatronicDisplayRequestReceived(data);
		}
	}

	public void add_EntitySpecialDeliveryDisplayRequestReceived(global::System.Action<EntityDisplayData> value)
	{
		EntitySpecialDeliveryDisplayRequestReceived = (global::System.Action<EntityDisplayData>)global::System.Delegate.Combine(EntitySpecialDeliveryDisplayRequestReceived, value);
	}

	public void remove_EntitySpecialDeliveryDisplayRequestReceived(global::System.Action<EntityDisplayData> value)
	{
		EntitySpecialDeliveryDisplayRequestReceived = (global::System.Action<EntityDisplayData>)global::System.Delegate.Remove(EntitySpecialDeliveryDisplayRequestReceived, value);
	}

	public void OnEntitySpecialDeliveryDisplayRequestReceived(EntityDisplayData data)
	{
		if (EntitySpecialDeliveryDisplayRequestReceived != null)
		{
			EntitySpecialDeliveryDisplayRequestReceived(data);
		}
	}

	public void add_StreakDataUpdated(global::System.Action<StreakData> value)
	{
		StreakDataUpdated = (global::System.Action<StreakData>)global::System.Delegate.Combine(StreakDataUpdated, value);
	}

	public void remove_StreakDataUpdated(global::System.Action<StreakData> value)
	{
		StreakDataUpdated = (global::System.Action<StreakData>)global::System.Delegate.Remove(StreakDataUpdated, value);
	}

	public void OnStreakDataUpdated(StreakData data)
	{
		if (StreakDataUpdated != null)
		{
			StreakDataUpdated(data);
		}
	}

	public void add_InventoryUpdated(global::System.Action<PlayerInventory> value)
	{
		InventoryUpdated = (global::System.Action<PlayerInventory>)global::System.Delegate.Combine(InventoryUpdated, value);
	}

	public void remove_InventoryUpdated(global::System.Action<PlayerInventory> value)
	{
		InventoryUpdated = (global::System.Action<PlayerInventory>)global::System.Delegate.Remove(InventoryUpdated, value);
	}

	public void OnInventoryUpdated(PlayerInventory e)
	{
		if (InventoryUpdated != null)
		{
			InventoryUpdated(e);
		}
	}

	public void add_SceneLoaded(global::System.Action<GameDisplayData.DisplayType> value)
	{
		SceneLoaded = (global::System.Action<GameDisplayData.DisplayType>)global::System.Delegate.Combine(SceneLoaded, value);
	}

	public void remove_SceneLoaded(global::System.Action<GameDisplayData.DisplayType> value)
	{
		SceneLoaded = (global::System.Action<GameDisplayData.DisplayType>)global::System.Delegate.Remove(SceneLoaded, value);
	}

	public void OnSceneLoaded(GameDisplayData.DisplayType type)
	{
		if (SceneLoaded != null)
		{
			SceneLoaded(type);
		}
	}

	public void add_VirtualGoodsDataReceived(global::System.Action<global::System.Collections.Generic.List<StoreContainer.StorefrontData>> value)
	{
		VirtualGoodsDataReceived = (global::System.Action<global::System.Collections.Generic.List<StoreContainer.StorefrontData>>)global::System.Delegate.Combine(VirtualGoodsDataReceived, value);
	}

	public void remove_VirtualGoodsDataReceived(global::System.Action<global::System.Collections.Generic.List<StoreContainer.StorefrontData>> value)
	{
		VirtualGoodsDataReceived = (global::System.Action<global::System.Collections.Generic.List<StoreContainer.StorefrontData>>)global::System.Delegate.Remove(VirtualGoodsDataReceived, value);
	}

	public void OnVirtualGoodsDataReceived(global::System.Collections.Generic.List<StoreContainer.StorefrontData> response)
	{
		if (VirtualGoodsDataReceived != null)
		{
			VirtualGoodsDataReceived(response);
		}
	}

	public void add_PlayerGoodsUpdated(global::System.Action<global::System.Collections.Generic.Dictionary<string, int>> value)
	{
		PlayerGoodsUpdated = (global::System.Action<global::System.Collections.Generic.Dictionary<string, int>>)global::System.Delegate.Combine(PlayerGoodsUpdated, value);
	}

	public void remove_PlayerGoodsUpdated(global::System.Action<global::System.Collections.Generic.Dictionary<string, int>> value)
	{
		PlayerGoodsUpdated = (global::System.Action<global::System.Collections.Generic.Dictionary<string, int>>)global::System.Delegate.Remove(PlayerGoodsUpdated, value);
	}

	public void OnPlayerGoodsUpdated(global::System.Collections.Generic.Dictionary<string, int> ownedGoods)
	{
		if (PlayerGoodsUpdated != null)
		{
			PlayerGoodsUpdated(ownedGoods);
		}
	}

	public void add_PlayerStoreDataUpdated(global::System.Action<long> value)
	{
		PlayerStoreDataUpdated = (global::System.Action<long>)global::System.Delegate.Combine(PlayerStoreDataUpdated, value);
	}

	public void remove_PlayerStoreDataUpdated(global::System.Action<long> value)
	{
		PlayerStoreDataUpdated = (global::System.Action<long>)global::System.Delegate.Remove(PlayerStoreDataUpdated, value);
	}

	public void OnPlayerStoreDataUpdated(long timestamp)
	{
		if (PlayerStoreDataUpdated != null)
		{
			PlayerStoreDataUpdated(timestamp);
		}
	}

	public void add_StoreDialogRequestReceived(global::System.Action<StoreDisplayData> value)
	{
		StoreDialogRequestReceived = (global::System.Action<StoreDisplayData>)global::System.Delegate.Combine(StoreDialogRequestReceived, value);
	}

	public void remove_StoreDialogRequestReceived(global::System.Action<StoreDisplayData> value)
	{
		StoreDialogRequestReceived = (global::System.Action<StoreDisplayData>)global::System.Delegate.Remove(StoreDialogRequestReceived, value);
	}

	public void OnStoreDialogRequest(StoreDisplayData storeDisplayData)
	{
		if (StoreDialogRequestReceived != null)
		{
			StoreDialogRequestReceived(storeDisplayData);
		}
	}

	public void add_PurchaseRequestAudioInvoked(global::System.Action<bool> value)
	{
		PurchaseRequestAudioInvoked = (global::System.Action<bool>)global::System.Delegate.Combine(PurchaseRequestAudioInvoked, value);
	}

	public void remove_PurchaseRequestAudioInvoked(global::System.Action<bool> value)
	{
		PurchaseRequestAudioInvoked = (global::System.Action<bool>)global::System.Delegate.Remove(PurchaseRequestAudioInvoked, value);
	}

	public void OnPurchaseRequestAudioInvoked(bool canAfford)
	{
		if (PurchaseRequestAudioInvoked != null)
		{
			PurchaseRequestAudioInvoked(canAfford);
		}
	}

	public void add_BuyMoreCoinsDialogRequest(global::System.Action value)
	{
		BuyMoreCoinsDialogRequest = (global::System.Action)global::System.Delegate.Combine(BuyMoreCoinsDialogRequest, value);
	}

	public void remove_BuyMoreCoinsDialogRequest(global::System.Action value)
	{
		BuyMoreCoinsDialogRequest = (global::System.Action)global::System.Delegate.Remove(BuyMoreCoinsDialogRequest, value);
	}

	public void OnBuyMoreCoinsDialogRequest()
	{
		if (BuyMoreCoinsDialogRequest != null)
		{
			BuyMoreCoinsDialogRequest();
		}
	}

	public void add_ExitAttackSequenceReceived(global::System.Action<ExitAttackSequenceDialogData> value)
	{
		ExitAttackSequenceReceived = (global::System.Action<ExitAttackSequenceDialogData>)global::System.Delegate.Combine(ExitAttackSequenceReceived, value);
	}

	public void remove_ExitAttackSequenceReceived(global::System.Action<ExitAttackSequenceDialogData> value)
	{
		ExitAttackSequenceReceived = (global::System.Action<ExitAttackSequenceDialogData>)global::System.Delegate.Remove(ExitAttackSequenceReceived, value);
	}

	public void OnExitAttackSequenceReceived(ExitAttackSequenceDialogData data)
	{
		if (ExitAttackSequenceReceived != null)
		{
			ExitAttackSequenceReceived(data);
		}
	}

	public void add_TrophyInventoryUpdated(global::System.Action<TrophyInventory> value)
	{
		TrophyInventoryUpdated = (global::System.Action<TrophyInventory>)global::System.Delegate.Combine(TrophyInventoryUpdated, value);
	}

	public void remove_TrophyInventoryUpdated(global::System.Action<TrophyInventory> value)
	{
		TrophyInventoryUpdated = (global::System.Action<TrophyInventory>)global::System.Delegate.Remove(TrophyInventoryUpdated, value);
	}

	public void OnTrophyInventoryUpdated(TrophyInventory data)
	{
		if (TrophyInventoryUpdated != null)
		{
			TrophyInventoryUpdated(data);
		}
	}

	public void add_TrophyChanged(global::System.Action<TrophyData> value)
	{
		TrophyChanged = (global::System.Action<TrophyData>)global::System.Delegate.Combine(TrophyChanged, value);
	}

	public void remove_TrophyChanged(global::System.Action<TrophyData> value)
	{
		TrophyChanged = (global::System.Action<TrophyData>)global::System.Delegate.Remove(TrophyChanged, value);
	}

	public void OnTrophyChanged(TrophyData data)
	{
		if (TrophyChanged != null)
		{
			TrophyChanged(data);
		}
	}

	public void add_PersonalFriendCodeUpdated(global::System.Action<string> value)
	{
		PersonalFriendCodeUpdated = (global::System.Action<string>)global::System.Delegate.Combine(PersonalFriendCodeUpdated, value);
	}

	public void remove_PersonalFriendCodeUpdated(global::System.Action<string> value)
	{
		PersonalFriendCodeUpdated = (global::System.Action<string>)global::System.Delegate.Remove(PersonalFriendCodeUpdated, value);
	}

	public void add_FriendCodeLookedUp(global::System.Action<FriendCodeResponseHandler.FriendLookupResponse> value)
	{
		FriendCodeLookedUp = (global::System.Action<FriendCodeResponseHandler.FriendLookupResponse>)global::System.Delegate.Combine(FriendCodeLookedUp, value);
	}

	public void remove_FriendCodeLookedUp(global::System.Action<FriendCodeResponseHandler.FriendLookupResponse> value)
	{
		FriendCodeLookedUp = (global::System.Action<FriendCodeResponseHandler.FriendLookupResponse>)global::System.Delegate.Remove(FriendCodeLookedUp, value);
	}

	public void OnPersonalFriendCodeUpdated(string friendCode)
	{
		if (PersonalFriendCodeUpdated != null)
		{
			PersonalFriendCodeUpdated(friendCode);
		}
	}

	public void OnFriendCodeLookedUp(FriendCodeResponseHandler.FriendLookupResponse response)
	{
		if (FriendCodeLookedUp != null)
		{
			FriendCodeLookedUp(response);
		}
	}

	public void add_PlayerAvatarUnlockedListReceived(global::System.Action<global::System.Collections.Generic.List<string>> value)
	{
		PlayerAvatarUnlockedListReceived = (global::System.Action<global::System.Collections.Generic.List<string>>)global::System.Delegate.Combine(PlayerAvatarUnlockedListReceived, value);
	}

	public void remove_PlayerAvatarUnlockedListReceived(global::System.Action<global::System.Collections.Generic.List<string>> value)
	{
		PlayerAvatarUnlockedListReceived = (global::System.Action<global::System.Collections.Generic.List<string>>)global::System.Delegate.Remove(PlayerAvatarUnlockedListReceived, value);
	}

	public void OnPlayerAvatarUnlockedListReceived(global::System.Collections.Generic.List<string> obj)
	{
		if (PlayerAvatarUnlockedListReceived != null)
		{
			PlayerAvatarUnlockedListReceived(obj);
		}
	}

	public void add_PlayerProfileUpdated(global::System.Action<PlayerProfile> value)
	{
		PlayerProfileUpdated = (global::System.Action<PlayerProfile>)global::System.Delegate.Combine(PlayerProfileUpdated, value);
	}

	public void remove_PlayerProfileUpdated(global::System.Action<PlayerProfile> value)
	{
		PlayerProfileUpdated = (global::System.Action<PlayerProfile>)global::System.Delegate.Remove(PlayerProfileUpdated, value);
	}

	public void add_PlayerAvatarUpdated(global::System.Action value)
	{
		PlayerAvatarUpdated = (global::System.Action)global::System.Delegate.Combine(PlayerAvatarUpdated, value);
	}

	public void remove_PlayerAvatarUpdated(global::System.Action value)
	{
		PlayerAvatarUpdated = (global::System.Action)global::System.Delegate.Remove(PlayerAvatarUpdated, value);
	}

	public void add_DisplayNameObscenityFound(global::System.Action<UserNameSetError> value)
	{
		DisplayNameObscenityFound = (global::System.Action<UserNameSetError>)global::System.Delegate.Combine(DisplayNameObscenityFound, value);
	}

	public void remove_DisplayNameObscenityFound(global::System.Action<UserNameSetError> value)
	{
		DisplayNameObscenityFound = (global::System.Action<UserNameSetError>)global::System.Delegate.Remove(DisplayNameObscenityFound, value);
	}

	public void OnPlayerProfileUpdated(PlayerProfile data)
	{
		if (PlayerProfileUpdated != null)
		{
			PlayerProfileUpdated(data);
		}
	}

	public void OnPlayerAvatarUpdated()
	{
		if (PlayerAvatarUpdated != null)
		{
			PlayerAvatarUpdated();
		}
	}

	public void OnDisplayNameObscenityFound(UserNameSetError error)
	{
		if (DisplayNameObscenityFound != null)
		{
			DisplayNameObscenityFound(error);
		}
	}

	public void add_GeneratePlayStreamEventReceived(global::System.Action<bool> value)
	{
		GeneratePlayStreamEventReceived = (global::System.Action<bool>)global::System.Delegate.Combine(GeneratePlayStreamEventReceived, value);
	}

	public void remove_GeneratePlayStreamEventReceived(global::System.Action<bool> value)
	{
		GeneratePlayStreamEventReceived = (global::System.Action<bool>)global::System.Delegate.Remove(GeneratePlayStreamEventReceived, value);
	}

	public void OnGeneratePlayStreamEventReceived(bool generatePlayStreamEvent)
	{
		if (GeneratePlayStreamEventReceived != null)
		{
			GeneratePlayStreamEventReceived(generatePlayStreamEvent);
		}
	}

	public void add_EntityIntroDisplayRequestReceived(global::System.Action<IntroScreen.IntroScreenDialogData> value)
	{
		EntityIntroDisplayRequestReceived = (global::System.Action<IntroScreen.IntroScreenDialogData>)global::System.Delegate.Combine(EntityIntroDisplayRequestReceived, value);
	}

	public void remove_EntityIntroDisplayRequestReceived(global::System.Action<IntroScreen.IntroScreenDialogData> value)
	{
		EntityIntroDisplayRequestReceived = (global::System.Action<IntroScreen.IntroScreenDialogData>)global::System.Delegate.Remove(EntityIntroDisplayRequestReceived, value);
	}

	public void OnEntityIntroDisplayRequestReceived(IntroScreen.IntroScreenDialogData data)
	{
		if (EntityIntroDisplayRequestReceived != null)
		{
			EntityIntroDisplayRequestReceived(data);
		}
	}

	public void add_RewardDialogOpened(global::System.Action value)
	{
		RewardDialogOpened = (global::System.Action)global::System.Delegate.Combine(RewardDialogOpened, value);
	}

	public void remove_RewardDialogOpened(global::System.Action value)
	{
		RewardDialogOpened = (global::System.Action)global::System.Delegate.Remove(RewardDialogOpened, value);
	}

	public void add_RewardDialogClosed(global::System.Action value)
	{
		RewardDialogClosed = (global::System.Action)global::System.Delegate.Combine(RewardDialogClosed, value);
	}

	public void remove_RewardDialogClosed(global::System.Action value)
	{
		RewardDialogClosed = (global::System.Action)global::System.Delegate.Remove(RewardDialogClosed, value);
	}

	public void OnRewardDialogOpened()
	{
		if (RewardDialogOpened != null)
		{
			RewardDialogOpened();
		}
	}

	public void OnRewardDialogClosed()
	{
		if (RewardDialogClosed != null)
		{
			RewardDialogClosed();
		}
	}

	public void add_ModInventoryUpdated(global::System.Action<ModInventory> value)
	{
		ModInventoryUpdated = (global::System.Action<ModInventory>)global::System.Delegate.Combine(ModInventoryUpdated, value);
	}

	public void remove_ModInventoryUpdated(global::System.Action<ModInventory> value)
	{
		ModInventoryUpdated = (global::System.Action<ModInventory>)global::System.Delegate.Remove(ModInventoryUpdated, value);
	}

	public void OnModInventoryUpdated(ModInventory data)
	{
		if (ModInventoryUpdated != null)
		{
			ModInventoryUpdated(data);
		}
	}

	public void add_StoreOpened(global::System.Action value)
	{
		StoreOpened = (global::System.Action)global::System.Delegate.Combine(StoreOpened, value);
	}

	public void remove_StoreOpened(global::System.Action value)
	{
		StoreOpened = (global::System.Action)global::System.Delegate.Remove(StoreOpened, value);
	}

	public void add_StoreClosed(global::System.Action value)
	{
		StoreClosed = (global::System.Action)global::System.Delegate.Combine(StoreClosed, value);
	}

	public void remove_StoreClosed(global::System.Action value)
	{
		StoreClosed = (global::System.Action)global::System.Delegate.Remove(StoreClosed, value);
	}

	public void OnStoreOpened()
	{
		if (StoreOpened != null)
		{
			StoreOpened();
		}
	}

	public void OnStoreClosed()
	{
		if (StoreClosed != null)
		{
			StoreClosed();
		}
	}

	public void add_OrtonScavengingEncounterMapEntityChosen(global::System.Action<ScavengingEntity> value)
	{
		OrtonScavengingEncounterMapEntityChosen = (global::System.Action<ScavengingEntity>)global::System.Delegate.Combine(OrtonScavengingEncounterMapEntityChosen, value);
	}

	public void remove_OrtonScavengingEncounterMapEntityChosen(global::System.Action<ScavengingEntity> value)
	{
		OrtonScavengingEncounterMapEntityChosen = (global::System.Action<ScavengingEntity>)global::System.Delegate.Remove(OrtonScavengingEncounterMapEntityChosen, value);
	}

	public void OnOrtonScavengingEncounterMapEntityChosen(ScavengingEntity entity)
	{
		if (OrtonScavengingEncounterMapEntityChosen != null)
		{
			OrtonScavengingEncounterMapEntityChosen(entity);
		}
	}

	public void add_AnimatronicScavengingEncounterStarted(global::System.Action<ScavengingEntity> value)
	{
		AnimatronicScavengingEncounterStarted = (global::System.Action<ScavengingEntity>)global::System.Delegate.Combine(AnimatronicScavengingEncounterStarted, value);
	}

	public void remove_AnimatronicScavengingEncounterStarted(global::System.Action<ScavengingEntity> value)
	{
		AnimatronicScavengingEncounterStarted = (global::System.Action<ScavengingEntity>)global::System.Delegate.Remove(AnimatronicScavengingEncounterStarted, value);
	}

	public void OnAnimatronicScavengingEncounterStarted(ScavengingEntity entity)
	{
		if (AnimatronicScavengingEncounterStarted == null)
		{
			global::UnityEngine.Debug.Log("AnimatronicScavengingEncounterStarted is NULL.");
		}
		else
		{
			AnimatronicScavengingEncounterStarted(entity);
		}
	}

	public void add_StartAttackScavengingEncounter(global::System.Action<ScavengingEncounter> value)
	{
		StartAttackScavengingEncounter = (global::System.Action<ScavengingEncounter>)global::System.Delegate.Combine(StartAttackScavengingEncounter, value);
	}

	public void remove_StartAttackScavengingEncounter(global::System.Action<ScavengingEncounter> value)
	{
		StartAttackScavengingEncounter = (global::System.Action<ScavengingEncounter>)global::System.Delegate.Remove(StartAttackScavengingEncounter, value);
	}

	public void OnStartAttackScavengingEncounter(ScavengingEncounter encounter)
	{
		if (StartAttackScavengingEncounter != null)
		{
			StartAttackScavengingEncounter(encounter);
		}
	}

	public void add_AttackScavengingEncounterStarted(global::System.Action<EncounterType> value)
	{
		AttackScavengingEncounterStarted = (global::System.Action<EncounterType>)global::System.Delegate.Combine(AttackScavengingEncounterStarted, value);
	}

	public void remove_AttackScavengingEncounterStarted(global::System.Action<EncounterType> value)
	{
		AttackScavengingEncounterStarted = (global::System.Action<EncounterType>)global::System.Delegate.Remove(AttackScavengingEncounterStarted, value);
	}

	public void OnAttackScavengingEncounterStarted(EncounterType type)
	{
		if (AttackScavengingEncounterStarted != null)
		{
			AttackScavengingEncounterStarted(type);
		}
	}

	public void add_AttackScavengingEncounterEnded(global::System.Action value)
	{
		AttackScavengingEncounterEnded = (global::System.Action)global::System.Delegate.Combine(AttackScavengingEncounterEnded, value);
	}

	public void remove_AttackScavengingEncounterEnded(global::System.Action value)
	{
		AttackScavengingEncounterEnded = (global::System.Action)global::System.Delegate.Remove(AttackScavengingEncounterEnded, value);
	}

	public void OnAttackScavengingEncounterEnded()
	{
		if (AttackScavengingEncounterEnded != null)
		{
			AttackScavengingEncounterEnded();
		}
	}

	public void add_ScavengingEncounterAnimatronicInitialized(global::System.Action<string> value)
	{
		ScavengingEncounterAnimatronicInitialized = (global::System.Action<string>)global::System.Delegate.Combine(ScavengingEncounterAnimatronicInitialized, value);
	}

	public void remove_ScavengingEncounterAnimatronicInitialized(global::System.Action<string> value)
	{
		ScavengingEncounterAnimatronicInitialized = (global::System.Action<string>)global::System.Delegate.Remove(ScavengingEncounterAnimatronicInitialized, value);
	}

	public void OnScavengingEncounterAnimatronicInitialized(string cpuId)
	{
		if (ScavengingEncounterAnimatronicInitialized != null)
		{
			ScavengingEncounterAnimatronicInitialized(cpuId);
		}
	}

	public void add_ScavengingEntitiesReceivedFromServer(global::System.Action<global::System.Collections.Generic.IEnumerable<ScavengingEntitySynchronizeableState>> value)
	{
		ScavengingEntitiesReceivedFromServer = (global::System.Action<global::System.Collections.Generic.IEnumerable<ScavengingEntitySynchronizeableState>>)global::System.Delegate.Combine(ScavengingEntitiesReceivedFromServer, value);
	}

	public void remove_ScavengingEntitiesReceivedFromServer(global::System.Action<global::System.Collections.Generic.IEnumerable<ScavengingEntitySynchronizeableState>> value)
	{
		ScavengingEntitiesReceivedFromServer = (global::System.Action<global::System.Collections.Generic.IEnumerable<ScavengingEntitySynchronizeableState>>)global::System.Delegate.Remove(ScavengingEntitiesReceivedFromServer, value);
	}

	public void OnScavengingEntitiesReceivedFromServer(global::System.Collections.Generic.IEnumerable<ScavengingEntitySynchronizeableState> e)
	{
		global::UnityEngine.Debug.Log("GOT Scavenging ENTITIES FROM SERVER! IT WORKS!");
		foreach (ScavengingEntitySynchronizeableState item in e)
		{
			global::UnityEngine.Debug.Log("SCAVENGE SUIT - " + item.parts["PlushSuit"]);
			global::UnityEngine.Debug.Log("SCAVENGE CPU - " + item.parts["Cpu"]);
		}
		if (ScavengingEntitiesReceivedFromServer != null)
		{
			ScavengingEntitiesReceivedFromServer(e);
		}
	}

	public void add_ScavengingEntitiesModelsUpdated(global::System.Action<global::System.Collections.Generic.IEnumerable<ScavengingEntity>> value)
	{
		ScavengingEntitiesModelsUpdated = (global::System.Action<global::System.Collections.Generic.IEnumerable<ScavengingEntity>>)global::System.Delegate.Combine(ScavengingEntitiesModelsUpdated, value);
	}

	public void remove_ScavengingEntitiesModelsUpdated(global::System.Action<global::System.Collections.Generic.IEnumerable<ScavengingEntity>> value)
	{
		ScavengingEntitiesModelsUpdated = (global::System.Action<global::System.Collections.Generic.IEnumerable<ScavengingEntity>>)global::System.Delegate.Remove(ScavengingEntitiesModelsUpdated, value);
	}

	public void OnScavengingEntitiesModelsUpdated(global::System.Collections.Generic.IEnumerable<ScavengingEntity> e)
	{
		if (ScavengingEntitiesModelsUpdated != null)
		{
			ScavengingEntitiesModelsUpdated(e);
		}
	}
}
