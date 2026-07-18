using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "SiblingRuleTile", menuName = "Tiles/Sibling Rule Tile")]
public class SiblingRuleTile : RuleTile
{
    public List<TileBase> Siblings;

    public class Neighbor : RuleTile.TilingRule.Neighbor
    {
        public const int Null = 3;
        public const int NotNull = 4;
    }

    public override bool RuleMatch(int neighbor, TileBase tile)
    {
        if (tile == this || Siblings.Contains(tile))
        {
            return base.RuleMatch(neighbor, this);
        }
        return base.RuleMatch(neighbor, tile);
    }
}