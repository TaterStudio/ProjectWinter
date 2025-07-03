using UnityEditor;
using UnityEngine;

namespace TaterGame.SnowIsland.Editor
{
    public class PrefabCreator
    {
        [MenuItem("Tools/TaterGame/ProjectWinter/ArtPrefabCreator")]
        public static void CreateArtPrefab()
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

        [MenuItem("Tools/TaterGame/ProjectWinter/ArtPrefab2GamePrefab")]
        public static void CreateGamePrefab()
        {
            
            var obj  = Selection.gameObjects;
            foreach (var gameObject in obj)
            {
                gameObject.transform.localScale = Vector3.one;
                gameObject.transform.position = Vector3.zero;
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.parent = new GameObject($"GP_{gameObject.name.Replace("AP_","")}").transform;
            }
        }
    }
}