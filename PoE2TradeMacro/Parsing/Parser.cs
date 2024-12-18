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

        public static List<List<string>> ParseItem(string item)
        {

            ParsedItemReturnContainer parsedItemReturnContainer = new ParsedItemReturnContainer();
            
            List<List<string>> itemContainer = ParseItemIntoSections(item);

            parsedItemReturnContainer = ParseArmour(itemContainer[1], parsedItemReturnContainer);

            return itemContainer;
        }


        public static List<List<string>> ParseItemIntoSections(string item)
        {
            List<List<string>> itemContainer = new List<List<string>>();

            List<string> itemSections = ParseSections(item, Constants.DELIMITER_filterGroup);

            foreach (string section in itemSections)
            {
                List<string> subSections = ParseSections(section, Constants.DELIMITER_filterGroupMods);
                subSections.RemoveAll(s => s == string.Empty);
                itemContainer.Add(subSections);
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
        public static ParsedItemReturnContainer ParseItemHeader(List<string> itemSection, ParsedItemReturnContainer parsedItemReturnContainer)
        {

            //ParsedItemReturnContainer parsedItemReturnContainer = new ParsedItemReturnContainer();

            // Check if the first line of this section starts with "Item Class: ..." and exit early if not so
            if (!(itemSection[0].StartsWith(Constants.MODPREFIX_ITEMCLASS)))
            {
                //parsedItemReturnContainer.parsedItemCopy = parsedItemCopy
                parsedItemReturnContainer.parseStatus = false;
                return parsedItemReturnContainer;
            }

            // Check if the second line of this section starts with "Rarity: ..." and exit early if not so
            if (!(itemSection[1].StartsWith(Constants.MODPREFIX_RARITY)))
            {

                //parsedItemReturnContainer.parsedItemCopy = parsedItemCopy;
                parsedItemReturnContainer.parseStatus = false;
                return parsedItemReturnContainer;

            }

            string itemClass = itemSection[0].Replace(Constants.MODPREFIX_ITEMCLASS, "");
            string itemRarity = itemSection[1].Replace(Constants.MODPREFIX_RARITY, "");
            string itemName = itemSection[2];
            // don't extract basetype as some items, such as currency doesn't have basetype

            //parsedItemCopy.itemClass = itemClass;
            //parsedItemCopy.itemRarity = itemRarity;
            //parsedItemCopy.itemName = itemName;


            parsedItemReturnContainer.parsedItemCopy.itemClass = itemClass;
            parsedItemReturnContainer.parsedItemCopy.itemRarity = itemRarity;
            parsedItemReturnContainer.parsedItemCopy.itemName = itemName;


            switch (itemRarity)
            {

                // Covering all equipment rarities. Since waytones also have 'equipment' rarities (see Constants), we handle those separately
                case string tempRarity when Constants.Rarities.Contains(tempRarity): // && itemClass != "Waystones":

                    if (itemClass == "Waystones")
                    {
                        parsedItemReturnContainer.parsedItemCopy.itemBaseType = Constants.ITEMBASE_WAYSTONE;
                        break;
                    }
                    else
                    {
                        parsedItemReturnContainer.parsedItemCopy.itemBaseType = itemSection[3];
                        break;
                    }

                // NOTE: We probably need to add more cases at some point. 

                case Constants.RARITY_CURRENCY:
                    parsedItemReturnContainer.parsedItemCopy.itemBaseType = Constants.ITEMBASE_STACKABLECURRENCY;
                    break;

                default:
                    break;
            }

            //parsedItemReturnContainer.parsedItemCopy = parsedItemCopy;
            
            parsedItemReturnContainer.parseStatus = true;
            return parsedItemReturnContainer;
            

        }

        // For now, this function only covers Armour, Evasation Rating and Enery Shield
        public static ParsedItemReturnContainer ParseArmour(List<string> itemSection, ParsedItemReturnContainer parsedItemReturnContainer)
        {
            foreach (string modEntry in itemSection)
            {
                if (modEntry.StartsWith(Constants.MODPREFIX_ARMOUR)){

                    int armourARMOUR;
                    int.TryParse(Helper.RemoveAll(modEntry, 
                                                    [
                                                    Constants.MODPREFIX_ARMOUR,
                                                    Constants.MODSUFFIX_AUGMENTED
                                                    ]), out armourARMOUR);

                    parsedItemReturnContainer.parsedItemCopy.armourARMOUR = armourARMOUR;
                    parsedItemReturnContainer.parseStatus = true;
                    continue;
                }

                if (modEntry.StartsWith(Constants.MODPREFIX_EVASIONRATING))
                {
                    int armourEVASIONRATING;
                    int.TryParse(Helper.RemoveAll(modEntry,
                                                    [
                                                    Constants.MODPREFIX_EVASIONRATING,
                                                    Constants.MODSUFFIX_AUGMENTED
                                                    ]), out armourEVASIONRATING);

                    parsedItemReturnContainer.parsedItemCopy.armourEVASIONRATING = armourEVASIONRATING;
                    parsedItemReturnContainer.parseStatus = true;
                    continue;
                }

                if (modEntry.StartsWith(Constants.MODPREFIX_ENERGYSHIELD))
                {
                    int armourENERGYSHIELD;
                    int.TryParse(Helper.RemoveAll(modEntry,
                                                    [
                                                    Constants.MODPREFIX_ENERGYSHIELD,
                                                    Constants.MODSUFFIX_AUGMENTED
                                                    ]), out armourENERGYSHIELD);

                    parsedItemReturnContainer.parsedItemCopy.armourENERGYSHIELD = armourENERGYSHIELD;
                    parsedItemReturnContainer.parseStatus = true;
                    continue;
                }
                //TODO: Add more implicits
            }

            if (parsedItemReturnContainer.parseStatus)
            {
                parsedItemReturnContainer = ParseItemQuality(itemSection, parsedItemReturnContainer);
            }

            return parsedItemReturnContainer;
        }

        // This takes a ParsedItemReturnContainer instead of a ParsedItem because it is only called from within other parsers
        public static ParsedItemReturnContainer ParseItemQuality(List<string> itemSection, ParsedItemReturnContainer parsedItemReturnContainer)   //ParsedItem parsedItemCopy)
        {            
            foreach (string modEntry in itemSection)
            {
                if (modEntry.StartsWith(Constants.MODPREFIX_QUALITY))
                {

                    int itemQuality;
                    if (!(int.TryParse(Helper.RemoveAll(modEntry,
                                                            [
                                                            Constants.MODPREFIX_QUALITY,
                                                            Constants.MODSUFFIX_AUGMENTED,
                                                            "+",
                                                            "%"
                                                            ]), out itemQuality)))
                    {
                        // Since this parser is called from within other parsers, it should not set the 
                        //  parserStatus to anything, since it is only called if the previous parser has a 
                        //  parserStatus = true
                        return parsedItemReturnContainer;
                    }

                    parsedItemReturnContainer.parsedItemCopy.itemQuality = itemQuality;
                    return parsedItemReturnContainer;
                }
            }

            return parsedItemReturnContainer;



        }
    }
}
