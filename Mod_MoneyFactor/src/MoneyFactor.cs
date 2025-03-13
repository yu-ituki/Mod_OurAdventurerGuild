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
		// お金アイテムのIDらしい.
		static readonly int c_MoneyItemID = "6b6e6f54-99b6-4f02-a460-4c020cd222a2".GetHashCode();

		/// <summary>
		/// お金付与関数.
		/// 派遣依頼の報酬などはこちらで処理されるようだ.
		/// </summary>
		[HarmonyLib.HarmonyPrefix]
		[HarmonyLib.HarmonyPatch(typeof(HubManager), "AddGold")]
		static void _Prefix_AddGold(ref int amount) {
			int newAmount = UnityEngine.Mathf.CeilToInt(amount * Plugin.Instance.ModConfig.Factor.Value);
		//	DebugUtil.LogError($"!!!!! {amount} -> {newAmount}");
			amount = newAmount;
		}

		/// <summary>
		/// お金付与関数.
		/// クエスト終了時にもらえるものはこっちでアイテムとして処理されるらしい.
		/// </summary>
		[HarmonyLib.HarmonyPrefix]
		[HarmonyLib.HarmonyPatch(typeof(Database), "AddGuildItem")]
		static void _Prefix_AddGuildItem(BaseItem baseItem, ref int amount) {
			if (baseItem.ID.GetHashCode() != c_MoneyItemID)
				return;
			int newAmount = UnityEngine.Mathf.CeilToInt(amount * Plugin.Instance.ModConfig.Factor.Value);
		//	DebugUtil.LogError($"!!!!! {amount} -> {newAmount}");
			amount = newAmount;
		}


	}
}
