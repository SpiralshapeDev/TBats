using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
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
        [DefaultValue(153)]
        [ReloadRequired]
        public int AdamantiteBatDamage;

        [Range(0, 32767)]
        [DefaultValue(238)]
        [ReloadRequired]
        public int ChlorophyteBatDamage;

        [Range(0, 32767)]
        [DefaultValue(100)]
        [ReloadRequired]
        public int CobaltBatDamage;

        [Range(0, 32767)]
        [DefaultValue(25)]
        [ReloadRequired]
        public int CopperBatDamage;

        [Range(0, 32767)]
        [DefaultValue(55)]
        [ReloadRequired]
        public int CrimtaneBatDamage;

        [Range(0, 32767)]
        [DefaultValue(40)]
        [ReloadRequired]
        public int DemoniteBatDamage;

        [Range(0, 32767)]
        [DefaultValue(27)]
        [ReloadRequired]
        public int EbonwoodBatDamage;

        [Range(0, 32767)]
        [DefaultValue(43)]
        [ReloadRequired]
        public int GoldBatDamage;

        [Range(0, 32767)]
        [DefaultValue(180)]
        [ReloadRequired]
        public int HallowedBatDamage;

        [Range(0, 32767)]
        [DefaultValue(35)]
        [ReloadRequired]
        public int IronBatDamage;

        [Range(0, 32767)]
        [DefaultValue(38)]
        [ReloadRequired]
        public int LeadBatDamage;

        [Range(0, 32767)]
        [DefaultValue(300)]
        [ReloadRequired]
        public int LuminiteBatDamage;

        [Range(0, 32767)]
        [DefaultValue(27)]
        [ReloadRequired]
        public int MahoganyWoodBatDamage;

        [Range(0, 32767)]
        [DefaultValue(33)]
        [ReloadRequired]
        public int MeteorBatDamage;

        [Range(0, 32767)]
        [DefaultValue(375)]
        [ReloadRequired]
        public int MidnightBatDamage;

        [Range(0, 32767)]
        [DefaultValue(65)]
        [ReloadRequired]
        public int MoltenBatDamage;

        [Range(0, 32767)]
        [DefaultValue(125)]
        [ReloadRequired]
        public int MythrilBatDamage;

        [Range(0, 32767)]
        [DefaultValue(148)]
        [ReloadRequired]
        public int OrichalcumBatDamage;

        [Range(0, 32767)]
        [DefaultValue(128)]
        [ReloadRequired]
        public int PalladiumBatDamage;

        [Range(0, 32767)]
        [DefaultValue(48)]
        [ReloadRequired]
        public int PlatinumBatDamage;

        [Range(0, 32767)]
        [DefaultValue(27)]
        [ReloadRequired]
        public int ShadewoodBatDamage;

        [Range(0, 32767)]
        [DefaultValue(188)]
        [ReloadRequired]
        public int ShroomiteBatDamage;

        [Range(0, 32767)]
        [DefaultValue(4)]
        [ReloadRequired]
        public int SilkBatDamage;

        [Range(0, 32767)]
        [DefaultValue(40)]
        [ReloadRequired]
        public int SilverBatDamage;

        [Range(0, 32767)]
        [DefaultValue(28)]
        [ReloadRequired]
        public int TinBatDamage;

        [Range(0, 32767)]
        [DefaultValue(153)]
        [ReloadRequired]
        public int TitaniumBatDamage;

        [Range(0, 32767)]
        [DefaultValue(40)]
        [ReloadRequired]
        public int TungstenBatDamage;

        [Range(0, 32767)]
        [DefaultValue(27)]
        [ReloadRequired]
        public int WoodBatDamage;

        [Range(0, 32767)]
        [DefaultValue(39)]
        [ReloadRequired]
        public int CactusBatDamage;

        [Range(0, 32767)]
        [DefaultValue(27)]
        [ReloadRequired]
        public int BorealWoodBatDamage;

        [Range(0, 32767)]
        [DefaultValue(27)]
        [ReloadRequired]
        public int PalmWoodBatDamage;

        [Range(0, 32767)]
        [DefaultValue(50)]
        [ReloadRequired]
        public int AshWoodBatDamage;

        [Range(0, 32767)]
        [DefaultValue(76)]
        [ReloadRequired]
        public int PearlwoodBatDamage;

        [Range(0, 32767)]
        [DefaultValue(33)]
        [ReloadRequired]
        public int PumpkinBatDamage;

    }
}