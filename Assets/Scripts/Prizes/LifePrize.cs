/*
* AUTHOR : Steve Datz
* DESC :
*/

using Unity.Mathematics;
using UnityEngine;

namespace Prizes
{
	[CreateAssetMenu(fileName = "_life_prize", menuName = "Prizes/Life")]
	public class LifePrize : Prize
	{
		//NOTE: In a real game, with a robust item system, would make individual subtypes for each item category
		//for this test, just being able to separate by int vs time is enough
		public float GetLifeTimeChange => _lifeTimeChangeSeconds;
		[SerializeField] private float _lifeTimeChangeSeconds = 1800;

		private const float SEC_TO_MIN = 0.016666f;
		public override string GetUIRewardDisplay()
		{
			int minutes = Mathf.RoundToInt(_lifeTimeChangeSeconds * SEC_TO_MIN);
			return $"{minutes}<indent=50%>  <line-height=25%><size=40%>min</size></line-height></indent>";
			//return $"{1800 / 60} <i>min</i>";
		}
	}
}