using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PoE2TradeMacro.Util
{
    public static class Constants
    {

        
        //public static readonly List<string> itemRarity = new List<string>(["Normal", "Magic", "Rare", "Unique"]);
        //public static readonly List<string> itemRarity2 = new List<string>(["Currency"]);

        public static readonly List<string> Rarities = new List<string>(["Normal", "Magic", "Rare", "Unique"]);

        public const string RARITY_NORMAL = "Normal";
        public const string RARITY_MAGIC = "Magic";
        public const string RARITY_RARE = "Rare";
        public const string RARITY_UNIQUE = "Unique";
        public const string RARITY_CURRENCY = "Currency";
        public const string RARITY_GEM = "Gem";

        public const string MODPREFIX_ITEMCLASS = "Item Class: ";
        public const string MODPREFIX_RARITY = "Rarity: ";
        public const string MODPREFIX_QUALITY = "Quality: ";
        public const string MODPREFIX_ARMOUR = "Armour: ";
        public const string MODPREFIX_EVASIONRATING = "Evasion Rating: ";
        public const string MODPREFIX_ENERGYSHIELD = "Energy Shield: ";
        public const string MODPREFIX_PHYSICALDAMAGE = "Physical Damage: ";
        public const string MODPREFIX_LIGHTNINGDAMAGE = "Lightning Damage: ";
        public const string MODPREFIX_COLDDAMAGE = "Cold Damage: ";
        public const string MODPREFIX_FIREDAMAGE = "Fire Damage: ";
        public const string MODPREFIX_CHAOSDAMAGE = "Chaos Damage: ";
        public const string MODPREFIX_CRITCHANCE = "Critical Hit Chance: ";
        public const string MODPREFIX_APS = "Attacks per Second: ";
        public const string MODPREFIX_RELOADTIME = "Reload Time: ";
        public const string MODPREFIX_SPIRIT = "Spirit: ";
        public const string MODPREFIX_BLOCKCHANCE = "Block chance: ";
        public const string MODPREFIX_ITEMLEVEL = "Item Level: ";
        public const string MODPREFIX_SOCKETS = "Sockets: ";

        public const string MODSUFFIX_AUGMENTED = " (augmented)";
        public const string MODSUFFIX_RUNE = " (rune)";

        public const string ITEMBASE_WAYSTONE = "Waystone";
        public const string ITEMBASE_STACKABLECURRENCY = "Stackable Currency";

        

        
        public const string TESTING_ITEM_UniqueHelmet = "Item Class: Helmets\r\nRarity: Unique\r\nThe Three Dragons\r\nSolid Mask\r\n--------\r\nEvasion Rating: 134 (augmented)\r\nEnergy Shield: 54 (augmented)\r\n--------\r\nRequirements:\r\nLevel: 45\r\nDex: 46\r\nInt: 46\r\n--------\r\nItem Level: 68\r\n--------\r\n54% increased Evasion and Energy Shield\r\n+11% to all Elemental Resistances\r\nFire Damage from Hits Contributes to Shock Chance instead of Ignite Chance and Magnitude\r\nCold Damage from Hits Contributes to Ignite Chance and Magnitude instead of Chill Magnitude or Freeze Buildup\r\nLightning Damage from Hits Contributes to Freeze Buildup instead of Shock Chance\r\n--------\r\n\"The ice seared his naked feet\r\nAs the lightning stilled his heart,\r\nBut it was the flames upon his lover's face\r\nThat roused him to vengeance.\"\r\n- From 'The Three Dragons' by Victario of Sarn";
        public const string TESTING_ITEM_Waystone = "Item Class: Waystones\r\nRarity: Magic\r\nSwarming Waystone (Tier 3) of Toughness\r\n--------\r\nWaystone Tier: 3\r\nWaystone Drop Chance: +90% (augmented)\r\n--------\r\nItem Level: 71\r\n--------\r\n18% increased Magic Monsters\r\n22% increased number of Rare Monsters\r\n32% more Monster Life\r\n--------\r\nCan be used in a Map Device, allowing you to enter a Map. Waystones can only be used once.";
        public const string TESTING_ITEM_QualitySocketsBodyArmour = "Item Class: Body Armours\r\nRarity: Rare\r\nApocalypse Wrap\r\nIronclad Vestments\r\n--------\r\nQuality: +17% (augmented)\r\nArmour: 170 (augmented)\r\nEnergy Shield: 64 (augmented)\r\n--------\r\nRequirements:\r\nLevel: 54\r\nStr: 39\r\nInt: 39\r\n--------\r\nSockets: S S \r\n--------\r\nItem Level: 69\r\n--------\r\n30% increased Mana Regeneration Rate (rune)\r\n--------\r\n24% increased Armour and Energy Shield\r\n+55 to maximum Life\r\n+32 to Spirit\r\n+29 to Intelligence\r\n+21% to Fire Resistance\r\n+23% to Chaos Resistance";
        public const string TESTING_ITEM_MagicLifeFlaskWithQual = "Item Class: Life Flasks\r\nRarity: Magic\r\nGargantuan Life Flask of the Copious\r\n--------\r\nQuality: +20% (augmented)\r\nRecovers 852 (augmented) Life over 5 Seconds\r\nConsumes 10 of 101 (augmented) Charges on use\r\nCurrently has 0 Charges\r\n--------\r\nRequirements:\r\nLevel: 40\r\n--------\r\nItem Level: 41\r\n--------\r\n35% increased Charges\r\n--------\r\nRight click to drink. Can only hold charges while in belt. Refill at Wells or by killing monsters.";
        public const string TESTING_ITEM_Quarterstaff = "Item Class: Quarterstaves\r\nRarity: Rare\r\nWoe Bane\r\nAdvanced Slicing Quarterstaff\r\n--------\r\nPhysical Damage: 45-94\r\nLightning Damage: 5-71 (augmented)\r\nCritical Hit Chance: 10.00%\r\nAttacks per Second: 1.40\r\n--------\r\nRequirements:\r\nLevel: 59\r\nDex: 105 (unmet)\r\nInt: 42\r\n--------\r\nItem Level: 67\r\n--------\r\nAdds 5 to 71 Lightning Damage\r\n+29 to Intelligence\r\nCauses 28% increased Stun Buildup\r\n26% increased Stun Duration";
        public const string TESTING_ITEM_OHMace = "Item Class: One Hand Maces\r\nRarity: Magic\r\nSquire's Advanced Plated Mace\r\n--------\r\nPhysical Damage: 40-83 (augmented)\r\nCritical Hit Chance: 5.00%\r\nAttacks per Second: 1.40\r\n--------\r\nRequirements:\r\nLevel: 55\r\nStr: 126 (unmet)\r\n--------\r\nItem Level: 60\r\n--------\r\n18% increased Physical Damage\r\n+17 to Accuracy Rating\r\n";
        public const string TESTING_ITEM_Bow = "Item Class: Bows\r\nRarity: Magic\r\nArcing Advanced Dualstring Bow of Valour\r\n--------\r\nPhysical Damage: 29-54\r\nLightning Damage: 4-62 (augmented)\r\nCritical Hit Chance: 5.00%\r\nAttacks per Second: 1.20\r\n--------\r\nRequirements:\r\nLevel: 55\r\nDex: 126 (unmet)\r\n--------\r\nItem Level: 60\r\n--------\r\nBow Attacks fire an additional Arrow (implicit)\r\n--------\r\nAdds 4 to 62 Lightning Damage\r\nGain 48 Life per Enemy Killed\r\n";
        public const string TESTING_ITEM_Crossbow = "Item Class: Crossbows\r\nRarity: Magic\r\nReaver's Advanced Bombard Crossbow of Radiance\r\n--------\r\nPhysical Damage: 24-97 (augmented)\r\nCritical Hit Chance: 5.00%\r\nAttacks per Second: 1.65\r\nReload Time: 0.75\r\n--------\r\nRequirements:\r\nLevel: 59\r\nStr: 74 (unmet)\r\nDex: 74 (unmet)\r\n--------\r\nItem Level: 60\r\n--------\r\nGrenade Skills Fire an additional Projectile (implicit)\r\n--------\r\n26% increased Physical Damage\r\n+120 to Accuracy Rating\r\n15% increased Light Radius\r\n";
        public const string TESTING_ITEM_Quiver = "Item Class: Quivers\r\nRarity: Magic\r\nFrosted Two-Point Quiver of Conquest\r\n--------\r\nRequirements:\r\nLevel: 26\r\n--------\r\nItem Level: 60\r\n--------\r\n30% increased Accuracy Rating (implicit)\r\n--------\r\nAdds 2 to 4 Cold damage to Attacks\r\nGain 19 Life per Enemy Killed\r\n--------\r\nCan only be equipped if you are wielding a Bow.";
        public const string TESTING_ITEM_ArcGem = "Item Class: Skill Gems\r\nRarity: Gem\r\nArc\r\n--------\r\nSpell, Lightning, Chaining\r\nLevel: 15\r\nQuality: +20% (augmented)\r\n--------\r\nRequirements:\r\nLevel: 64\r\nInt: 146\r\n--------\r\nSockets: G G G G \r\n--------\r\nAn arc of Lightning stretches from the caster to a targeted enemy and Chains on to other nearby enemies.\r\n--------\r\nSkills can be managed in the Skills Panel.";


        public const string DELIMITER_filterGroup= "--------";
        public const string DELIMITER_filterGroupMods= "\r\n";



        public const string TRADE_Search = "https://www.pathofexile.com/trade2/search/poe2/Standard";
        public const string TRADE_API_Search = "https://www.pathofexile.com/api/trade2/search/poe2/Standard";
        public const string TRADE_API_Fetch = "https://www.pathofexile.com/api/trade2/fetch";
        public const string TRADE_API_Stats = "https://www.pathofexile.com/api/trade2/data/stats";
        public const string TRADE_API_Filters = "https://www.pathofexile.com/api/trade2/data/filters";
        public const string TRADE_API_Items = "https://www.pathofexile.com/api/trade2/data/items";
        public const string TRADE_API_Static = "https://www.pathofexile.com/api/trade2/data/static";

    }
}
