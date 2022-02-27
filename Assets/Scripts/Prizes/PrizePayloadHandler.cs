/*
* AUTHOR : Steve Datz
* DESC :
*/

using System;
using UnityEngine;


namespace Prizes
{
	//NOTE: In a real game this class would be responsible for unpacking the prize and handing it out to whatever other classes
	public static class PrizePayloadHandler
	{
		
		public static void IssuePrize(Prize prize)
		{

			bool result = prize switch
			{
				LifePrize lifePrize => HandleLifePrize(lifePrize),
				IntPrize intPrize => HandleIntPrize(intPrize),
				_ => throw new ArgumentOutOfRangeException()
			};
			
		}
	/************************************************************************************************************************/
		private static bool HandleIntPrize(IntPrize intPrize)
		{
			Debug.Log($"Earned <color=green>{intPrize.PrizeName}</color> x{intPrize.RewardAmount}");
			return true;
		}

		private static bool HandleLifePrize(LifePrize lifePrize)
		{
			Debug.Log($"Earned <color=green>{lifePrize.PrizeName}</color> +{lifePrize.GetLifeTimeChange} seconds");
			return true;
		}
	}
}
