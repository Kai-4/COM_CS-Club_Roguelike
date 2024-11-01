using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonTree
{
    [System.Serializable]
    public class DungeonTreeNode
    {
        [SerializeField]
        private string m_guide;
        [SerializeField]
        private Rect m_position;

        public string typeName;

        public string id => m_guide;
        public Rect position => m_position;

        public DungeonTreeNode()
        {
            NewGUID();
        }

        private void NewGUID()
        {
            m_guide = Guid.NewGuid().ToString();
        }

        public void SetPosition(Rect position)
        {
            m_position = position;
        }
    }
}

