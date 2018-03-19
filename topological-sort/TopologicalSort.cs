﻿using System;
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

        private void DFSUtil(Graph.Vertex V, ref int timestamp, ref List<int> stopstamp, ref List<bool> visited, ref StreamWriter sw)
        {
            int j;
            int i = V.GetIndex();
            
            //start
            visited[i] = true;
            sw.WriteLine(V.data);
            timestamp++;
            foreach (string vertexname in graph.GetNeighbor(V.data))
            {
                j = graph.GetVertexIndex(vertexname);
                sw.WriteLine("( " + V.data + " " + vertexname);
                if (!visited[j])
                {
                    DFSUtil(graph.GetVertex(j), ref timestamp, ref stopstamp, ref visited, ref sw);
                }
            }
            //stop
            sw.WriteLine("> " + V.data);
            timestamp++;
            stopstamp[i] = timestamp;
        }

        public void DFS()
        {
            StreamWriter sw = new StreamWriter("DFS.dat");
            int timestamp = 0;
            int i;
            List<bool> visited = new List<bool>(new bool[graph.GetGraphSize()]);
            List<int> stopstamp = new List<int>(new int[graph.GetGraphSize()]);
            List<string> res = new List<string>(new string[graph.GetGraphSize()]);

            foreach (Graph.Vertex V in graph.GetVertices())
            {
                i = V.GetIndex();
                if (!visited[i])
                {
                    DFSUtil(V, ref timestamp, ref stopstamp, ref visited, ref sw);
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
            for (i = 0; i <= resultIndex.Last(); i++)
            {
                res[i] = graph.GetVertex(resultIndex[resultIndex.Last()-i]).data;
            }
            result = res;
        }

        public List<string> GetResult()
        {
            return this.result;
        }
    }
}
