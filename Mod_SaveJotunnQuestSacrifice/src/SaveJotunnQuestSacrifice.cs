using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mod
{
	[HarmonyLib.HarmonyPatch]
	class SaveJotunnQuestSacrifice
	{
		const string c_TileEffName = "CaptiveSummonEffect";

		/// <summary>
		/// 各インタラクティブタイルの初期化時.
		/// </summary>
		/// <param name="__instance"></param>
		[HarmonyLib.HarmonyPrefix]
		[HarmonyLib.HarmonyPatch(typeof(BattleMapTileInteractable), "Awake")]
		static void _Prefix_BattleMapTileInteractable_Awake(BattleMapTileInteractable __instance) {
			// タイムリミット指定されている召喚効果が存在し、かつそれが.
			// ヨトゥンの捕虜だったら.
			// タイムリミットターン数を変更する.
			if (__instance.CurrentDuration <= 0)
				return;

			var limitTriggers = __instance.EffectsToTriggerIfTimeLimitPassed?.Value;
			if (limitTriggers.Length <= 0)
				return;
			var limitEff = limitTriggers[0];
			if (limitEff.name != c_TileEffName)
				return;	//< 泥臭いが名前解決する...

			// まあここまで来たら多分ヨトゥンの捕虜やろ.
			__instance.CurrentDuration = Plugin.Instance.ModConfig.Round.Value + 1;
			__instance.DecreaseDuration();	//< 表示更新のために無理やり呼び出し.
		}


	}
}
