using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DungeonTree
{
    public class DungeonTreeWindow : EditorWindow
    {
        

        public static void Open(DungeonTreeAsset target)
        {
            DungeonTreeWindow[] windows = Resources.FindObjectsOfTypeAll<DungeonTreeWindow>();
            foreach (var w in windows)
            {
                if(w.currentGraph == target)
                {
                    w.Focus();
                    return;
                }
            }

            DungeonTreeWindow window = CreateWindow<DungeonTreeWindow>(typeof(DungeonTreeWindow), typeof(SceneView));
            window.titleContent = new GUIContent($"{target.name}", EditorGUIUtility.ObjectContent(null, typeof(DungeonTreeAsset)).image);
            window.Load(target);
        }
        
        [SerializeField]
        private DungeonTreeAsset m_currentGraph;

        [SerializeField]
        private SerializedObject m_serializedObject;

        [SerializeField]
        private DungeonTreeView m_currentView;

        public DungeonTreeAsset currentGraph => m_currentGraph;

        private void OnEnable()
        {
            if (m_currentGraph != null)
            {
                DrawGraph();
            }
        }
        public void Load(DungeonTreeAsset target)
        {
            m_currentGraph = target;
            DrawGraph();
        }

        private void DrawGraph()
        {
            m_serializedObject = new SerializedObject(m_currentGraph);
            m_currentView = new DungeonTreeView(m_serializedObject, this);
            rootVisualElement.Add(m_currentView);
        }
    }
}
