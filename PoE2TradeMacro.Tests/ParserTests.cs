using System.Xml.Serialization;
using PoE2TradeMacro.Parsing;
using PoE2TradeMacro.Parsing.Types;
using PoE2TradeMacro.Util;

namespace PoE2TradeMacro.Tests
{
    public class ParserTests
    {

        [Theory]
        [InlineData(Constants.TESTING_ITEM_QualitySocketsBodyArmour, new string[] { "itemClass", "itemRarity" }, new object[] { "Body Armours", "Rare" })]
        [InlineData(Constants.TESTING_ITEM_UniqueHelmet, new string[] { "itemClass", "itemRarity" }, new object[] { "Helmets", "Unique" })]
        public void Parse_ItemHeader(string item, string[] expectedParsedItemFieldStrings ,object[] expectedParsedItemFieldValues)
        {
            ParsedItemReturnContainer parsedItemReturnContainer = new ParsedItemReturnContainer();

            List<List<string>> itemContainer = Parser.ParseItemIntoSections(item);

            foreach (List<string> itemSection in itemContainer)
            {
                parsedItemReturnContainer = Parser.ParseItemHeader(itemSection, parsedItemReturnContainer);
            }

            Assert.True(parsedItemReturnContainer.parseStatus);
            
            for (int i = 0; i < expectedParsedItemFieldStrings.Length; i++)
            {
                Assert.Equal(expectedParsedItemFieldValues[i],

                        parsedItemReturnContainer.parsedItemCopy.GetType()
                                    .GetField(expectedParsedItemFieldStrings[i])?
                                    .GetValue(parsedItemReturnContainer.parsedItemCopy)

                    );
            }

        }

        //NOTE: parseStatus is asserted to be false, since the item quality parser is 'nested'
        //          ,however parsing of the quality modEntry should still work as normal
        [Theory]
        [InlineData(Constants.TESTING_ITEM_QualitySocketsBodyArmour, 17)]
        [InlineData(Constants.TESTING_ITEM_MagicLifeFlaskWithQual, 20)]
        //TODO: Add weapon with quality to the test here as well
        public void Parse_EquipmentItemWithQuality(string item, int expectedItemQuality)
        {
            ParsedItemReturnContainer parsedItemReturnContainer = new ParsedItemReturnContainer();

            List<List<string>> itemContainer = Parser.ParseItemIntoSections(item);

            foreach (List<string> itemSection in itemContainer)
            {
                parsedItemReturnContainer = Parser.ParseItemQuality(itemSection, parsedItemReturnContainer);
            }


            Assert.False(parsedItemReturnContainer.parseStatus);
            Assert.Equal(parsedItemReturnContainer.parsedItemCopy.itemQuality, expectedItemQuality);
        }

        // The test is written as is, so we can always add more tests simply by decorating the test with another
        //      [InlineData...]
        [Theory]
        [InlineData(Constants.TESTING_ITEM_UniqueHelmet, new string[] { "armourEVASIONRATING", "armourENERGYSHIELD" }, new int[] { 134, 54 })]
        [InlineData(Constants.TESTING_ITEM_QualitySocketsBodyArmour, new string[] { "armourARMOUR", "armourENERGYSHIELD" }, new int[] { 170, 64 })]
        public void Parse_Armour(string item, string[] expectedParsedItemFieldStrings, int[] expectedParsedItemFieldValues)
        {
            ParsedItemReturnContainer parsedItemReturnContainer = new ParsedItemReturnContainer();

            List<List<string>> itemContainer = Parser.ParseItemIntoSections(item);

            foreach (List<string> itemSection in itemContainer)
            {
                parsedItemReturnContainer = Parser.ParseArmour(itemSection, parsedItemReturnContainer);
            }

            Assert.True(parsedItemReturnContainer.parseStatus);

            for (int i = 0; i < expectedParsedItemFieldStrings.Length; i++)
            {
                Assert.Equal(expectedParsedItemFieldValues[i],
                        
                        parsedItemReturnContainer.parsedItemCopy.GetType()
                                    .GetField(expectedParsedItemFieldStrings[i])?
                                    .GetValue(parsedItemReturnContainer.parsedItemCopy)

                    );
            }
        }

        [Theory]
        [InlineData(Constants.TESTING_ITEM_QualitySocketsBodyArmour, new string[] { "itemLevel" }, new int[] { 69 })]
        [InlineData(Constants.TESTING_ITEM_UniqueHelmet, new string[] { "itemLevel" }, new int[] { 68 })]
        public void Parse_ItemLevel(string item, string[] expectedParsedItemFieldStrings, int[] expectedParsedItemFieldValues)
        {
            ParsedItemReturnContainer parsedItemReturnContainer = new ParsedItemReturnContainer();

            List<List<string>> itemContainer = Parser.ParseItemIntoSections(item);

            foreach (List<string> itemSection in itemContainer)
            {
                parsedItemReturnContainer = Parser.ParseItemLevel(itemSection, parsedItemReturnContainer);
            }

            Assert.True(parsedItemReturnContainer.parseStatus);

            for (int i = 0; i < expectedParsedItemFieldStrings.Length; i++)
            {
                Assert.Equal(expectedParsedItemFieldValues[i],

                        parsedItemReturnContainer.parsedItemCopy.GetType()
                                    .GetField(expectedParsedItemFieldStrings[i])?
                                    .GetValue(parsedItemReturnContainer.parsedItemCopy)

                    );
            }
        }

