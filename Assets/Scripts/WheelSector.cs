/*
* AUTHOR : Steve Datz
* DESC :
*/

using System;
using Prizes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649 // Ignore : "Field is never assigned to, and will always have its default value"


	public class WheelSector : MonoBehaviour
	{
		[Header("Components")] 
		[SerializeField] private Image _icon;
		[SerializeField] private TextMeshProUGUI _text;
		[Header("Settings")]
		[Range(0,100)]
		[SerializeField] private float _dropChance;
		[SerializeField] private Prize _prize;
		/************************************************************************************************************************/
		private void Start()
		{
			SetDisplay();
		}
		

		/************************************************************************************************************************/
		public float DropChance => _dropChance;
		public Prize Prize => _prize;
		
		/************************************************************************************************************************/
		
		private void SetDisplay()
		{
			_icon.sprite = _prize.PrizeIcon;
			_text.text = $"{_prize.GetUIRewardDisplay()}";
		}

	}
	