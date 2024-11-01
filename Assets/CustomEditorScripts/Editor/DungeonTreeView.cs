using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

// https://youtu.be/iBukERGzEz0?t=1189

namespace DungeonTree {
    public class DungeonTreeView : GraphView
    {
        private DungeonTreeAsset m_dungeonTree;
        private SerializedObject m_serializedObject;
        private DungeonTreeWindow m_window;

        public DungeonTreeWindow window => m_window;

        public List<DungeonTreeEditorNode> m_treeNodes;
        public Dictionary<string, DungeonTreeEditorNode> m_nodeDictionary;

        private DungeonTreeWindowSearchProvider m_searchProvider;

        public DungeonTreeView(SerializedObject serializedObject, DungeonTreeWindow window)
        {
            m_serializedObject = serializedObject;
            m_dungeonTree = (DungeonTreeAsset)serializedObject.targetObject;
            m_window = window;

            m_treeNodes = new List<DungeonTreeEditorNode>();
            m_nodeDictionary = new Dictionary<string, DungeonTreeEditorNode>();

            m_searchProvider = ScriptableObject.CreateInstance<DungeonTreeWindowSearchProvider>();
            m_searchProvider.graph = this;

            this.nodeCreationRequest = ShowSearchWindow;

            StyleSheet style = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/CustomEditorScripts/Editor/DungeonTreeEditor.uss");
            styleSheets.Add(style);

            GridBackground background = new GridBackground();
            background.name = "Grid";
            Add(background);
            background.SendToBack();

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new ClickSelector());

            DrawNodes();

            graphViewChanged += OnGraphViewChangedEvent;
        }

        private GraphViewChange OnGraphViewChangedEvent(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                List<DungeonTreeEditorNode> nodes = graphViewChange.elementsToRemove.OfType<DungeonTreeEditorNode>().ToList();
                for (int i = nodes.Count - 1; i >= 0; i--)
                {
                    RemoveNode(nodes[i]);
                }
            }
            return graphViewChange;
        }

        private void RemoveNode(DungeonTreeEditorNode editorNode)
        {
            Undo.RecordObject(m_serializedObject.targetObject, "Removed Node");
            m_dungeonTree.Nodes.Remove(editorNode.Node);
            m_nodeDictionary.Remove(editorNode.Node.id);
            m_treeNodes.Remove(editorNode);
            m_serializedObject.Update();
        }

        private void DrawNodes()
        {
            foreach(DungeonTreeNode node in m_dungeonTree.Nodes)
            {
                AddNodeToGraph(node);
            }
        }

        private void ShowSearchWindow(NodeCreationContext obj)
        {
            m_searchProvider.target = (VisualElement)focusController.focusedElement;
            SearchWindow.Open(new SearchWindowContext(obj.screenMousePosition), m_searchProvider);
        }

        public void Add(DungeonTreeNode node)
        {
            Undo.RecordObject(m_serializedObject.targetObject, "Added Node");
            m_dungeonTree.Nodes.Add(node);
            m_serializedObject.Update();

            AddNodeToGraph(node);
        }

        private void AddNodeToGraph(DungeonTreeNode node)
        {
            node.typeName = node.GetType().AssemblyQualifiedName;

            DungeonTreeEditorNode editorNode = new DungeonTreeEditorNode(node);
            editorNode.SetPosition(node.position);
            m_treeNodes.Add(editorNode);
            m_nodeDictionary.Add(node.id, editorNode);

            AddElement(editorNode);
        }
    }
}