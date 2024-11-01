using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonTree
{
    public class NodeInfoAttribute : Attribute
    {
        private string m_nodeTitle;
        private string m_menuItem;

        public string title => m_nodeTitle;
        public string menuItem => m_menuItem;

        public NodeInfoAttribute(string title, string menuItem = "")
        {
            m_nodeTitle = title;
            m_menuItem = menuItem;
        }
    }
}