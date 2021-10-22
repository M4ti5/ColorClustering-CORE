using System.Collections.Generic;

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

            if (toRemove.leftChild != null && toRemove.rightChild != null) {// With 2 Childs

                //Child bind
                toMove = FindMax(null , toRemove.leftChild);

                toMove.leftChild = toRemove.leftChild;
                toMove.rightChild = toRemove.rightChild;

                toMove.leftChild.parent = toMove;
                toMove.rightChild.parent = toMove;

                //Parent Bind
                if (toRemove.parent != null) {
                    if (toRemove.parent.leftChild == toRemove) {
                        toRemove.parent.leftChild = toMove;
                    } else if (toRemove.parent.rightChild == toRemove) {
                        toRemove.parent.rightChild = toMove;
                    }
                }
                toMove.parent = toRemove.parent;

            } else if (toRemove.leftChild != null || toRemove.rightChild != null) {// With 1 Child
                
                //Child bind
                if (toRemove.leftChild != null) {// Only Left Child
                    toMove = toRemove.leftChild;
                }else if(toRemove.rightChild != null) {// Only right Child
                    toMove = toRemove.rightChild;
                }
                toMove.parent = toRemove.parent;

                //Parent Bind
                if (toRemove.parent != null) {
                    if (toRemove.parent.leftChild == toRemove) {
                        toRemove.parent.leftChild = toMove;
                    } else if (toRemove.parent.rightChild == toRemove) {
                        toRemove.parent.rightChild = toMove;
                    }
                }

            } else {//Without Child

                //Child Bind
                if (toRemove.parent != null) {
                    if (toRemove.parent.leftChild == toRemove) {
                        toRemove.parent.leftChild = toMove;
                    } else if (toRemove.parent.rightChild == toRemove) {
                        toRemove.parent.rightChild = toMove;
                    }
                }
                toRemove.parent = null;
            }

            
        }

        public List<Pixel> ToList () {
            List<Pixel> result = new List<Pixel>();

            List<PixelTreeNode> buffer = new List<PixelTreeNode>();// the use of buffer is than it enumerates node to do
            buffer.Add(root);

            while (buffer.Count != 0) { //Breadth-first order
                PixelTreeNode node = buffer[0];
                buffer.Remove(node);

                result.Add(node.node);

                if (node.leftChild != null) { //Add left Child into the buffer
                    buffer.Add(node.leftChild);
                }
                if (node.rightChild != null) {//Add right Child into the buffer
                    buffer.Add(node.rightChild);
                }
            }
            return result;
        }

    }
}
