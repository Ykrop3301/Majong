using Gameplay.Tiles;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Level
{
    public class SpriteRandomizer
    {
        public void Randomize<T>(List<Sprite> sprites, List<T> objects) where T : IHaveSprite
        {
            List<Sprite> freeSprites = ShuffleSprites(sprites, objects.Count);

            for (int i = 0; i < freeSprites.Count; i++)
            {
                objects[i].SetSprite(freeSprites[i]);
            }
        }

        private List<Sprite> ShuffleSprites(List<Sprite> sprites, int needCount)
        {
            if (sprites == null || sprites.Count == 0 || needCount <= 0)
                return new List<Sprite>();

            System.Random random = new System.Random();

            List<Sprite> shuffled = new List<Sprite>(sprites);
            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
            }

            List<Sprite> result = new List<Sprite>(needCount);

            int added = 0;
            int sourceIndex = 0;

            while (added < needCount)
            {
                int toAdd = Mathf.Min(3, needCount - added);

                for (int j = 0; j < toAdd; j++)
                {
                    result.Add(shuffled[sourceIndex]);
                    added++;
                }

                sourceIndex = (sourceIndex + 1) % shuffled.Count;
            }

            for (int i = result.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                (result[i], result[j]) = (result[j], result[i]);
            }

            return result;
        }
    }
}
