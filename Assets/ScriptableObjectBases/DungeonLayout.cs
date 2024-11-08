using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonLayout", menuName = "ScriptableObjects/Dungeon/New Layout Tree")]

public class DungeonLayout : ScriptableObject
{
    public int nodeKey => layout.Key;

    KeyValuePair<int, int> layout;
}
