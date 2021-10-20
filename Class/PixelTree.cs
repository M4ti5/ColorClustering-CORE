namespace ColorClustering {
    class PixelTree {
        public class PixelTreeNode {

            public Pixel node;

            public PixelTreeNode parent = null;
            public PixelTreeNode rigthChild = null;
            public PixelTreeNode leftChild = null;

            public PixelTreeNode (PixelTreeNode _parent) {
                parent = _parent;
            }
            public PixelTreeNode (PixelTreeNode _parent , Pixel _node) : this(_parent) {
                node = _node;
            }

            public void AddLeftChild (Pixel node) {
                leftChild = new PixelTreeNode(this , node);
            }
            public void AddRightChild (Pixel node) {
                rigthChild = new PixelTreeNode(this , node);
            }
        }

        private PixelTreeNode root;

        public PixelTree () {

        }



        public PixelTreeNode GetNode (Pixel node) {
            return SearchNode(root , node);
        }

        public bool Contain (Pixel node) {
            if (SearchNode(root , node) != null) {
                return true;
            } else {
                return false;
            }
        }

        private PixelTreeNode SearchNode (PixelTreeNode head , Pixel node) {

            if (head == null) {
                return null;
            }

            if (node.red < head.node.red) { // Red
                head.rigthChild = SearchNode(head.rigthChild , node);
            } else if (node.red > head.node.red) {
                head.leftChild = SearchNode(head.leftChild , node);
            } else {
                if (node.green < head.node.green) { //Green
                    head.rigthChild = SearchNode(head.rigthChild , node);
                } else if (node.green > head.node.green) {
                    head.leftChild = SearchNode(head.leftChild , node);
                } else {
                    if (node.blue < head.node.blue) { // Blue
                        head.rigthChild = SearchNode(head.rigthChild , node);
                    } else if (node.blue > head.node.blue) {
                        head.leftChild = SearchNode(head.leftChild , node);
                    } else {
                        return head;
                    }
                }

            }
            return head;
        }

        public void AddNode (Pixel node) {
            InsertNode(root.parent , root , node);
        }
        private PixelTreeNode InsertNode (PixelTreeNode parent , PixelTreeNode head , Pixel node) {

            if (head == null) {
                head = new PixelTreeNode(parent , node);
                return head;
            }

            if (node.red < head.node.red) { // Red
                head.rigthChild = InsertNode(head , head.rigthChild , node);
            } else if (node.red > head.node.red) {
                head.leftChild = InsertNode(head , head.leftChild , node);
            } else {
                if (node.green < head.node.green) { //Green
                    head.rigthChild = InsertNode(head , head.rigthChild , node);
                } else if (node.green > head.node.green) {
                    head.leftChild = InsertNode(head , head.leftChild , node);
                } else {
                    if (node.blue < head.node.blue) { // Blue
                        head.rigthChild = InsertNode(head , head.rigthChild , node);
                    } else if (node.blue > head.node.blue) {
                        head.leftChild = InsertNode(head , head.leftChild , node);
                    } else {
                        return null;
                    }
                }

            }
            return head;
        }
    }
}
