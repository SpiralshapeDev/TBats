using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Tbats.Common.Configs
{
    public class ServerConfig : ModConfig
    {
        // ConfigScope.ClientSide should be used for client side, usually visual or audio tweaks.
        // ConfigScope.ServerSide should be used for basically everything else, including disabling items or changing NPC behaviors
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("BatDamage")]

        [Range(0, 32767)]
        [DefaultValue(47)]
        public int AdamantiteBatDamage;

        [Range(0, 32767)]
        [DefaultValue(61)]
        public int ChlorophyteBatDamage;

        [Range(0, 32767)]
        [DefaultValue(36)]
        public int CobaltBatDamage;

        [Range(0, 32767)]
        [DefaultValue(9)]
        public int CopperBatDamage;

        [Range(0, 32767)]
        [DefaultValue(15)]
        public int CrimtaneBatDamage;

        [Range(0, 32767)]
        [DefaultValue(15)]
        public int DemoniteBatDamage;

        [Range(0, 32767)]
        [DefaultValue(8)]
        public int EbonwoodBatDamage;

        [Range(0, 32767)]
        [DefaultValue(13)]
        public int GoldBatDamage;

        [Range(0, 32767)]
        [DefaultValue(58)]
        public int HallowedBatDamage;

        [Range(0, 32767)]
        [DefaultValue(10)]
        public int IronBatDamage;

        [Range(0, 32767)]
        [DefaultValue(10)]
        public int LeadBatDamage;

        [Range(0, 32767)]
        [DefaultValue(80)]
        public int LuminiteBatDamage;

        [Range(0, 32767)]
        [DefaultValue(8)]
        public int MahoganyWoodBatDamage;

        [Range(0, 32767)]
        [DefaultValue(20)]
        public int MeteorBatDamage;

        [Range(0, 32767)]
        [DefaultValue(120)]
        public int MidnightBatDamage;

        [Range(0, 32767)]
        [DefaultValue(23)]
        public int MoltenBatDamage;

        [Range(0, 32767)]
        [DefaultValue(42)]
        public int MythrilBatDamage;

        [Range(0, 32767)]
        [DefaultValue(42)]
        public int OrichalcumBatDamage;

        [Range(0, 32767)]
        [DefaultValue(36)]
        public int PalladiumBatDamage;

        [Range(0, 32767)]
        [DefaultValue(11.5f)]
        public int PlatinumBatDamage;

        [Range(0, 32767)]
        [DefaultValue(8)]
        public int ShadewoodBatDamage;

        [Range(0, 32767)]
        [DefaultValue(68)]
        public int ShroomiteBatDamage;

        [Range(0, 32767)]
        [DefaultValue(4)]
        public int SilkBatDamage;

        [Range(0, 32767)]
        [DefaultValue(13)]
        public int SilverBatDamage;

        [Range(0, 32767)]
        [DefaultValue(9)]
        public int TinBatDamage;

        [Range(0, 32767)]
        [DefaultValue(47)]
        public int TitaniumBatDamage;

        [Range(0, 32767)]
        [DefaultValue(11.5f)]
        public int TungstenBatDamage;

        [Range(0, 32767)]
        [DefaultValue(8)]
        public int WoodBatDamage;

        [Range(0, 32767)]
        [DefaultValue(7)]
        public int CactusBatDamage;

        [Range(0, 32767)]
        [DefaultValue(8)]
        public int BorealWoodBatDamage;

        [Range(0, 32767)]
        [DefaultValue(8)]
        public int PalmWoodBatDamage;

        [Range(0, 32767)]
        [DefaultValue(14)]
        public int AshWoodBatDamage;

        [Range(0, 32767)]
        [DefaultValue(8)]
        public int PearlwoodBatDamage;

        [Range(0, 32767)]
        [DefaultValue(7)]
        public int PumpkinBatDamage;

    }
}