using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonRoomPool", menuName = "ScriptableObjects/Dungeon/New Room Pool")]
public class DungeonRoomPool : ScriptableObject
{
    [SerializeField]
    GameObject StartRoom;

    [SerializeField]
    List<GameObject> EnemyRooms;

    [SerializeField]
    List<GameObject> HazardRooms;

    [SerializeField]
    List<GameObject> HubRooms;

    [SerializeField]
    List<GameObject> TreasureRooms;

    [SerializeField]
    List<GameObject> Injectables;

    [SerializeField]
    List<GameObject> BossRooms;

}
