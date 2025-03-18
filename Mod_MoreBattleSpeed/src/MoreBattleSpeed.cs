using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Mod
{
	[HarmonyLib.HarmonyPatch]
	class MoreBattleSpeed
	{
		struct Info : IDisposable {
			public float m_Speed;
			public Sprite m_Sprite;

			public Info( string spritePath, float speed ) {
				m_Sprite = CommonUtil.LoadSprite(spritePath);
				m_Speed = speed;
			}

			public void Dispose() {
				CommonUtil.UnloadSprite(ref m_Sprite);
			}
			
		}

		static Info[] s_Infos;
		static int s_CurrentExSpeed;


		public static void Load() {
			s_Infos = new Info[] {
				new Info("x3.png", 3.0f),
				new Info("x5.png", 5.0f),
			//	new Info("x10.png", 10.0f),	//< なんか早すぎてアニメーションとか変になるからオミット.
			};
		}

		public static void Unload() {
			for (int i = 0; i < s_Infos.Length; i++) {
				s_Infos[i].Dispose();
			}
			s_Infos = null;
		}

		/// <summary>
		/// キャラの経験値付与関数.
		/// </summary>
		/// <param name="__instance"></param>
		[HarmonyLib.HarmonyPrefix]
		[HarmonyLib.HarmonyPatch(typeof(BattleManagerV2), "ToggleSpeed")]
		static bool _Prefix_ToggleSpeed(BattleManagerV2 __instance) {
			if (Time.timeScale <= 0f)
				return true;

			float currentScale = __instance.GlobalDatabase.BattleSpeedScale;
			if (CommonUtil.IsEqual(currentScale, 1f))
				return true;
			if (CommonUtil.IsEqual(currentScale, 1.5f))
				return true;
			if (CommonUtil.IsEqual(currentScale, s_Infos[s_Infos.Length - 1].m_Speed)) {
				s_CurrentExSpeed = 0;
				return true;
			}

			// 自身のやつに変える.
			__instance.GlobalSoundManager.PlayButtonClickedSound();
			__instance.GlobalDatabase.BattleSpeedScale = s_Infos[s_CurrentExSpeed].m_Speed;
			__instance.BattleSpeedIcon.sprite = s_Infos[s_CurrentExSpeed].m_Sprite;
			Time.timeScale = __instance.GlobalDatabase.BattleSpeedScale;
			++s_CurrentExSpeed;

			DebugUtil.LogWarning($"change ex speed : {__instance.GlobalDatabase.BattleSpeedScale}");
			return false; //< こっちの処理で乗っ取る.
		}

		/// <summary>
		/// UI再設定処理...
		/// </summary>
		/// <param name="__instance"></param>
		[HarmonyLib.HarmonyPostfix]
		[HarmonyLib.HarmonyPatch(typeof(BattleManagerV2), "UpdateBattleSpeedUI")]
		static void _Postfox_UpdateBattleSpeedUI(BattleManagerV2 __instance) {
			s_CurrentExSpeed = 0;
			float currentScale = __instance.GlobalDatabase.BattleSpeedScale;
			for ( int i = 0; i < s_Infos.Length; ++i ) {
				if (!CommonUtil.IsEqual(s_Infos[i].m_Speed, currentScale))
					continue;
				__instance.BattleSpeedIcon.sprite = s_Infos[i].m_Sprite;
				s_CurrentExSpeed = i;
				break;
			}
		}

	}
}
