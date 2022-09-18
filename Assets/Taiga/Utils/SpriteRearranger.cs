using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Taiga.Utils
{
    public class SpriteRearranger
    {
        public static void Rearrange()
        {
            var camera = UnityEngine.Camera.main;
            var cameraPosition = camera.transform.position;

            // Debug.LogFormat(">>cam_pos<< {0}", cameraPosition);
            var distanceKeyValues = new List<KeyValuePair<float, GameObject>>();
            var gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            var rearrangeableGameObjects = gameObjects
                .Where(rgo =>
                {
                    return rgo.CompareTag("Rearrangeable") &&
                           rgo.hideFlags != HideFlags.NotEditable &&
                           rgo.hideFlags != HideFlags.HideAndDontSave &&
                           !EditorUtility.IsPersistent(rgo.transform.root.gameObject);
                })
                .ToArray();
            foreach (var gameObject in rearrangeableGameObjects)
            {
                // var camDistance = camera.WorldToScreenPoint(gameObject.transform.position).y;
                // var camDistance = Vector3.Distance(
                //     cameraPosition,
                //     gameObject.transform.position
                // );

                var camDistance = Vector3.Distance(
                    Vector3.Scale(cameraPosition, new Vector3(1, 0, 1)),
                    Vector3.Scale(gameObject.transform.position, new Vector3(1, 0, 1))
                    );
                if (gameObject.name == "Body") // || gameObject.name == "Effect 1")
                {
                    Debug.Log(
                        $">>cam_distance<< [{gameObject.name}] [{gameObject.transform.root.name}] angle:{camera.transform.root.localRotation.eulerAngles.y} x:{gameObject.transform.position.x} z:{gameObject.transform.position.z} {camDistance}");
                }

                distanceKeyValues.Add(new KeyValuePair<float, GameObject>(camDistance, gameObject));
                // Debug.LogFormat(">>cam_distance<< {0}", camDistance);
            }

            var sortedDistanceKeyValues =
                distanceKeyValues.OrderByDescending(kv => kv.Key);

            var sortingOrder = 0;
            foreach (var distanceKeyValue in sortedDistanceKeyValues)
            {
                var gameObject = distanceKeyValue.Value;
                var sprite = gameObject.GetComponent<SpriteRenderer>();
                if (sprite == null && gameObject.transform.childCount > 0)
                {
                    sprite = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
                }
                if (sprite == null)
                {
                    continue;
                }

                sprite.sortingOrder = sortingOrder;
                // Debug.LogFormat(">>game_object_name<< {0} {1}", gameObject.name, sortingOrder);
                sortingOrder++;
            }

            // Debug.LogFormat(">>rgo_count<< {0}", sortedDistanceKeyValues.Count());
        }
    }
}