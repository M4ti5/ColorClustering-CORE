using System.Collections.Generic;

namespace ColorClustering {
    public class KNode : Pixel {
        public byte? bindedCluster = null;
        public int weight = 1;
        public KNode (byte _red , byte _green , byte _blue) : base(_red , _green , _blue) {

        }
        public static PixelTree DeleteDuplicate (List<KNode> list) {
            PixelTree tree = new PixelTree(list[0]);

            foreach (KNode node in list) {
                if (!tree.Contains(node)) {
                    tree.AddNode(node);
                } else {
                    node.weight += 1;
                }
            }
            return tree;
        }
    }
}
