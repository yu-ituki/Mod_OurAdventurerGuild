using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mod
{
	[HarmonyLib.HarmonyPatch]
	class ExpFactor
	{
		/// <summary>
		/// キャラの経験値付与関数.
		/// </summary>
		/// <param name="__instance"></param>
		[HarmonyLib.HarmonyPrefix]
		[HarmonyLib.HarmonyPatch(typeof(Character), "AddExperiencePoints")]
		static void _Prefix_AddExperiencePoints(Character __instance, ref int exp, bool notify) {
			exp = UnityEngine.Mathf.CeilToInt(exp * Plugin.Instance.ModConfig.Factor.Value);
		}


	}
}
