using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace ColorClustering {
    public class DBScan {
        private Image image;

        private List<DBSNode> pixelMap = new List<DBSNode>();
        private List<DBSNode> alteredPixelMap = new List<DBSNode>();
        private List<Pixel> reducedPixelMap = new List<Pixel>();
        private PixelTree pixelTree;

        private bool isClustered = false;

        public DBScan (Image _image, int m , int d) {

            Stopwatch time = new Stopwatch();
            Stopwatch mainTime = new Stopwatch();

            Console.WriteLine("DScan Start ...");
            mainTime.Start();

            image = _image;
            byte[] redMap = image.GetRedMap();
            byte[] greenMap = image.GetGreenMap();
            byte[] blueMap = image.GetBlueMap();


            for (int i = 0 ; i < image.width * image.height ; i++) {
                pixelMap.Add(new DBSNode(redMap[i] , greenMap[i] , blueMap[i]));
            }

            Console.WriteLine("Optimizing ...");
            time.Start();
            pixelTree = DBSNode.DeleteDuplicate(pixelMap);

            Console.WriteLine("Factoring ...");

            reducedPixelMap = pixelTree.ToList();
            Console.WriteLine("Factoring it's Okey!");

            time.Stop();
            Console.WriteLine("Optimizing it's Okey!");
            Console.WriteLine("Optimizing time : " + time.Elapsed.ToString());
            Console.WriteLine("Optimizing percent : " + ((float)reducedPixelMap.Count / (float)pixelMap.Count )* 100  + " %");

            time.Restart();
            Console.WriteLine("Clustering...");
            Clustering(m , d);
            time.Stop();
            Console.WriteLine("Clustering it's Okey!");
            Console.WriteLine("Clustering time : " + time.Elapsed.ToString());

            time.Restart();
            Console.WriteLine("Alteration...");
            Alteration();
            time.Stop();
            Console.WriteLine("Alteration it's Okey!");
            Console.WriteLine("Alteration time : " + time.Elapsed.ToString());

            mainTime.Stop();
            Console.WriteLine("DBScan time : " + mainTime.Elapsed.ToString());
        }

        public void Clustering (int m , float r) {

            List<DBSArea> areas = new List<DBSArea>(); // List of area 
            Stopwatch time = new Stopwatch();

            // Attribution of each Pixel
            for (int i = 0 ; i < reducedPixelMap.Count ; i++) {
                time.Start();
                DBSNode nodeRef = (DBSNode)reducedPixelMap[i];

                for (int j = i + 1 ; j < reducedPixelMap.Count ; j++) {

                    DBSNode nodeTest = (DBSNode)reducedPixelMap[j];

                    if (!( nodeRef.HaveArea() && nodeTest.HaveArea() && DBSArea.SameArea(nodeRef.area , nodeTest.area) )) {

                        if (InCircle(nodeRef , nodeTest , r)) {

                            if (!nodeRef.HaveArea() && !nodeTest.HaveArea()) { // Create Area
                                nodeRef.area = new DBSArea(nodeRef);
                                nodeTest.area = nodeRef.area;
                                nodeRef.area.Add(nodeTest);

                                areas.Add(nodeRef.area);

                            } else if (nodeRef.HaveArea() && !nodeTest.HaveArea()) { // Add Node to own Area
                                nodeRef.area.Add(nodeTest);
                            } else if (!nodeRef.HaveArea() && nodeTest.HaveArea()) { // Add Node to other Area
                                nodeTest.area.Add(nodeRef);

                            } else { // fusion of Area
                                if (!DBSArea.SameArea(nodeRef.area , nodeTest.area)) { // Not the same Area
                                    areas.Remove(nodeTest.area);
                                    nodeRef.area.Merge(nodeTest.area);
                                }
                            }
                        }
                    }
                }

                //Processing Bar
                if (i % 1000 == 0) {
                    time.Stop();
                    Console.WriteLine(i + "/" + reducedPixelMap.Count + "  Time: " + time.Elapsed.ToString());
                    time.Reset();
                }
            }

            Console.WriteLine("Number of Area : " + areas.Count.ToString());
            
            //Filtering Area which contains < m Nodes
            Console.WriteLine("Filtering Area...");
            foreach (DBSArea area in areas) {
                if (area.Size() < m) {
                    foreach (DBSNode node in area.nodes) {
                        node.area = null;
                    }
                    area.nodes.Clear();
                } else {
               
                    area.AvrageNode();
                }
            }


            //Resume
            Console.WriteLine("Number of Area : " + areas.Count.ToString());
            int temp = 0;
            for (int i = 0 ; i < areas.Count ; i++) {
                temp += areas[i].Size();
            }

            Console.WriteLine("Number of pixel : " + reducedPixelMap.Count.ToString());
            Console.WriteLine("Number of pixel Classified : " + temp.ToString());

            isClustered = true;
        }

        public void Alteration () {
            if (isClustered) {

                alteredPixelMap = new List<DBSNode>();
                int i = 0;

                foreach (DBSNode node in pixelMap) { // for all pixels
                    if (node.area != null) {
                        alteredPixelMap.Add(new DBSNode(node.area.avrageNode.red , node.area.avrageNode.green , node.area.avrageNode.blue));
                    } else {
                        alteredPixelMap.Add(pixelMap[i]);
                    }
                    
                    i++;
                }


            } else {
                Console.WriteLine("You must clusterized your picture before a alteration");
            }
        }

        public Bitmap Draw () {
            if (isClustered) {
                Bitmap newImage = image.bitmap;

                for (int y = 0 ; y < image.height ; y++) {
                    for (int x = 0 ; x < image.width ; x++) {
                        newImage.SetPixel(x , y , alteredPixelMap[image.width * y + x].ToColor());
                    }
                }
                Console.WriteLine("It's Drawn!");
                return newImage;

            } else {
                Console.WriteLine("You must clusterized your picture before drawing");
                return null;
            }
        }

        public void Print () {
            //Creation of new picture

            String newPath = image.path[..^4] + "New" + image.path[^4..]; //Change the name of new file
            Draw().Save(newPath); // Print the picture

        }

        public static bool InCircle (DBSNode a , DBSNode b , float r) {
            if (( a.red - b.red ) * ( a.red - b.red ) + ( a.green - b.green ) * ( a.green - b.green ) + ( a.blue - b.blue ) * ( a.blue - b.blue ) <= r * r) {
                return true;
            } else {
                return false;
            }
        }
    }
}

