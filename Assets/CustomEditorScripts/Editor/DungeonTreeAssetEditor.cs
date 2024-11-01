using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace DungeonTree {
    [CustomEditor(typeof(DungeonTreeAsset))]
public class DungeonTreeAssetEditor : UnityEditor.Editor
    {
        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId, int index)
        {
            Object asset = EditorUtility.InstanceIDToObject(instanceId);
            if (asset.GetType() == typeof(DungeonTreeAsset))
            {
                DungeonTreeWindow.Open((DungeonTreeAsset)asset);
                return true;
            }

            return false;
        }
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open"))
            {
                DungeonTreeWindow.Open((DungeonTreeAsset)target);
            }
        }
    }
}