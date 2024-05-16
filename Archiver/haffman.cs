using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Archiver.haffman;

namespace Archiver
{
    internal class haffman
    {
        public class HuffmanNode
        {
            public char Symbol { get; set; }
            public int Frequency { get; set; }
            public int h { get; set; }
            public HuffmanNode Left { get; set; }
            public HuffmanNode Right { get; set; }
        }


        private Dictionary<char, int> _frequencies;

        public haffman(Dictionary<char, int> frequencies)
        {
            _frequencies = frequencies;
        }

        public HuffmanNode BuildTree()
        {
            var priorityQueue = new SortedDictionary<int, List<HuffmanNode>>();

            // Create a priority queue sorted by frequency
            foreach (var kvp in _frequencies)
            {
                var node = new HuffmanNode { Symbol = kvp.Key, Frequency = kvp.Value };
                if (!priorityQueue.ContainsKey(node.Frequency))
                    priorityQueue[node.Frequency] = new List<HuffmanNode>();
                priorityQueue[node.Frequency].Add(node);
            }

            // Construct Huffman Tree
            while (priorityQueue.Count > 1)
            {
                var firstMinFreq = priorityQueue.First();
                var firstMinNode = firstMinFreq.Value.First();
                firstMinFreq.Value.RemoveAt(0);
                if (firstMinFreq.Value.Count == 0)
                    priorityQueue.Remove(firstMinFreq.Key);

                var secondMinFreq = priorityQueue.First();
                var secondMinNode = secondMinFreq.Value.First();
                secondMinFreq.Value.RemoveAt(0);
                if (secondMinFreq.Value.Count == 0)
                    priorityQueue.Remove(secondMinFreq.Key);

                var newNode = new HuffmanNode
                {
                    Frequency = firstMinNode.Frequency + secondMinNode.Frequency,
                    Left = firstMinNode,
                    Right = secondMinNode
                };

                if (!priorityQueue.ContainsKey(newNode.Frequency))
                    priorityQueue[newNode.Frequency] = new List<HuffmanNode>();
                priorityQueue[newNode.Frequency].Add(newNode);
            }

            return priorityQueue.First().Value.First();
        }

        public Dictionary<char, string> BuildCodes()
        {
            var root = BuildTree();
            var codes = new Dictionary<char, string>();
            TraverseTree(root, "", codes);
            return codes;
        }

        private void TraverseTree(HuffmanNode node, string code, Dictionary<char, string> codes)
        {
            if (node == null)
                return;
            if (node.Left == null && node.Right == null)
                codes[node.Symbol] = code;
            TraverseTree(node.Left, code + "0", codes);
            TraverseTree(node.Right, code + "1", codes);
        }

    }
  
}


       
            

           
        

