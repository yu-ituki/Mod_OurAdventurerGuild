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
		public ConfigEntry<float> Factor { get; set; }

		public override void Initialize( ConfigFile config )
		{
			Factor = config.Bind( "General", "Factor", 1.5f, "multiplier for get money.");
		}
	}
}
