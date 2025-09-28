public class AnimatronicBillboardView : global::UnityEngine.MonoBehaviour
{
	private const int _numBillboardToShow = 3;

	[global::UnityEngine.SerializeField]
	private BillboardIndicator[] _billboardIndicators;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject _billboardBg;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject _billboardBgImage;

	private global::System.Collections.Generic.List<string> _cpuIds;

	private float _showDuration;

	private float _hideDuration;

	private TextAnim[] _textAnims;

	private void Awake()
	{
		_cpuIds = new global::System.Collections.Generic.List<string>(new string[5] { "FreddyFazbear", "Ballora", "ToyFreddy", "Mangle", "Springtrap" });
		HideAllBillboards();
	}

	public void Init()
	{
		_showDuration = 1f;
		_hideDuration = 0.2f;
	}

	public void ShowBillboardsFor(string cpuId)
	{
		if (!_cpuIds.Contains(cpuId))
		{
			HideAllBillboards();
			return;
		}
		global::System.Collections.Generic.List<int> list = GenerateIndicesToShow(_cpuIds.IndexOf(cpuId), _cpuIds.Count, 3);
		list.Shuffle();
		StartCoroutine(ShowBillboardsAtIndexForSeconds(list, GenerateIndicesToShow(list.IndexOf(_cpuIds.IndexOf(cpuId)), _textAnims.Length, list.Count), _showDuration, _hideDuration));
	}

	private global::System.Collections.Generic.List<int> GenerateIndicesToShow(int correctIndex, int numSourceOptions, int numOptionsToChoose)
	{
		global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
		for (int i = 0; i < numSourceOptions; i++)
		{
			list.Add(i);
		}
		list.Remove(correctIndex);
		global::UnityEngine.Debug.Log(list.Count);
		global::UnityEngine.Debug.Log("Correct: " + correctIndex + " - SourceList: " + list[0] + " " + list[1]);
		global::System.Collections.Generic.List<int> list2 = GenerateNonRepeatingRandomElements(list, numOptionsToChoose - 1);
		list2.Add(correctIndex);
		return list2;
	}

	private global::System.Collections.IEnumerator ShowBillboardsAtIndexForSeconds(global::System.Collections.Generic.List<int> billboardIndices, global::System.Collections.Generic.List<int> animIndices, float showDuration, float hideDuration)
	{
		int loopIDX = 0;
		while (true)
		{
			_billboardBgImage.SetActive(value: false);
			ShowBillboardAtIndex(billboardIndices[loopIDX], animIndices[loopIDX]);
			yield return new global::UnityEngine.WaitForSeconds(showDuration);
			HideAllBillboards();
			loopIDX++;
			if (loopIDX >= billboardIndices.Count)
			{
				break;
			}
			_billboardBgImage.gameObject.SetActive(value: true);
			yield return new global::UnityEngine.WaitForSeconds(hideDuration);
		}
		_billboardBgImage.SetActive(value: false);
		yield return null;
	}

	private void ShowBillboardAtIndex(int billboardIndex, int animIndex)
	{
		if (_billboardIndicators.Length > billboardIndex && _textAnims.Length > animIndex)
		{
			BillboardIndicator obj = _billboardIndicators[billboardIndex];
			obj.gameObject.SetActive(value: true);
			_billboardBg.SetActive(value: true);
			obj.PlayAnimation(_textAnims[animIndex]);
		}
		else
		{
			HideAllBillboards();
		}
	}

	private void HideAllBillboards()
	{
		BillboardIndicator[] billboardIndicators = _billboardIndicators;
		for (int i = 0; i < billboardIndicators.Length; i++)
		{
			billboardIndicators[i].gameObject.SetActive(value: false);
		}
		_billboardBg.SetActive(value: false);
	}

	public AnimatronicBillboardView()
	{
		_showDuration = 1f;
		_hideDuration = 0.2f;
		_textAnims = new TextAnim[3]
		{
			TextAnim.Steady,
			TextAnim.Flashing,
			TextAnim.Jitter
		};
	}

	internal static global::System.Collections.Generic.List<int> GenerateNonRepeatingRandomElements(global::System.Collections.Generic.List<int> sourceList, int numToSelect)
	{
		int i = 0;
		int num = -1;
		global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
		for (; i < numToSelect; i++)
		{
			num = sourceList[global::UnityEngine.Random.Range(0, sourceList.Count)];
			list.Add(num);
			sourceList.Remove(num);
		}
		return list;
	}
}
