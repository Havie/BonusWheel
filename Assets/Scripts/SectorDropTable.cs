using System.Collections.Generic;
using System.Linq;
using Prizes;
using Unity.Mathematics;
using UnityEngine;


public class SectorDropTable
	{
		private readonly DropTableItem[] _dropTable = new DropTableItem[100];
		
		/************************************************************************************************************************/
		public SectorDropTable(IEnumerable<WheelSector> sectors)
		{
			GenerateDropTable(sectors);
		}
		/************************************************************************************************************************/

		/// <summary>
		/// Generates a prize based on pre-determined drop chances
		/// </summary>
		/// <returns>The prize from the sector</returns>
		public Prize GeneratePrize(out int sectorIndex)
		{
			int i = UnityEngine.Random.Range(0, _dropTable.Length);
			var dropTableItem = _dropTable[i];
			sectorIndex = dropTableItem.SectorIndex;
			//Debug.Log($"i={i} , sectorIndex={sectorIndex} , prize={dropTableItem.Prize.PrizeName}->{dropTableItem.Prize.GetUIRewardDisplay()}");
			return dropTableItem.Prize;
		}
		
		/************************************************************************************************************************/
		/// <summary>
		/// Maps the drop chances from the sectors to an index'd array 0-99
		/// for generating random numbers for
		/// </summary>
		/// <param name="sectors"></param>
		private void GenerateDropTable(IEnumerable<WheelSector> sectors)
		{
			int currUsedIndex = 0;
			int sectorCount = 0;
			foreach (var sector in sectors)
			{
				for (int i = 0; i < sector.DropChance; i++)
				{
					if (currUsedIndex >= _dropTable.Length)
					{
						Debug.Log($"<color=orange>Sectors drop table doesnt add to 100</color> Recheck sector settings");
						return;
					}
					_dropTable[currUsedIndex] = new DropTableItem(sectorCount, sector.Prize);
					//Debug.Log($"[<color=red>{currUsedIndex}</color>] = {sector.gameObject.name}");
					++currUsedIndex;
				}

				++sectorCount;
			}
		}

		private readonly struct DropTableItem
		{
			public readonly int SectorIndex;
			public readonly Prize Prize;
			
			public DropTableItem(int sectorIndex, Prize prize)
			{
				SectorIndex = sectorIndex;
				Prize = prize;
			}
		}
	}



