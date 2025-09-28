public sealed class CPU_DATA
{
	public class Aggression
	{
		public int Min { get; set; }

		public int Max { get; set; }
	}

	public class AnimatronicNames
	{
		public string Default { get; set; }
	}

	public class AnimPlayerFacingText
	{
		public string CPUDescription { get; set; }
	}

	public class ArtAssets
	{
		public CPU_DATA.Audio Audio { get; set; }

		public CPU_DATA.UI UI { get; set; }
	}

	public class AttackEyes
	{
		public int Intensity { get; set; }

		public string HexColor { get; set; }
	}

	public class AttackLocalization
	{
		public string LossText { get; set; }
	}

	public class Audio
	{
		public string SoundBank { get; set; }
	}

	public class Entry
	{
		public string Logical { get; set; }

		public CPU_DATA.AnimatronicNames AnimatronicNames { get; set; }

		public string PlushSuitRef { get; set; }

		public int Condition { get; set; }

		public CPU_DATA.VisualSettings VisualSettings { get; set; }

		public CPU_DATA.Status Status { get; set; }

		public int Phantasm { get; set; }

		public CPU_DATA.Perception Perception { get; set; }

		public CPU_DATA.Aggression Aggression { get; set; }

		public string AttackProfile { get; set; }

		public string ScavengingAttackProfile { get; set; }

		public CPU_DATA.Rewards Rewards { get; set; }

		public CPU_DATA.AttackLocalization AttackLocalization { get; set; }

		public CPU_DATA.ArtAssets ArtAssets { get; set; }

		public string AlwaysShowIcon { get; set; }

		public int Order { get; set; }

		public CPU_DATA.SpawnByStreak SpawnByStreak { get; set; }

		public CPU_DATA.SpeedMPH SpeedMPH { get; set; }

		public CPU_DATA.StalkingTimers StalkingTimers { get; set; }

		public CPU_DATA.AnimPlayerFacingText AnimPlayerFacingText { get; set; }

		public CPU_DATA.MapNodes MapNodes { get; set; }

		public string ConcreteItemID { get; set; }

		public bool PlayerAcquirable { get; set; }

		public int SalvagerSpeedMPH { get; set; }

		public CPU_DATA.MultiAnimatronicConfig MultiAnimatronicConfig { get; set; }
	}

	public class Essence
	{
		public int Min { get; set; }

		public int Max { get; set; }
	}

	public class EssenceOnLose
	{
		public int Min { get; set; }

		public int Max { get; set; }
	}

	public class Functioning
	{
		public float Min { get; set; }

		public float Max { get; set; }
	}

	public class LookAtEyes
	{
		public int Intensity { get; set; }

		public string HexColor { get; set; }
	}

	public class LookAwayEyes
	{
		public int Intensity { get; set; }

		public string HexColor { get; set; }
	}

	public class MapNodes
	{
		public CPU_DATA.NodeTravelTime NodeTravelTime { get; set; }

		public CPU_DATA.NodePathMin NodePathMin { get; set; }
	}

	public class MapSpeedMPH
	{
		public int NotUpgraded { get; set; }
	}

	public class MultiAnimatronicConfig
	{
		public string CpuIds { get; set; }

		public string PlushsuitIds { get; set; }

		public string SelectionType { get; set; }

		public int SelectionCount { get; set; }

		public string IncludeCurrentCpu { get; set; }
	}

	public class NodePathMin
	{
		public int Min { get; set; }

		public int Max { get; set; }
	}

	public class NodeTravelTime
	{
		public int Min { get; set; }

		public int Max { get; set; }
	}

	public class Parts
	{
		public int Min { get; set; }

		public int Max { get; set; }
	}

	public class Perception
	{
		public int Min { get; set; }

		public int Max { get; set; }

		public int Increment { get; set; }
	}

	public class PvPAnimatronicsAggressor
	{
		public string LootStructureId { get; set; }

		public string EventLootStructureId { get; set; }

		public CPU_DATA.Parts Parts { get; set; }

		public CPU_DATA.Essence Essence { get; set; }
	}

	public class PvPAnimatronicsVictim
	{
		public string LootStructureId { get; set; }

		public string EventLootStructureId { get; set; }

		public CPU_DATA.Parts Parts { get; set; }

		public CPU_DATA.Essence Essence { get; set; }

		public double? ModDropChance { get; set; }

		public CPU_DATA.EssenceOnLose EssenceOnLose { get; set; }

		public double? PlushSuitDropChance { get; set; }

		public double? CPUDropChance { get; set; }
	}

	public class RankRangeByBestStreak
	{
		public int Min { get; set; }

		public int Max { get; set; }
	}

	public class RankRangeByCurrentStreak
	{
		public int Min { get; set; }

		public int Max { get; set; }
	}

	public class Rewards
	{
		public CPU_DATA.WanderingAnimatronics WanderingAnimatronics { get; set; }

		public CPU_DATA.PvPAnimatronicsVictim PvPAnimatronicsVictim { get; set; }

		public CPU_DATA.PvPAnimatronicsAggressor PvPAnimatronicsAggressor { get; set; }
	}

	public class Root
	{
		public global::System.Collections.Generic.List<CPU_DATA.Entry> Entries { get; set; }
	}

	public class SpawnByStreak
	{
		public CPU_DATA.RankRangeByBestStreak RankRangeByBestStreak { get; set; }

		public CPU_DATA.RankRangeByCurrentStreak RankRangeByCurrentStreak { get; set; }
	}

	public class SpeedMPH
	{
		public int TravelSpeedMPH { get; set; }

		public CPU_DATA.MapSpeedMPH MapSpeedMPH { get; set; }
	}

	public class StalkingTimers
	{
		public CPU_DATA.Functioning Functioning { get; set; }
	}

	public class Status
	{
		public string LiveState { get; set; }

		public global::System.DateTime AvailableDate { get; set; }
	}

	public class UI
	{
		public string CpuIcon { get; set; }

		public string CpuSilhouetteIcon { get; set; }

		public string CpuIconFlat { get; set; }
	}

	public class VisualSettings
	{
		public int Condition { get; set; }

		public CPU_DATA.AttackEyes AttackEyes { get; set; }

		public CPU_DATA.LookAtEyes LookAtEyes { get; set; }

		public CPU_DATA.LookAwayEyes LookAwayEyes { get; set; }
	}

	public class WanderingAnimatronics
	{
		public CPU_DATA.Essence Essence { get; set; }

		public CPU_DATA.EssenceOnLose EssenceOnLose { get; set; }

		public CPU_DATA.Parts Parts { get; set; }

		public string LootStructureId { get; set; }

		public string EventLootStructureId { get; set; }

		public string LoseStructureId { get; set; }

		public double ModDropChance { get; set; }

		public double PlushSuitDropChance { get; set; }

		public double CPUDropChance { get; set; }
	}
}
