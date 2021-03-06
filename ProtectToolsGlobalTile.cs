﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ProtectTools
{
    class ProtectToolsGlobalTile : GlobalTile
    {
        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            return !TileUtils.isProtected(i, j);
        }
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (TileUtils.isProtected(i, j))
            {
                fail = true;
                effectOnly = true;
                noItem = true;
            }
        }
        public override bool Slope(int i, int j, int type)
        {
            bool result = !TileUtils.isProtected(i, j);
            int sloop = TileWallUI.instance.btnSlope.GetValue<int>();
            if (0 <= sloop)
            {
                result = false;
                if (sloop == 5)
                {
                    Main.tile[i, j].halfBrick(false);
                    WorldGen.PoundTile(i, j);
                }
                else
                {
                    WorldGen.SlopeTile(i, j, sloop);
                }
            }
            return result;
        }
        public override bool CreateDust(int i, int j, int type, ref int dustType)
        {
            return !TileUtils.isProtected(i, j);
        }
        public override bool KillSound(int i, int j, int type)
        {
            return !TileUtils.isProtected(i, j);
        }
    }
}
