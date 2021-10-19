using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorClustering {
    class DBSArea {
        private List<DBSNode> Nodes;

        public DBSArea () {
            Nodes = new List<DBSNode>(); 
        }

        public DBSArea (DBSNode _Node) : this() {
            Nodes.Add(_Node);
        }

        public DBSArea (List<DBSNode> _Nodes): this() {
            for( int i = 0 ; i < _Nodes.Count ; i++) {
                Nodes.Add(_Nodes[i]);
            }
        }

        public bool Have (DBSNode node) {
            if (Nodes.Contains(node)) {
                return true;
            } else {
                return false;
            } 
        }

        public void Add (DBSNode node) {
            Nodes.Add(node);
            node.area = this;
        }

        public DBSNode Get(int index) {
            return Nodes[index];
        }

        public int Size () {
            return Nodes.Count;
        }

        public void Merge (DBSArea other) {
            for(int i = 0 ; i < other.Size() ; i++) {
                Nodes.Add(other.Nodes[i]);
                other.Nodes[i].area = this;
            }
    
        }

        public static bool SameArea (DBSArea a , DBSArea b) {
            if(a == b) {
                return true;
            } else {
                return false;
            }
        }

    }
}
