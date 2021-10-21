using System.Collections.Generic;

namespace ColorClustering {
    public class DBSArea {
        private List<DBSNode> nodes;

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

        public static List<DBSArea> Purge (List<DBSArea> areas , int m , out DBSArea outliers) {
            List<DBSArea> temp = new List<DBSArea>();
            outliers = new DBSArea();
            foreach(DBSArea area in areas) {
                if (area.Size() < m) {
                    foreach( DBSNode node in area.nodes) {
                        node.area = null;
                        outliers.Add(node);
                    }
                } else {
                    temp.Add(area);
                    outliers = null;
                }
            }

            return temp;
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
