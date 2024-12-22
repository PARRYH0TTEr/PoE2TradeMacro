﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using PoE2TradeMacro.Util;


namespace PoE2TradeMacro.Parsing
{
    public static class Parser
    {

        public static List<List<string>> ParseItem(string item)
        {

            ParsedItemReturnContainer parsedItemReturnContainer = new ParsedItemReturnContainer();
            
            List<List<string>> itemContainer = ParseItemIntoSections(item);

            parsedItemReturnContainer = ParseWeapon(itemContainer[1], parsedItemReturnContainer);

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

            // Check if the first line of this section starts with "Item Class: ..." and exit early if not so
            if (!(itemSection[0].StartsWith(Constants.MODPREFIX_ITEMCLASS)))
            {
                return parsedItemReturnContainer;
            }

            // Check if the second line of this section starts with "Rarity: ..." and exit early if not so
            if (!(itemSection[1].StartsWith(Constants.MODPREFIX_RARITY)))
            {
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
            
            parsedItemReturnContainer.parseStatus = true;
            return parsedItemReturnContainer;
        }

        // For now, this function only covers Armour, Evasion Rating and Energy Shield
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

                if (modEntry.StartsWith(Constants.MODPREFIX_BLOCKCHANCE))
                {
                    int armourBLOCKCHANCE;
                    int.TryParse(Helper.RemoveAll(modEntry,
                                                    [
                                                    Constants.MODPREFIX_BLOCKCHANCE,
                                                    Constants.MODSUFFIX_AUGMENTED
                                                    ]), out armourBLOCKCHANCE);

                    parsedItemReturnContainer.parsedItemCopy.armourBLOCKCHANCE = armourBLOCKCHANCE;
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

        public static ParsedItemReturnContainer ParseItemLevel(List<string> itemSection, ParsedItemReturnContainer parsedItemReturnContainer)
        {
            // Even though it seems that item level is in its own section, 
            //  we'll still loop over the entries in the section for now.
            foreach (string modEntry in itemSection)
            {
                if (modEntry.StartsWith(Constants.MODPREFIX_ITEMLEVEL))
                {
                    int itemLevel;
                    int.TryParse(Helper.RemoveAll(modEntry,
                                                    [
                                                        Constants.MODPREFIX_ITEMLEVEL
                                                    ]), out itemLevel);

                    parsedItemReturnContainer.parsedItemCopy.itemLevel = itemLevel;
                    parsedItemReturnContainer.parseStatus = true;
                    continue;
                }
            }
            return parsedItemReturnContainer;
        }
    
        public static ParsedItemReturnContainer ParseWeapon(List<string> itemSection, ParsedItemReturnContainer parsedItemReturnContainer)
        {
            bool hasElementalDamage = false;
            int elementalDamageAccum = 0;

            foreach (string modEntry in itemSection)
            {
                if (modEntry.StartsWith(Constants.MODPREFIX_PHYSICALDAMAGE))
                {
                    string physicalDamageInterval = Helper.RemoveAll(modEntry,
                                                    [
                                                        Constants.MODPREFIX_PHYSICALDAMAGE,
                                                        Constants.MODSUFFIX_AUGMENTED
                                                    ]);

                    int physicalDamage = Helper.GetAvgDamage(physicalDamageInterval);

                    parsedItemReturnContainer.parsedItemCopy.weaponPHYSICALDAMAGE = physicalDamage;
                    parsedItemReturnContainer.parseStatus = true;
                    continue;
                }

                if (modEntry.StartsWith(Constants.MODPREFIX_LIGHTNINGDAMAGE))
                {
                    string lightningDamageInterval = Helper.RemoveAll(modEntry,
                                                     [
                                                        Constants.MODPREFIX_LIGHTNINGDAMAGE,
                                                        Constants.MODSUFFIX_AUGMENTED
                                                     ]);

                    elementalDamageAccum += Helper.GetAvgDamage(lightningDamageInterval);
                    hasElementalDamage = true;
                    continue;
                }

                if (modEntry.StartsWith(Constants.MODPREFIX_COLDDAMAGE))
                {
                    string coldDamageInterval = Helper.RemoveAll(modEntry,
                                                [
                                                    Constants.MODPREFIX_COLDDAMAGE,
                                                    Constants.MODSUFFIX_AUGMENTED
                                                ]);

                    elementalDamageAccum += Helper.GetAvgDamage(coldDamageInterval);
                    hasElementalDamage = true;
                    continue;
                }

                if (modEntry.StartsWith(Constants.MODPREFIX_FIREDAMAGE))
                {
                    string fireDamageInterval = Helper.RemoveAll(modEntry,
                                                                    [
                                                                        Constants.MODPREFIX_FIREDAMAGE,
                                                                        Constants.MODSUFFIX_AUGMENTED
                                                                    ]);

                    elementalDamageAccum += Helper.GetAvgDamage(fireDamageInterval);
                    hasElementalDamage = true;
                    continue;
                }

                if (modEntry.StartsWith(Constants.MODPREFIX_CRITCHANCE))
                {
                    float critChance;
                    float.TryParse(Helper.RemoveAll(modEntry,
                                                        [
                                                            Constants.MODPREFIX_CRITCHANCE,
                                                            Constants.MODSUFFIX_AUGMENTED,
                                                            "%"
                                                        ]), out critChance);

                    parsedItemReturnContainer.parsedItemCopy.weaponCRITCHANCE = critChance;
                    parsedItemReturnContainer.parseStatus = true;
                    continue;
                }

                if (modEntry.StartsWith(Constants.MODPREFIX_APS))
                {
                    float APS;
                    float.TryParse(Helper.RemoveAll(modEntry, 
                                                        [
                                                            Constants.MODPREFIX_APS,
                                                            Constants.MODSUFFIX_AUGMENTED
                                                        ]), out APS);

                    parsedItemReturnContainer.parsedItemCopy.weaponAPS = APS;
                    parsedItemReturnContainer.parseStatus = true;
                    continue;
                }

                if (modEntry.StartsWith(Constants.MODPREFIX_RELOADTIME))
                {
                    float reloadTime;
                    float.TryParse(Helper.RemoveAll(modEntry,
                                                        [
                                                            Constants.MODPREFIX_RELOADTIME
                                                        ]), out reloadTime);

                    parsedItemReturnContainer.parsedItemCopy.weaponRELOADTIME = reloadTime;
                    parsedItemReturnContainer.parseStatus = true;
                    continue;
                }

                if (modEntry.StartsWith(Constants.MODPREFIX_SPIRIT))
                {
                    int spirit;
                    int.TryParse(Helper.RemoveAll(modEntry,
                                                    [
                                                        Constants.MODPREFIX_SPIRIT,
                                                        Constants.MODSUFFIX_AUGMENTED
                                                    ]), out spirit);

                    parsedItemReturnContainer.parsedItemCopy.weaponSPIRIT = spirit;
                    parsedItemReturnContainer.parseStatus = true;
                    continue;
                }
            }

            if (hasElementalDamage)
            {
                parsedItemReturnContainer.parsedItemCopy.weaponELEMENTALDAMAGE = elementalDamageAccum;
                parsedItemReturnContainer.parseStatus = true;
            }

            if (parsedItemReturnContainer.parseStatus)
            {
                ParseItemQuality(itemSection, parsedItemReturnContainer);
            }

            return parsedItemReturnContainer;
        }
    }
}
