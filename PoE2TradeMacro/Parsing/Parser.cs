using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using PoE2TradeMacro.Util;


namespace PoE2TradeMacro.Parsing
{
    public static class Parser
    {

        public static readonly string testUniqueHelmetString = "Item Class: Helmets\r\nRarity: Unique\r\nThe Three Dragons\r\nSolid Mask\r\n--------\r\nEvasion Rating: 134 (augmented)\r\nEnergy Shield: 54 (augmented)\r\n--------\r\nRequirements:\r\nLevel: 45\r\nDex: 46\r\nInt: 46\r\n--------\r\nItem Level: 68\r\n--------\r\n54% increased Evasion and Energy Shield\r\n+11% to all Elemental Resistances\r\nFire Damage from Hits Contributes to Shock Chance instead of Ignite Chance and Magnitude\r\nCold Damage from Hits Contributes to Ignite Chance and Magnitude instead of Chill Magnitude\r\nLightning Damage from Hits Contributes to Freeze Buildup instead of Shock Chance\r\n--------\r\n\"The ice seared his naked feet\r\nAs the lightning stilled his heart,\r\nBut it was the flames upon his lover's face\r\nThat roused him to vengeance.\"\r\n- From 'The Three Dragons' by Victario of Sarn";

        public static readonly string testWaystoneString = "Item Class: Waystones\r\nRarity: Magic\r\nSwarming Waystone (Tier 3) of Toughness\r\n--------\r\nWaystone Tier: 3\r\nWaystone Drop Chance: +90% (augmented)\r\n--------\r\nItem Level: 71\r\n--------\r\n18% increased Magic Monsters\r\n22% increased number of Rare Monsters\r\n32% more Monster Life\r\n--------\r\nCan be used in a Map Device, allowing you to enter a Map. Waystones can only be used once.";

        public static List<List<string>> ParseItem(string item)
        {

            ParsedItem parsedItem = new ParsedItem();

            List<string> itemSections = ParseSections(item, Constants.filterGroupDelimiter);
            List<List<string>> itemContainer = new List<List<string>>();

            foreach (string section in itemSections)
            {
                List<string> subSections = ParseSections(section, Constants.filterGroupModsDelimiter);
                subSections.RemoveAll(s => s == string.Empty);
                itemContainer.Add(subSections);
            }
            try
            {
                parsedItem = ParseItemHeader(itemContainer[0], parsedItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
                        
            return itemContainer;
        }

        public static List<string> ParseSections(string item, string delimiter)
        {
            List<string> itemSections = new List<string>(item.Split(new string[] { delimiter }, StringSplitOptions.None));
            return itemSections;
        }


        //////////////////////////////////////////////////
        //                                              //
        // Parsing of various sections of an item below //
        //                                              //
        //////////////////////////////////////////////////


        // Pass the first section of the item to this function
        public static ParsedItem ParseItemHeader(List<string> itemSection, ParsedItem parsedItemCopy)
        {
            // Check if the first line of this section starts with "Item Class: ..." and exit early if not so
            if (!(itemSection[0].StartsWith(Constants.MODPREFIX_ITEMCLASS)))
            {
                throw new IncompleteParsingException("The current item section did not contain mod-entry 'Item Class: '");
            }

            // Check if the second line of this section starts with "Rarity: ..." and exit early if not so
            if (!(itemSection[1].StartsWith(Constants.MODPREFIX_RARITY)))
            {
                throw new IncompleteParsingException("The current item section did not contain mod-entry 'Rarity: '");
            }

            string itemClass = itemSection[0].Replace(Constants.MODPREFIX_ITEMCLASS, "");
            string itemRarity = itemSection[1].Replace(Constants.MODPREFIX_RARITY, "");
            string itemName = itemSection[2];
            // don't extract basetype as some items, such as currency doesn't have basetype

            parsedItemCopy.itemClass = itemClass;
            parsedItemCopy.itemRarity = itemRarity;
            parsedItemCopy.itemName = itemName;


            switch (itemRarity)
            {

                // Covering all equipment rarities. Since waytones also have 'equipment' rarities (see Constants), we handle those separately
                case string tempRarity when Constants.EquipmentRarities.Contains(tempRarity): // && itemClass != "Waystones":

                    if (itemClass == "Waystones")
                    {
                        parsedItemCopy.itemBaseType = Constants.ITEMBASE_WAYSTONE;
                        break;
                    }
                    else
                    {
                        parsedItemCopy.itemBaseType = itemSection[3];
                        break;
                    }

                default:
                    break;
            }

            return parsedItemCopy;


        }
    }
}
