using System.Collections.Generic;

namespace ColorClustering {
    public class DBSArea {
        public List<DBSNode> nodes;
        public DBSNode avrageNode;
        public DBSArea () {
            nodes = new List<DBSNode>();
        }

        public DBSArea (DBSNode _Node) : this() {
            nodes.Add(_Node);
        }

        public DBSArea (List<DBSNode> _nodes) : this() {
            for (int i = 0 ; i < _nodes.Count ; i++) {
                nodes.Add(_nodes[i]);
            }
        }

        public bool Have (DBSNode node) {
            if (nodes.Contains(node)) {
                return true;
            } else {
                return false;
            }
        }

        public void Add (DBSNode node) {
            nodes.Add(node);
            node.area = this;
        }

        public DBSNode Get (int index) {
            return nodes[index];
        }

        public int Size () {
            return nodes.Count;
        }

        public void Merge (DBSArea other) {
            for (int i = 0 ; i < other.Size() ; i++) {
                nodes.Add(other.nodes[i]);
                other.nodes[i].area = this;
            }

        }

        public DBSNode AvrageNode () {
            float[] temp = { 0 , 0 , 0 };
            int size = 0;
            foreach (DBSNode node in nodes) {
                temp[0] += node.red * node.weight;
                temp[1] += node.green * node.weight;
                temp[2] += node.blue * node.weight;
                size += node.weight;
            }


            temp[0] /= size;
            temp[1] /= size;
            temp[2] /= size;
            avrageNode = new DBSNode((byte)temp[0] , (byte)temp[1] , (byte)temp[2]);

            return avrageNode;
        }

        public static bool SameArea (DBSArea a , DBSArea b) {
            if (a == b) {
                return true;
            } else {
                return false;
            }
        }

    }
}
