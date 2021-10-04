using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MusicBoxSlot
{
	public class MusicBoxSlot : Mod {
    }

	public class MusixBoxSlot : ModAccessorySlot
    {
		public override bool DrawDyeSlot => false;

		public override string VanityBackgroundTexture => "Terraria/Images/Inventory_Back13"; // white
		public override string FunctionalBackgroundTexture => "Terraria/Images/Inventory_Back7"; // pale blue

		public override string VanityTexture => "Terraria/Images/Item_" + ItemID.MusicBoxBoss2;
		public override string FunctionalTexture => "Terraria/Images/Item_" + ItemID.MusicBoxBoss2;

		public override void OnMouseHover(AccessorySlotType context)
		{
			// We will modify the hover text while an item is not in the slot, so that it says "Music Box".
			switch (context)
			{
				case AccessorySlotType.FunctionalSlot:
					Main.hoverItemName = "Music Box"; //TODO: Localiztion?
					break;
				case AccessorySlotType.VanitySlot:
					Main.hoverItemName = "Vanity Music Box"; //TODO: Localiztion?
					break;
			}
		}
		
		public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
		{
			bool result = false;
			
			var cache1 = Main.musicBox2;
			Main.musicBox2 = -1;

			var a = typeof(ModLoader).Assembly.GetType("Terraria.Player").GetMethod("ApplyMusicBox", BindingFlags.Instance | BindingFlags.NonPublic);
			a.Invoke(Main.LocalPlayer, new object[1] { item });

			if (Main.musicBox2 >= 0)
				result = true;

			Main.musicBox2 = cache1;

			return result;
		}
	}
}