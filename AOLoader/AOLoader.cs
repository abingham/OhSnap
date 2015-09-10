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
            public string Description;
        }

        private static string GetPrefix(YamlMappingNode node)
        {
            try
            {
                var prefixNode = node.Children.First(c => ((YamlScalarNode)c.Key).Value == "_prefix");
                return ((YamlScalarNode)prefixNode.Value).Value;
            }
            catch (InvalidOperationException)
            {
                // We get to here if no "_prefix" node was found.
                return "";
            }                        
        }

        private static IEnumerable<Classification> load(YamlMappingNode node,
                                                        string prefix = "",
                                                        List<string> names = null)
        {
            if (null == names)
            {
                names = new List<string>();
            }

            foreach (var childNode in node.Children)
            {                
                var name = ((YamlScalarNode)childNode.Key).Value;

                if (name == "_classifications")
                {
                    foreach (var classificationMapping in (YamlMappingNode)childNode.Value)
                    {
                        var code = ((YamlScalarNode)classificationMapping.Key).Value;
                        var description = ((YamlScalarNode)classificationMapping.Value).Value;
                        yield return new Classification()
                        {
                            Code = prefix + code,
                            Name = names.ToArray<string>(),
                            Description = description
                        };
                    }
                }
                else if (!name.StartsWith("_"))
                {
                    var body = (YamlMappingNode)childNode.Value;


                    var newPrefix = prefix + GetPrefix(body);
                    names.Add(name);                    
                    foreach (var c in load(body, newPrefix, names))
                    {
                        yield return c;
                    }
                    names.RemoveAt(names.Count - 1);     
                }
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
