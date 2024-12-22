using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PoE2TradeMacro.Parsing
{
    public class ParsedItem
    {
        public string? itemClass = null;
        public string? itemRarity = null;
        public string? itemName = null;
        public string? itemBaseType = null;
        public int? itemQuality = null;
        public int? itemLevel = null;

        public int? armourARMOUR = null;
        public int? armourEVASIONRATING = null;
        public int? armourENERGYSHIELD = null;
        public int? armourBLOCKCHANCE = null;

        public int? weaponPHYSICALDAMAGE = null;
        public int? weaponELEMENTALDAMAGE = null;
        public double? weaponCRITCHANCE = null;
        public double? weaponAPS = null;
        public double? weaponRELOADTIME = null;
        public int? weaponSPIRIT = null;

    }
}
