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

        public TopologicalSort(Graph graph)
        {
            this.graph = graph;
        }

        public void BFS()
        {
            int [] visitCounter = new int[graph.GetGraphSize()];
            for (int i = 0; i < graph.GetGraphSize(); i++)
            {
                visitCounter[i] = 0;
            }
            List<string> result = new List<string>();

            Graph.Vertex root = graph.GetVertex(0);
            Queue<string> queue = new Queue<string>();
            List<string> neighbors = graph.GetNeighbor(root.data);

            System.IO.File.WriteAllText("BFS.dat", root.data + Environment.NewLine);
            string strNeighbors = "";
            foreach (var vertex in neighbors)
            {
                queue.Enqueue(vertex);
                strNeighbors = strNeighbors + vertex + ",";
            }
            System.IO.File.AppendAllText("BFS.dat", strNeighbors + Environment.NewLine);

            while (queue.Count != 0)
            {
                string visit = queue.Dequeue();
                int index = graph.GetVertexIndex(visit);
                neighbors = graph.GetNeighbor(visit);
                System.IO.File.AppendAllText("BFS.dat", visit + Environment.NewLine);
                strNeighbors = "";
                foreach (var vertex in neighbors)
                {
                    queue.Enqueue(vertex);
                    strNeighbors = strNeighbors + vertex + ",";
                }
                if (strNeighbors.Length != 0)
                {
                    System.IO.File.AppendAllText("BFS.dat", strNeighbors + Environment.NewLine);
                }
                visitCounter[index]++;
            }

            string resSeq = "";
            for (int i = 0; i < graph.GetGraphSize(); ++i)
            {
                resSeq = resSeq + visitCounter[i] + ",";
            }
            System.IO.File.AppendAllText("BFS.dat", resSeq + Environment.NewLine);

            bool finish;
            do
            {
                finish = true;
                int i = 0;
                int min = graph.GetGraphSize();
                int pos = -1;
                while (i < graph.GetGraphSize())
                {
                    if (visitCounter[i] != -1 && visitCounter[i] < min)
                    {
                        min = visitCounter[i];
                        pos = i;
                        finish = false;
                    }
                    i++;
                }
                if (!finish)
                {
                    string data = graph.GetVertex(pos).data;
                    result.Add(data);
                    neighbors = graph.GetNeighbor(graph.GetVertex(pos).data);
                    foreach (var vertex in neighbors)
                    {
                        visitCounter[graph.GetVertexIndex(vertex)]--; 
                    }
                    visitCounter[pos] = -1;
                }
            } while (!finish);

            if (this.result == null)
            {
                this.result = result;
            }

            resSeq = "";
            for (int i = 0; i < graph.GetGraphSize(); ++i)
            {
                resSeq = resSeq + result[i] + ",";
            }
            System.IO.File.AppendAllText("BFS.dat", resSeq + Environment.NewLine);
        }

        public void DFS()
        {
            // Output every vertex on each steps to file
        }

        public List<string> GetResult()
        {
            return this.result;
        }
    }
}
