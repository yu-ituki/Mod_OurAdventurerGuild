using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mod
{
	[HarmonyLib.HarmonyPatch]
	class MoneyFactor
	{
		/// <summary>
		/// キャラの経験値付与関数.
		/// </summary>
		/// <param name="__instance"></param>
		[HarmonyLib.HarmonyPrefix]
		[HarmonyLib.HarmonyPatch(typeof(HubManager), "AddGold")]
		static void _Prefix_AddGold(ref int amount) {
			amount = UnityEngine.Mathf.CeilToInt(amount * Plugin.Instance.ModConfig.Factor.Value);
		}


	}
}
