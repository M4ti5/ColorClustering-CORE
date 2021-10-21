namespace ColorClustering {
    public class PixelTree {
        public class PixelTreeNode {

            public Pixel node;

            public PixelTreeNode parent = null;
            public PixelTreeNode rightChild = null;
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
                rightChild = new PixelTreeNode(this , node);
            }
        }

        public PixelTreeNode root;

        public PixelTree () {
        }

        public PixelTree (Pixel _node) : this() {
            root = new PixelTreeNode(null , _node);
        }

        public PixelTreeNode GetPixelTreeNode (Pixel node) {
            PixelTreeNode temp = SearchNode(root , node);
            return temp;
        }

        public bool Contains (Pixel node) {
            if (SearchNode(root , node) != null) {
                return true;
            } else {
                return false;
            }
        }
        public PixelTreeNode SearchNode (PixelTreeNode head , Pixel node) {

            if (head == null) {
                return null;
            }

            if (head.node == node) {
                return head;
            }

            if (node.red < head.node.red) { // Red
                head = SearchNode(head.leftChild , node);
            } else if (node.red > head.node.red) {
                head = SearchNode(head.rightChild , node);
            } else {
                if (node.green < head.node.green) { //Green
                    head = SearchNode(head.leftChild , node);
                } else if (node.green > head.node.green) {
                    head = SearchNode(head.rightChild , node);
                } else {
                    if (node.blue < head.node.blue) { // Blue
                        head = SearchNode(head.leftChild , node);
                    } else if (node.blue > head.node.blue) {
                        head = SearchNode(head.rightChild , node);
                    }
                }
            }

            return head;
        }


        public void AddNode (Pixel node) {
            InsertNode(root , root , node);
        }
        private PixelTreeNode InsertNode (PixelTreeNode parent , PixelTreeNode head , Pixel node) {

            if (head == null) {
                head = new PixelTreeNode(parent , node);
                return head;
            }

            if (node.red < head.node.red) { // Red
                head.leftChild = InsertNode(head , head.leftChild , node);
            } else if (node.red > head.node.red) {
                head.rightChild = InsertNode(head , head.rightChild , node);
            } else {
                if (node.green < head.node.green) { //Green
                    head.leftChild = InsertNode(head , head.leftChild , node);
                } else if (node.green > head.node.green) {
                    head.rightChild = InsertNode(head , head.rightChild , node);
                } else {
                    if (node.blue < head.node.blue) { // Blue
                        head.leftChild = InsertNode(head , head.leftChild , node);
                    } else if (node.blue > head.node.blue) {
                        head.rightChild = InsertNode(head , head.rightChild , node);
                    } else {
                        return null;
                    }
                }

            }
            return head;
        }

        public PixelTreeNode FindMax (PixelTreeNode head) {
            return FindMax(null , head);
        }

        private PixelTreeNode FindMax (PixelTreeNode parent , PixelTreeNode head) {
            if (head == null) {
                return parent;
            } else {
                return FindMax(head , head.rightChild);
            }

        }

        public void RemoveNode (Pixel node) {
            PixelTreeNode toRemove = GetPixelTreeNode(node);
            PixelTreeNode toMove = null;

            if (toRemove.leftChild != null || toRemove.rightChild != null) {//With Child(s)

                if (toRemove.leftChild != null && toRemove.rightChild != null) {//2 childs
                    toMove = FindMax(null , toRemove.leftChild);

                    if (toRemove.leftChild != toMove) {
                        toMove.leftChild = toRemove.leftChild;
                    }
                    toMove.rightChild = toRemove.rightChild;
                    if (toMove.leftChild != null) {
                        toMove.leftChild.parent = toMove;
                    }
                    if (toMove.rightChild != null) {
                        toMove.rightChild.parent = toMove;
                    }

                } else if (toRemove.leftChild != null) {//Only leftChild
                    toMove = toRemove.leftChild;

                    toMove.leftChild.parent = toMove;
                    toMove.leftChild = toRemove.leftChild;

                } else if (toRemove.rightChild != null) {//Only rightChild
                    toMove = toRemove.rightChild;

                    toMove.rightChild = toRemove.rightChild;
                    toMove.rightChild.parent = toMove;
                }

                toMove.parent = toRemove.parent;

            } else {//Wihout childs
                toRemove.parent = null;
            }

            //Parent's Binds
            if (toRemove.parent.leftChild == toRemove) {//Is leftChild of parent
                toRemove.parent.leftChild = toMove;
            } else if (toRemove.parent.rightChild == toRemove) {// Is rightChild of parent
                toRemove.parent.rightChild = toMove;
            }
        }
    }
}
