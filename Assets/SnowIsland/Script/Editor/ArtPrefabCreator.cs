using UnityEditor;
using UnityEngine;

namespace TaterGame.SnowIsland.Editor
{
    public class ArtPrefabCreator
    {
        [MenuItem("Tools/TaterGame/ProjectWinter/ArtPrefabCreator")]
        public static void ShowExample()
        {
            var obj  = Selection.gameObjects;
            foreach (var gameObject in obj)
            {
                gameObject.transform.localScale = Vector3.one;
                gameObject.transform.position = Vector3.zero;
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.parent = new GameObject($"AP_{gameObject.name}").transform;
            }
        }
    }
}