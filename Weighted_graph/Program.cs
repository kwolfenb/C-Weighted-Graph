using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weighted_Graph
{
    class PriorityQueue
    {
        public List<object[]> values;
        public PriorityQueue()
        {
            this.values = new List<object[]>();
        }

        public void Enqueue(string val, int weight)
        {
            object[] data = new object[2] { val, weight };
            this.values.Add(data);
            this.sort();
        }

        public object[] Dequeue()
        {
            var val = this.values[0];
            this.values.RemoveAt(0);
            return val;
        }

        public void sort()
        {
            this.values = this.values.OrderBy(arr => arr[1]).ToList();
        }
    }
    class WeightedGraph
    {
        public Dictionary<string, Dictionary<string, int>> adjacencyList;
        public WeightedGraph()
        {
            this.adjacencyList = new Dictionary<string, Dictionary<string, int>>() { };
        }
        public void addVertex(string vertex) // Add new vertex
        {
            if (!this.adjacencyList.ContainsKey(vertex))
            {
                this.adjacencyList.Add(vertex, new Dictionary<string, int>());
            }
            else
            {
                Console.WriteLine("this vertex is in use");
            }
        }
        public void addEdge(string v1, string v2, int weight) // New edge between 2 vertices
        {
            if (this.adjacencyList.ContainsKey(v1) && this.adjacencyList.ContainsKey(v2))
            {
                this.adjacencyList[v1].Add(v2, weight);
                this.adjacencyList[v2].Add(v1, weight);
            }
            else
            {
                Console.WriteLine("Error: Vertex does not exist");
            }
        }

        public void Dijkstra(string start, string end)
        {
            int infinity = int.MaxValue;
            Dictionary<string, int> distances = new Dictionary<string, int>();
            PriorityQueue nodes = new PriorityQueue();
            Dictionary<string, string> previous = new Dictionary<string, string>();
            string smallest;
            foreach (var v in this.adjacencyList)
            {
                if (v.Key == start)
                {
                    distances.Add(v.Key, 0);
                    nodes.Enqueue(v.Key, 0);
                }
                else
                {
                    distances.Add(v.Key, infinity);
                    nodes.Enqueue(v.Key, infinity);
                }
                previous.Add(v.Key, null);
            }
            while (nodes.values.Count > 0)
            {
                smallest = (string)nodes.Dequeue()[0];
                if (smallest == end)
                {
                    break;
                }
                else
                {
                    foreach (var neighbor in this.adjacencyList[smallest])
                    {
                        Console.WriteLine(neighbor);
                    }

                }
            }

        }


        static void Main(string[] args)
        {
            WeightedGraph graph = new WeightedGraph();
            graph.addVertex("A"); graph.addVertex("D");
            graph.addVertex("B"); graph.addVertex("E");
            graph.addVertex("C"); graph.addVertex("F");
            graph.addEdge("A", "B", 4); graph.addEdge("A", "C", 2);
            graph.addEdge("B", "E", 3); graph.addEdge("C", "D", 2);
            graph.addEdge("C", "F", 4); graph.addEdge("D", "F", 1);
            graph.addEdge("F", "E", 1); graph.addEdge("D", "E", 3);

            foreach (var x in graph.adjacencyList)
            {
                foreach (KeyValuePair<string, int> y in x.Value)
                {
                    Console.WriteLine("vertex " + x.Key + " ---> " + y.Key + ": " + y.Value);
                }
            }

            PriorityQueue q = new PriorityQueue();
            q.Enqueue("A", 5);
            q.Enqueue("B", 4);
            q.Enqueue("C", 7);
            Console.WriteLine(q.Dequeue()[1]);

            graph.Dijkstra("A", "E");
        }
    }

}
