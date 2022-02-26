using TMPro;
using UnityEngine;

namespace Prizes
{
	public abstract class Prize : ScriptableObject
	{
		[field : SerializeField]
		public string PrizeName { get; private set; }
		[field : SerializeField]
		public Sprite PrizeIcon  { get; private set; }
		
		public abstract string GetUIRewardDisplay(bool isDebugLog=false);
	}
}
