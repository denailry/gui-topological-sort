using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace topological_sort
{
    public class TopologicalSort
    {
        Graph graph;
        List<string> result;

        public TopologicalSort()
        {

        }

        public void BFS()
        {

        }

        public void DFSUtil(Graph.Vertex V, ref int timestamp, ref List<int> startstamp, ref List<int> stopstamp, ref List<bool> visited)
        {
            int i;
            foreach (string vertexname in graph.GetNeighbor(V.data)) {
                i = graph.GetVertexIndex(vertexname);
                if (!visited[i]) {
                    timestamp++;
                    startstamp[i] = timestamp;
                    DFSUtil(graph.GetVertex(i), ref timestamp, ref startstamp, ref stopstamp, ref visited);
                    stopstamp[i] = timestamp;
                }
            }
        }

        public void DFS()
        {
            int timestamp = 0;
            int i;
            List<bool> visited = new List<bool>(new bool[graph.GetGraphSize()]);
            List<int> startstamp = new List<int>(graph.GetGraphSize());
            List<int> stopstamp = new List<int>(graph.GetGraphSize());

            foreach (Graph.Vertex V in graph.GetVertices())
            {
                i = V.GetIndex();
                if (!visited[i]) {
                    timestamp++;
                    startstamp[i] = timestamp;
                    DFSUtil(V, ref timestamp, ref startstamp, ref stopstamp, ref visited);
                    stopstamp[i] = timestamp;
                }
            }
            /*//Sort based on stopstamp
            var sorted = stopstamp
                .Select((x, j) => new KeyValuePair<int, int>(x, j))
                .OrderBy(x < x.Key)
                .ToList();
            List<int> result = sorted.Select(x => x.Key).ToList();
            List<int> idx = sorted.Select(x => x.Value).ToList();
            return */
        }

        public List<string> GetResult()
        {
            return this.result;
        }
    }
}
