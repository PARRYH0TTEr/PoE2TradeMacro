using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PoE2TradeMacro.Parsing.Types
{
    public class ParsedItem
    {
        public string? itemClass;
        public string? itemRarity;
        public string? itemName;
        public string? itemBaseType;
        public int? itemQuality;
        public int? itemLevel;

        public int? armourARMOUR;
        public int? armourEVASIONRATING;
        public int? armourENERGYSHIELD;
        public int? armourBLOCKCHANCE;

        public int? weaponPHYSICALDAMAGE;
        public int? weaponELEMENTALDAMAGE;
        public double? weaponCRITCHANCE;
        public double? weaponAPS;
        public double? weaponRELOADTIME;
        public int? weaponSPIRIT;

        public int? sockets;

        //public List<string> explicitModEntries;
        public List<AffixVals> explicitAffixEntries;


        public ParsedItem()
        {
            this.itemClass = null;
            this.itemRarity = null;
            this.itemName = null;
            this.itemBaseType = null;
            this.itemQuality = null;
            this.itemLevel = null;

            this.armourARMOUR = null;
            this.armourEVASIONRATING = null;
            this.armourENERGYSHIELD = null;
            this.armourBLOCKCHANCE = null;

            this.weaponPHYSICALDAMAGE = null;
            this.weaponELEMENTALDAMAGE = null;
            this.weaponCRITCHANCE = null;
            this.weaponAPS = null;
            this.weaponRELOADTIME = null;
            this.weaponSPIRIT = null;

            this.sockets = null;

            this.explicitAffixEntries = new List<AffixVals>();
        }

        public ParsedItem(ParsedItem other)
        {
            this.itemClass = other.itemClass;
            this.itemRarity = other.itemRarity;
            this.itemName = other.itemName;
            this.itemBaseType = other.itemBaseType;
            this.itemQuality = other.itemQuality;
            this.itemLevel = other.itemLevel;

            this.armourARMOUR = other.armourARMOUR;
            this.armourEVASIONRATING = other.armourEVASIONRATING;
            this.armourENERGYSHIELD = other.armourENERGYSHIELD;
            this.armourBLOCKCHANCE = other.armourBLOCKCHANCE;

            this.weaponPHYSICALDAMAGE = other.weaponPHYSICALDAMAGE;
            this.weaponELEMENTALDAMAGE = other.weaponELEMENTALDAMAGE;
            this.weaponCRITCHANCE = other.weaponCRITCHANCE;
            this.weaponAPS = other.weaponAPS;
            this.weaponRELOADTIME = other.weaponRELOADTIME;
            this.weaponSPIRIT = other.weaponSPIRIT;

            this.sockets = other.sockets;

            this.explicitAffixEntries = new List<AffixVals>(other.explicitAffixEntries);
        }
    }
}
