using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonTree
{
    [CreateAssetMenu(fileName = "DungeonTree", menuName = "ScriptableObjects/New Dungeon Tree")]
    public class DungeonTreeAsset : ScriptableObject
    {
        [SerializeReference]
        private List<DungeonTreeNode> m_nodes;

        public List<DungeonTreeNode> Nodes => m_nodes;

        public DungeonTreeAsset()
        {
            m_nodes = new List<DungeonTreeNode>();
        }
    }
}