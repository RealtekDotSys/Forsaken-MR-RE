namespace SRDebugger.UI.Tabs
{
	public class InfoTabController : global::SRF.SRMonoBehaviourEx
	{
		public const char Tick = '✓';

		public const char Cross = '×';

		public const string NameColor = "#BCBCBC";

		private global::System.Collections.Generic.Dictionary<string, global::SRDebugger.UI.Controls.InfoBlock> _infoBlocks = new global::System.Collections.Generic.Dictionary<string, global::SRDebugger.UI.Controls.InfoBlock>();

		[global::SRF.RequiredField]
		public global::SRDebugger.UI.Controls.InfoBlock InfoBlockPrefab;

		[global::SRF.RequiredField]
		public global::UnityEngine.RectTransform LayoutContainer;

		protected override void OnEnable()
		{
			base.OnEnable();
			Refresh();
		}

		public void Refresh()
		{
			global::SRDebugger.Services.ISystemInformationService service = global::SRF.Service.SRServiceManager.GetService<global::SRDebugger.Services.ISystemInformationService>();
			foreach (string category in service.GetCategories())
			{
				if (!_infoBlocks.ContainsKey(category))
				{
					global::SRDebugger.UI.Controls.InfoBlock value = CreateBlock(category);
					_infoBlocks.Add(category, value);
				}
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::SRDebugger.UI.Controls.InfoBlock> infoBlock in _infoBlocks)
			{
				FillInfoBlock(infoBlock.Value, service.GetInfo(infoBlock.Key));
			}
		}

		private void FillInfoBlock(global::SRDebugger.UI.Controls.InfoBlock block, global::System.Collections.Generic.IList<global::SRDebugger.InfoEntry> info)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			int num = 0;
			foreach (global::SRDebugger.InfoEntry item in info)
			{
				if (item.Title.Length > num)
				{
					num = item.Title.Length;
				}
			}
			num += 2;
			bool flag = true;
			foreach (global::SRDebugger.InfoEntry item2 in info)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.Append("<color=");
				stringBuilder.Append("#BCBCBC");
				stringBuilder.Append(">");
				stringBuilder.Append(item2.Title);
				stringBuilder.Append(": ");
				stringBuilder.Append("</color>");
				for (int i = item2.Title.Length; i <= num; i++)
				{
					stringBuilder.Append(' ');
				}
				if (item2.Value is bool)
				{
					stringBuilder.Append(((bool)item2.Value) ? '✓' : '×');
				}
				else
				{
					stringBuilder.Append(item2.Value);
				}
			}
			block.Content.text = stringBuilder.ToString();
		}

		private global::SRDebugger.UI.Controls.InfoBlock CreateBlock(string title)
		{
			global::SRDebugger.UI.Controls.InfoBlock infoBlock = SRInstantiate.Instantiate(InfoBlockPrefab);
			infoBlock.Title.text = title;
			infoBlock.CachedTransform.SetParent(LayoutContainer, worldPositionStays: false);
			return infoBlock;
		}
	}
}
