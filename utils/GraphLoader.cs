using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TAiO_Algorytmy
{
    public static class GraphLoader
    {
        public static Graph LoadGraph(string pathToFile, string name)
        {
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);

            if (!File.Exists(dir.Parent.FullName + "/"+pathToFile))
            {
                throw new FileNotFoundException(pathToFile);
            }
            using (var reader = new StreamReader(pathToFile))
            {
                var lines = new List<string[]>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    lines.Add(values);
                }
                if (lines.Count > 0)
                {
                    var size = lines.Count;
                    var matrix = new int[size, size];
                    for (var i = 0; i < size; i++)
                    {
                        for (var j = 0; j < size; j++)
                        {
                            if (int.TryParse(lines[i][j], out var value))
                            {
                                if (value == 0 || value == 1)
                                    matrix[i, j] = value;
                                else
                                    throw new InvalidDataException($"Input Data different than 0 or 1");
                            }
                            else
                            {
                                throw new InvalidDataException($"Could not parse cell [{i},{j}]");
                            }

                        }
                    }
                    return new Graph(matrix, name);
                }
                else
                {
                    throw new Exception("Empty file");
                }
            }
        }
        public static (Graph, Graph) SingleFileGraphLoader(string pathToFile, string name)
        {
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            if (!File.Exists(currentDirectory + "/" + pathToFile))
            {
                
                throw new FileNotFoundException(pathToFile);
            }
            using (var reader = new StreamReader(pathToFile))
            {
                var graph1Lines = new List<string[]>();
                var graph2Lines = new List<string[]>();
                while (!reader.EndOfStream)
                {
                    var graphSize = int.Parse(reader.ReadLine());
                    for (int i = 1; i < 1 + graphSize; i++)
                    {
                        var lineValues = reader.ReadLine().Split(',');
                        graph1Lines.Add(lineValues);
                    }
                    var graph2Size = int.Parse(reader.ReadLine());
                    for (int i = 1; i < 1 + graph2Size; i++)
                    {
                        var lineValues = reader.ReadLine().Split(',');
                        graph2Lines.Add(lineValues);
                    }

                }
                if (graph1Lines.Count > 0 && graph2Lines.Count > 0)
                {
                    int[,] matrix = generateGraph(graph1Lines);
                    Graph graph1 = new Graph(matrix, name);
                    int[,] matrix2 = generateGraph(graph2Lines);
                    Graph graph2 = new Graph(matrix2, "G2");
                    return (graph1, graph2);
                }
                else
                {
                    throw new Exception("Empty file");
                }
            }
        }

        private static int[,] generateGraph(List<string[]> graph1Lines)
        {
            var size = graph1Lines.Count;
            var matrix = new int[size, size];
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    if (int.TryParse(graph1Lines[i][j], out var value))
                    {
                        if (value == 0 || value == 1)
                            matrix[i, j] = value;
                        else
                            throw new InvalidDataException($"Input Data different than 0 or 1");
                    }
                    else
                    {
                        throw new InvalidDataException($"Could not parse cell [{i},{j}]");
                    }

                }
            }

            return matrix;
        }
    }
}
