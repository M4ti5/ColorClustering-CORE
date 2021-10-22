using System.Collections.Generic;

namespace ColorClustering {
    public class DBSNode : Pixel {
        public DBSArea area = null;
        public DBSNode (byte _red , byte _green , byte _blue) : base(_red , _green , _blue) {

        }
        public static PixelTree DeleteDuplicate (List<DBSNode> list) {
            PixelTree tree = new PixelTree(list[0]);
            
            foreach(DBSNode node in list){
                if(!tree.Contains(node)) {
                    tree.AddNode(node);
                }
            }

            return tree;
        }



        public static bool operator == (DBSNode a , DBSNode b) {
            if (a.red == b.red && a.green == b.green && a.blue == b.blue) {
                return true;
            } else {
                return false;
            }
        }

        public static bool operator != (DBSNode a , DBSNode b) {
            return !( a == b );
        }

        public bool HaveArea () {
            if (area != null) {
                return true;
            } else {
                return false;
            }
        }

    }
}
