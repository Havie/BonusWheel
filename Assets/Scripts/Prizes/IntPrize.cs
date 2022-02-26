/*
* AUTHOR : Steve Datz
* DESC :
*/

using UnityEngine;

namespace Prizes
{
	[CreateAssetMenu(fileName = "_int_prize", menuName = "Prizes/Integer prize")]
	public class IntPrize : Prize
	{
		//NOTE: In a real game, with a robust item system, would make individual subtypes for each item category
		//for this test, just being able to separate by int vs time is enough
		
		[field : SerializeField]
		public int RewardAmount { get; private set; }
		
		
		public override string GetUIRewardDisplay()
		{
			return $"x{RewardAmount}";
		}
	}
}