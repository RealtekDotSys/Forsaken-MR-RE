public class FazWrenchConduit : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Material offGlow;

	private global::UnityEngine.Animator animator;

	private MasterDomain masterDomain;

	private AudioPlayer audioPlayer;

	private global::UnityEngine.MeshRenderer rend;

	private bool justUsed;

	private bool givenReward;

	private void Start()
	{
		animator = GetComponent<global::UnityEngine.Animator>();
		rend = GetComponent<global::UnityEngine.MeshRenderer>();
		GetComponent<CustomAnimationEventScript>().AnimEvent += OnAnimationEvent;
		masterDomain = MasterDomain.GetDomain();
		audioPlayer = masterDomain.GameAudioDomain.AudioPlayer;
		if (global::UnityEngine.PlayerPrefs.GetInt("FazWrenchUsed") == 1)
		{
			animator.SetBool("PlayerPrefsUsed", value: true);
			global::UnityEngine.Material[] materials = new global::UnityEngine.Material[2]
			{
				rend.materials[0],
				offGlow
			};
			rend.materials = materials;
		}
	}

	private void OnMouseDown()
	{
		InteractFazWrench();
	}

	public void InteractFazWrench()
	{
		if (global::UnityEngine.PlayerPrefs.GetInt("FazWrenchUsed") == 1 || justUsed)
		{
			Fail();
			return;
		}
		TrophyInventory trophyInventory = masterDomain.WorkshopDomain.Inventory.TrophyInventory;
		if (trophyInventory.entries.ContainsKey("FazWrench") && trophyInventory.entries["FazWrench"] > 0)
		{
			Use();
		}
		else
		{
			Fail();
		}
	}

	private void Fail()
	{
		audioPlayer.RaiseGameEventForMode(AudioEventName.UIWorkshopAnimatronicLockedTapped, AudioMode.Global);
	}

	private void Use()
	{
		justUsed = true;
		global::UnityEngine.PlayerPrefs.SetInt("FazWrenchUsed", 1);
		animator.SetBool("ConduitUsed", value: true);
	}

	private void OnAnimationEvent(string eventId)
	{
		if (!(eventId == "ShatterGlass"))
		{
			if (eventId == "GiveReward" && !givenReward)
			{
				givenReward = true;
				masterDomain.ServerDomain.grantItemRequester.GrantPlayerItem("MXES_Mod");
			}
		}
		else
		{
			audioPlayer.RaiseGameEventForMode(AudioEventName.GlassShatter, AudioMode.Global);
			global::UnityEngine.Material[] materials = new global::UnityEngine.Material[2]
			{
				rend.materials[0],
				offGlow
			};
			rend.materials = materials;
		}
	}
}
