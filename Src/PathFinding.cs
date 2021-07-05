using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    class PathFinding
    {
        private Grid grid;

        static public float Heuristic(Location a, Location b)
        {
            //return MathF.Abs(a.x - b.x) + MathF.Abs(a.y - b.y);
            return MathF.Sqrt(MathF.Pow(a.x - b.x, 2) + MathF.Pow(a.y - b.y, 2));
        }

        public PathFinding(Grid grid)
        {
            this.grid = grid;
        }

        public Location GetNextCell(Location start, Location goal)
        {
            Dictionary<Location, Location> cameFrom = new Dictionary<Location, Location>();
            Dictionary<Location, float> costSoFar = new Dictionary<Location, float>();

            return GetNextCell(start, goal, cameFrom, costSoFar);
        }

        private Location GetNextCell(Location start, Location goal, Dictionary<Location, Location> cameFrom, Dictionary<Location, float> costSoFar)
        {
            var frontier = new PriorityQueue<Location>();
            frontier.Enqueue(start, 0);

            cameFrom[start] = start;
            costSoFar[start] = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (current == goal)
                {
                    break;
                }

                foreach (var neighbor in grid.Neighbors(current))
                {
                    float newCost = costSoFar[current] + 1;

                    if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                    {
                        costSoFar[neighbor] = newCost;
                        float priority = newCost + Heuristic(neighbor, goal);
                        frontier.Enqueue(neighbor, priority);
                        cameFrom[neighbor] = current;
                    }
                }
            }

            var result = goal;

            while (cameFrom.ContainsKey(result) && cameFrom[result] != start)
            {
                result = cameFrom[result];
            }

            return result;
        }
    }
}
