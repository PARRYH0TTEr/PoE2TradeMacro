using PoE2TradeMacro.Parsing;
using PoE2TradeMacro.Util;

namespace PoE2TradeMacro.Tests
{
    public class ParserTests
    {

        [Theory]
        [InlineData(Constants.TESTING_QualitySocketsBodyArmour, new string[] {"Body Armours", "Rare"})]
        [InlineData(Constants.TESTING_UniqueHelmet, new string[] {"Helmets", "Unique"})]
        public void Parse_ItemHeader(string item, string[] expectedParsedItemFieldValues)
        {
            ParsedItemReturnContainer parsedItemReturnContainer = new ParsedItemReturnContainer();
            string[] expectedParsedItemFieldStrings = new string[] { "itemClass", "itemRarity" };

            List<List<string>> itemContainer = Parser.ParseItemIntoSections(item);

            parsedItemReturnContainer = Parser.ParseItemHeader(itemContainer[0], parsedItemReturnContainer);

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

        //NOTE: parseStatus is asserted to be false, since the item quality parser 'nested'
        //          ,however parsing of the quality modEntry should still work as normal
        [Theory]
        [InlineData(Constants.TESTING_QualitySocketsBodyArmour, 17)]
        [InlineData(Constants.TESTING_MagicLifeFlaskWithQual, 20)]
        //TODO: Add weapon with quality to the test here as well
        public void Parse_EquipmentItemWithQuality(string item, int expectedItemQuality)
        {
            //ParsedItem parsedItem = new ParsedItem();
            ParsedItemReturnContainer parsedItemReturnContainer = new ParsedItemReturnContainer();

            List<List<string>> itemContainer = Parser.ParseItemIntoSections(item);

            parsedItemReturnContainer = Parser.ParseItemQuality(itemContainer[1], parsedItemReturnContainer);

            Assert.False(parsedItemReturnContainer.parseStatus);
            Assert.Equal(parsedItemReturnContainer.parsedItemCopy.itemQuality, expectedItemQuality);
        }

        // The test is written as is, so we can always add more tests simply by decorating the test with another
        //      [InlineData...]
        [Theory]
        [InlineData(Constants.TESTING_UniqueHelmet, new string[] {"armourEVASIONRATING", "armourENERGYSHIELD"}, new int[] { 134, 54 })]
        [InlineData(Constants.TESTING_QualitySocketsBodyArmour, new string[] {"armourARMOUR", "armourENERGYSHIELD"}, new int[] { 170, 64 })]
        public void Parse_ArmourImplicits(string item, string[] expectedParsedItemFieldStrings, int[] expectedParsedItemFieldValues)
        {
            ParsedItemReturnContainer parsedItemReturnContainer = new ParsedItemReturnContainer();

            List<List<string>> itemContainer = Parser.ParseItemIntoSections(item);

            parsedItemReturnContainer = Parser.ParseArmour(itemContainer[1], parsedItemReturnContainer);

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