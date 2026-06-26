using System.Collections.Generic;
using Core.Detection.Olfactory;
using Core.Detection.Olfactory.DataStructures;
using UnityEngine;

namespace Unity.Detection
{
    public class ScentMap : IScentMap
    {
        private List<OlfactoryData> _nodes = new();
        public void Deposit(OlfactoryData data)
        {
            _nodes.Add(data);
        }

        public List<OlfactoryData> Sample(Vector2 position, float scentRadius)
        {
            List<OlfactoryData> results = new();
            for (int i = 0; i < _nodes.Count; i++)
            {
                if (Vector2.Distance(_nodes[i].ScentPosition, position) <= scentRadius)
                {
                    results.Add(_nodes[i]);
                }
            }
            return results;
        }

        public void Decay(float dt)
        {

            for (int i = _nodes.Count - 1; i >= 0; i--)
            {
                var node = _nodes[i];
                node.DecayIntensity(dt);
                if (node.ScentIntensity <= 0f)
                    _nodes.RemoveAt(i);
                else
                    _nodes[i] = node;
            }
        }

        // private void ClearEmptyEntries()
        // {
        //     for (int i = 0; i < _removalEntries.Count)
        // }

        // private (int, int) PositionToCell(Vector2 position)
        // {
        //     // Convert the floats to integers
        //     int x = (int)position.x;
        //     int y = (int)position.y;
        //     return new(x, y);
        // }
    }
}