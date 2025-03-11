using BepInEx.Configuration;

using System.Collections.Generic;

using UnityEngine;

namespace Mod
{
	/// <summary>
	/// Modコンフィグ用.
	/// </summary>
	public class ModConfig : ModConfigBase
	{
		public ConfigEntry<int> Round { get; set; }

		public override void Initialize( ConfigFile config )
		{
			Round = config.Bind( "General", "Round", 10, "Rounds until the Jotunn battle quest captive dies.");
		}
	}
}
