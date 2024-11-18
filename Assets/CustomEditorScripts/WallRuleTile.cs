using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class WallRuleTile : RuleTile<WallRuleTile.Neighbor> {
    public TileBase that;

    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int ThisOrThat = 3;
        public const int NotThisOrThat = 4;
        public const int Null = 5;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch (neighbor) {
            case Neighbor.ThisOrThat:
                return (tile != null && (tile == this || tile == that));
            case Neighbor.NotThisOrThat: 
                return (tile != null && !(tile == this || tile == that));
            case Neighbor.Null: return tile == null;
        }
        return base.RuleMatch(neighbor, tile);
    }
}