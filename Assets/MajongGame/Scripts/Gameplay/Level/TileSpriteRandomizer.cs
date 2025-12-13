using MajongGame.Gameplay.Tiles;
using System.Collections.Generic;
using UnityEngine;

namespace MajongGame.Gameplay.Level
{
    public class TileSpriteRandomizer
    {
        public void Randomize(List<Sprite> sprites, List<Tile> tiles)
        {
            List<Sprite> freeSprites = ShuffleSprites(sprites, tiles.Count);

            for (int i = 0; i < freeSprites.Count; i++)
            {
                tiles[i].SetSprite(freeSprites[i]);
            }
        }

        private List<Sprite> ShuffleSprites(List<Sprite> sprites, int tilesCount)
        {
            if (sprites == null || sprites.Count <= 1)
                return null;

            System.Random random = new System.Random();

            List<Sprite> list = new List<Sprite>(sprites);

            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                Sprite temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }

            List<Sprite> result = new List<Sprite>();

            int count = tilesCount / 3;
            for (int i = 0; i < count; i++)
            {
                if (result.Count == tilesCount) 
                    break;
                if (i >= list.Count) i = 0;

                result.Add(list[i]);
                result.Add(list[i]);
                result.Add(list[i]);
            }

            for (int i = result.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                Sprite temp = result[i];
                result[i] = result[j];
                result[j] = temp;
            }

            return result;
        }
    }
}