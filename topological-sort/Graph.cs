using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace topological_sort
{
    public class Graph
    {
        public class Vertex
        {
            private int index;
            public string data;
            public Vertex(string data, int index)
            {
                this.index = index;
                this.data = data;
            }
        }
        /// <summary>
        /// 4 attributes
        /// A list of vertices (to store node information for each index such as name/text)
        /// a 2D array - our adjacency matrix, stores edges between vertices
        /// a graphSize integer
        /// a StreamReader, to read in graph data to create the data structure
        /// </summary>
        private List<Vertex> vertices;
        private int graphSize;
        private int maxSize;
        //private StreamReader sr;
        private int[,] adjMatrix;
        public Graph(int n)
        {
            vertices = new List<Vertex>();
            maxSize = n;//non-zero arrays, add 1
            adjMatrix = new int[maxSize, maxSize];
            for (int i=0; i<maxSize; i++)
                for (int j=0; j<maxSize; j++)
                    adjMatrix[i, j] = 0;
            graphSize = 0;
        }
        public int GetGraphSize()
        {
            return this.graphSize;
        }
        /*private void printPath(int[] path, int start, int end)
        {
            //prints a path, given a start and end, and an array that holds previous 
            //nodes visited
            Console.WriteLine("Shortest path from source to destination:");
            int temp = end;
            Stack<int> s = new Stack<int>();
            while (temp != start)
            {
                s.Push(temp);
                temp = path[temp];
            }
            Console.Write("{0} ", temp);//print source
            while (s.Count != 0)
            {
                Console.Write("{0} ", s.Pop());//print successive nodes to destination
            }
        }*/
        public Vertex GetVertex(int idx) {
            return vertices[idx];
        }
        public int GetVertexIndex(string data) {
            bool found = false;
            int i=0;
            while (i<graphSize && !found) {
                if (data == vertices[i].data)
                    found = true;
                else
                    i++;
            }
            if (found)
                return i;
            else
                return -1;
        }
        public List<string> GetNeighbor(string data) {
            List<string> L = new List<string>();
            int idx = GetVertexIndex(data);
            for (int j=0; j<graphSize; j++) {
                if (adjMatrix[idx, j] == 1)
                    L.Add(GetVertex(j).data);
            }
            return L;
        }
        public void AddVertex(string data)
        {
            Vertex newNode = new Vertex(data, graphSize);
            graphSize++;
            vertices.Add(newNode);
        }
        public void AddEdge(string vertexA, string vertexB)
        {
            int i = GetVertexIndex(vertexA);
            int j = GetVertexIndex(vertexB);
            Console.Write("{0}", i);
            if (i != -1 && j != -1)
            {
                adjMatrix[i, j] = 1;
            }
        }
        public void RemoveEdge(string vertexA, string vertexB)
        {
            int i = GetVertexIndex(vertexA);
            int j = GetVertexIndex(vertexB);
            if (i != -1 && j != -1)
            {
                adjMatrix[i, j] = 0;
            }
        }
        public bool isAdjacent(string vertexA, string vertexB)
        {   //checks whether two vertices are adjacent, returns true or false
            
            int i = GetVertexIndex(vertexA);
            int j = GetVertexIndex(vertexB);
            return adjMatrix[i, j] >= 0;
        }
        public void Display() //displays the adjacency matrix
        {
            Console.WriteLine("***********Adjacency Matrix Representation***********");
            Console.WriteLine("Number of nodes: {0}\n", graphSize - 1);
            Console.Write("\t");
            foreach (Vertex n in vertices)
            {
                Console.Write("{0}\t", n.data);
            }
            Console.WriteLine();//newline for the graph display
            for (int i = 0; i < graphSize; i++)
            {
                Console.Write("{0}\t", vertices[i].data);
                for (int j = 0; j < graphSize; j++)
                {
                    Console.Write("{0}\t", adjMatrix[i, j]);
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine("Read the graph from left to right");
        }
        private void DisplayVertex(int v) //displays data/description for a node
        {
            Console.WriteLine(vertices[v].data);
        }
    }
}
