﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ProtectTools
{
    public static class ProtectToolsUtils
    {
        public const int tileSize = 16;

        public static Texture2D Offset(this Texture2D texture, int x, int y, int width, int height)
        {
            Texture2D result = new Texture2D(Main.graphics.GraphicsDevice, width, height);
            Color[] data = new Color[height * width];
            texture.GetData(0, new Rectangle?(new Rectangle(x, y, width, height)), data, 0, data.Length);
            result.SetData(data);
            return result;
        }

        public static Texture2D Resize(this Texture2D texture, int width, int height)
        {
            Texture2D result = texture;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                texture.SaveAsPng(ms, width, height);
                result = Texture2D.FromStream(texture.GraphicsDevice, ms);
            }
            return result;
        }

        public static Texture2D Resize(this Texture2D texture, int size)
        {
            Texture2D result = texture;

            float max = Math.Max(texture.Width, texture.Height);
            float scale = size / max;
            int width = (int)(texture.Width * scale);
            int height = (int)(texture.Height * scale);

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                texture.SaveAsPng(ms, width, height);
                result = Texture2D.FromStream(texture.GraphicsDevice, ms);
            }
            return result;
        }

        public static Vector2 Offset(this Vector2 position, float x, float y)
        {
            position.X += x;
            position.Y += y;
            return position;
        }

        public static List<int>[] GetScreenContainTilesAndWalls()
        {
            List<int>[] result = { new List<int>(), new List<int>() };
            int maxX = Main.screenWidth / tileSize;
            int maxY = Main.screenHeight / tileSize;

            Point pos = Main.sceneTilePos.ToTileCoordinates();

            List<int> allTiles = TileUtils.GetAllTiles();
            List<int> allWalls = WallUtils.GetAllWalls();

            for (int x = pos.X; x < pos.X + maxX; x++)
            {
                for (int y = pos.Y; y < pos.Y + maxY; y++)
                {
                    var tile = Main.tile[x, y];
                    var tileItemType = TileUtils.getKillDropItemType(tile);
                    var wallItemType = WallUtils.getKillDropItemType(tile);

                    if (allTiles.Contains(tileItemType) && !result[0].Contains(tileItemType))
                    {
                        result[0].Add(tileItemType);
                    }
                    if (allWalls.Contains(wallItemType) && !result[1].Contains(wallItemType))
                    {
                        result[1].Add(wallItemType);
                    }
                }
            }
            return result;
        }
    }
}
