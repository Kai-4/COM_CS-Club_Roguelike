using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TownNode
{
    public Vector2 pos = new Vector2();
    public TownNode left;
    public TownNode right;
    public TownNode(Vector2 p, TownNode l, TownNode r)
    {
        pos = p;
        left = l;
        right = r;
    }
}

[CreateAssetMenu(fileName = "NodeMap", menuName = "ScriptableObjects/New Town Node Map")]
public class TownNodeMap : ScriptableObject
{
    public List<TownNode> nodes = new List<TownNode>();
}
