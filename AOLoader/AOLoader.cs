using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDotNet.RepresentationModel;

namespace AOLoader
{
    public class AOLoader
    {
        public struct Classification
        {
            public string Code;
            public string[] Name;
            public string description;
        }

        private static IEnumerable<Classification> load(YamlMappingNode node)
        {
            foreach (var entry in node.Children)
            {
                yield return new Classification()
                {
                    Code = ((YamlScalarNode)entry.Key).Value
                };
            }
        }

        public static IEnumerable<Classification> load(StreamReader input)
        {
            // Load the stream
            var yaml = new YamlStream();
            yaml.Load(input);

            var root = (YamlMappingNode)yaml.Documents[0].RootNode;
            return load(root);
        }

        public static IEnumerable<Classification> loadFile(string filename)
        {
            using (var input = new StreamReader(new FileStream(filename, FileMode.Open)))
            {
                return load(input);                
            }      
        }
    }
}
