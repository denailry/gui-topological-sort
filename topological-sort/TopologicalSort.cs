using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace topological_sort
{
    public class TopologicalSort
    {
        private Graph graph;
        private List<string> result;

        public TopologicalSort()
        {

        }

        public void BFS()
        {

        }

        public void DFSUtil(Graph.Vertex V, ref int timestamp, ref List<int> startstamp, ref List<int> stopstamp, ref List<bool> visited, ref StreamWriter sw)
        {
            int i;
            foreach (string vertexname in graph.GetNeighbor(V.data)) {
                i = graph.GetVertexIndex(vertexname);
                timestamp++;
                if (!visited[i])
                {
                    startstamp[i] = timestamp;
                    sw.WriteLine();
                    DFSUtil(graph.GetVertex(i), ref timestamp, ref startstamp, ref stopstamp, ref visited, ref sw);
                    stopstamp[i] = timestamp;
                }
                else
                    sw.WriteLine("<");
            }
        }

        public void DFS()
        {
            StreamWriter sw = new StreamWriter("DFS.txt");
            int timestamp = 0;
            int i;
            List<bool> visited = new List<bool>(new bool[graph.GetGraphSize()]);
            List<int> startstamp = new List<int>(graph.GetGraphSize());
            List<int> stopstamp = new List<int>(graph.GetGraphSize());
            List<string> res = new List<string>(graph.GetGraphSize());

            foreach (Graph.Vertex V in graph.GetVertices())
            {
                timestamp++;
                i = V.GetIndex();
                if (!visited[i]) {
                    startstamp[i] = timestamp;
                    sw.WriteLine(V.data);
                    DFSUtil(V, ref timestamp, ref startstamp, ref stopstamp, ref visited, ref sw);
                    sw.WriteLine("<");
                    stopstamp[i] = timestamp;
                }
            }
            sw.Close();
            //Sort based on stopstamp
            var sorted = stopstamp
                .Select((x, j) => new KeyValuePair<int, int>(x, j))
                .OrderBy(x => x.Key)
                .ToList();
            //List<int> result = sorted.Select(x => x.Key).ToList();
            List<int> resultIndex = sorted.Select(x => x.Value).ToList();
            for (i = resultIndex.Capacity-1; i >= 0; i--) {
                res[i] = graph.GetVertex(i).data;
            }
            result = res;
        }

        public List<string> GetResult()
        {
            return this.result;
        }
    }
}