        [Theory]
        [InlineData(Constants.TESTING_ITEM_Quarterstaff, new string[] { "weaponPHYSICALDAMAGE", "weaponELEMENTALDAMAGE", "weaponCRITCHANCE", "weaponAPS" }, new object[] { 69, 38, 10.00, 1.4 })]
        [InlineData(Constants.TESTING_ITEM_OHMace, new string[] { "weaponPHYSICALDAMAGE", "weaponCRITCHANCE", "weaponAPS" }, new object[] { 61, 5.0, 1.4 })]
        [InlineData(Constants.TESTING_ITEM_Bow, new string[] { "weaponPHYSICALDAMAGE", "weaponELEMENTALDAMAGE", "weaponCRITCHANCE", "weaponAPS" }, new object[] { 41, 33, 5.0, 1.2 })]
        [InlineData(Constants.TESTING_ITEM_Crossbow, new string[] { "weaponPHYSICALDAMAGE", "weaponCRITCHANCE", "weaponAPS", "weaponRELOADTIME" }, new object[] { 60, 5.0, 1.65, 0.75 })]
        public void Parse_Weapon(string item, string[] expectedParsedItemFieldStrings, object[] expectedParsedItemFieldValues)
        {
            ParsedItemReturnContainer parsedItemReturnContainer = new ParsedItemReturnContainer();

            List<List<string>> itemContainer = Parser.ParseItemIntoSections(item);

            foreach (List<string> itemSection in itemContainer)
            {
                parsedItemReturnContainer = Parser.ParseWeapon(itemSection, parsedItemReturnContainer);
            }

            Assert.True(parsedItemReturnContainer.parseStatus);

            for (int i = 0; i < expectedParsedItemFieldStrings.Length; i++)
            {

                // This might seem redundant, but we somehow have to get the type of the field based on the 
                //  current element in the expectedParsedItemFieldStrings[]

                // Check if the current field-type is a float and if so compare with a precision of ~4,
                //  separately from the rest of the assertions

                //var currentField = parsedItemReturnContainer.parsedItemCopy.GetType()
                //                    .GetField(expectedParsedItemFieldStrings[i])?
                //                    .GetValue(parsedItemReturnContainer.parsedItemCopy);


                var currentField = parsedItemReturnContainer.parsedItemCopy.GetType()
                                    .GetField(expectedParsedItemFieldStrings[i]);

                // Check if the current fieldtype is double. If so, do epsilon-close assert

                //Nullable.GetUnderlyingType(currentFieldType) == typeof(double);
                if (currentField?.FieldType == typeof(Nullable<double>))
                {
                    Assert.Equal(expected: (double)expectedParsedItemFieldValues[i],
                            actual: (double)currentField?.GetValue(parsedItemReturnContainer.parsedItemCopy)
                            
                            , 5);
                }
                else
                {
                    Assert.Equal(expectedParsedItemFieldValues[i],

                            currentField?.GetValue(parsedItemReturnContainer.parsedItemCopy)

                            );
                }
            }
        }

        [Theory]
        [InlineData(Constants.TESTING_ITEM_QualitySocketsBodyArmour, new string[] { "sockets" }, new object[] { 2 })]
        [InlineData(Constants.TESTING_ITEM_ArcGem, new string[] { "sockets" }, new object[] { 4 })]
        public void Parse_Sockets(string item, string[] expectedParsedItemFieldStrings, object[] expectedParsedItemFieldValues)
        {
            ParsedItemReturnContainer parsedItemReturnContainer = new ParsedItemReturnContainer();

            List<List<string>> itemContainer = Parser.ParseItemIntoSections(item);

            foreach (List<string> itemSection in itemContainer)
            {
                parsedItemReturnContainer = Parser.ParseSockets(itemSection, parsedItemReturnContainer);
            }

            Assert.True(parsedItemReturnContainer.parseStatus);

            for (int i = 0; i < expectedParsedItemFieldStrings.Length; i++)
            {
                Assert.Equal(expectedParsedItemFieldValues[i],

                        parsedItemReturnContainer.parsedItemCopy.GetType()
                                    .GetField(expectedParsedItemFieldStrings[i])?
                                    .GetValue(parsedItemReturnContainer.parsedItemCopy)
                        );
            }

        }

    }
}