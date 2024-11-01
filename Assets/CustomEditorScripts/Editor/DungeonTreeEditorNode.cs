using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace DungeonTree
{
    public class DungeonTreeEditorNode : Node
    {
        private DungeonTreeNode m_treeNode;
        public DungeonTreeNode Node => m_treeNode;
        public DungeonTreeEditorNode(DungeonTreeNode node)
        {
            this.AddToClassList("dungeon-tree-node");

            m_treeNode = node;

            Type typeInfo = node.GetType();
            NodeInfoAttribute info = typeInfo.GetCustomAttribute<NodeInfoAttribute>();

            title = info.title;

            string[] depths = info.menuItem.Split('/');
            foreach (string depth in depths)
            {
                this.AddToClassList(depth.ToLower().Replace(' ', '-'));
            }

            this.name = typeInfo.Name;
        }
    }
}