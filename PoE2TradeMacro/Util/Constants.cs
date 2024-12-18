using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoE2TradeMacro.Util
{
    public static class Constants
    {

        
        //public static readonly List<string> itemRarity = new List<string>(["Normal", "Magic", "Rare", "Unique"]);
        //public static readonly List<string> itemRarity2 = new List<string>(["Currency"]);

        public static readonly List<string> EquipmentRarities = new List<string>(["Normal", "Magic", "Rare", "Unique"]);

        public static readonly string RARITY_NORMAL = "Normal";
        public static readonly string RARITY_MAGIC = "Magic";
        public static readonly string RARITY_RARE = "Rare";
        public static readonly string RARITY_UNIQUE = "Unique";
        public static readonly string RARITY_CURRENCY = "Currency";
        public static readonly string RARITY_GEM = "Gem";

        public static readonly string MODPREFIX_ITEMCLASS = "Item Class: ";
        public static readonly string MODPREFIX_RARITY = "Rarity: ";
        public static readonly string MODPREFIX_QUALITY = "Quality: ";
        public static readonly string MODPREFIX_ARMOUR = "Armour: ";
        public static readonly string MODPREFIX_EVASIONRATING = "Evasion Rating: ";
        public static readonly string MODPREFIX_ENERGYSHIELD = "Energy Shield: ";



        public static readonly string ITEMBASE_WAYSTONE = "Waystone";



        public static readonly string filterGroupDelimiter = "--------";
        public static readonly string filterGroupModsDelimiter = "\r\n";



    }
}
