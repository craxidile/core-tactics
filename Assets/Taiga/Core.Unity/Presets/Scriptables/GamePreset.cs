using System.Collections.Generic;
using System.Linq;
using Taiga.Core.Map;
using UnityEngine;

namespace Taiga.Core.Unity.Preset
{

    [CreateAssetMenu(fileName = "TaigaGamePreset", menuName = "Taiga Preset/Game", order = 1)]
    public class GamePreset : ScriptableObject, IMapPreset
    {
        public Texture2D map;

        public ICollection<Vector2Int> GetMapTerrain()
        {
            var result = new HashSet<Vector2Int>();
            for (var x = 0; x < map.width; x++)
            {
                for (var y = 0; y < map.height; y++)
                {
                    var color = map.GetPixel(x, y);
                    if (color == Color.black)
                    {
                        var position = new Vector2Int(x, y);
                        result.Add(position);
                    }
                }
            }
            return result;
        }
    }

}
