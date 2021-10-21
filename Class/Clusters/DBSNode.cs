using System.Collections.Generic;

namespace ColorClustering {
    public class DBSNode : Pixel {
        public DBSArea area = null;
        public DBSNode (byte _red , byte _green , byte _blue) : base(_red , _green , _blue) {

        }
        public static List<DBSNode> DeleteDuplicate (List<DBSNode> list , out PixelTree tree) {

            List<DBSNode> cleanedList = new List<DBSNode>();
            tree = new PixelTree();

            for (int i = 0 ; i < list.Count ; i++) {

                if (cleanedList.Count == 0) {
                    cleanedList.Add(list[i]);
                    tree.AddNode(list[i]);
                }

                for (int j = 0 ; j < cleanedList.Count ; j++) {
                    if (list[i] == cleanedList[j]) {
                        break;
                    } else if (j == cleanedList.Count - 1) {
                        cleanedList.Add(list[i]);
                        tree.AddNode(list[i]);
                    }
                }

            }

            return cleanedList;
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
