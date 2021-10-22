using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace ColorClustering {
    public class KMeans {

        private Image image;

        private List<KNode> pixelMap = new List<KNode>();
        private List<KNode> alteredPixelMap = new List<KNode>();
        private List<Pixel> reducedPixelMap = new List<Pixel>();

        private PixelTree pixelTree;

        private byte[] redMap;
        private byte[] greenMap;
        private byte[] blueMap;

        private KNode[] clusters;
        private bool isClustered = false;

        public KMeans (Image _image , byte k , int t , String method) {
            Stopwatch time = new Stopwatch();
            Stopwatch mainTime = new Stopwatch();

            Console.WriteLine("Kmeans Start ...");
            mainTime.Start();

            image = _image;
            redMap = image.GetRedMap();
            greenMap = image.GetGreenMap();
            blueMap = image.GetBlueMap();

            for (int i = 0 ; i < image.width * image.height ; i++) {
                pixelMap.Add(new KNode(redMap[i] , greenMap[i] , blueMap[i]));
            }

            Console.WriteLine("Optimizing ...");
            time.Start();

            pixelTree = KNode.DeleteDuplicate(pixelMap);

            Console.WriteLine("Factoring ...");

            reducedPixelMap = pixelTree.ToList();
            Console.WriteLine("Factoring it's Okey!");

            time.Stop();
            Console.WriteLine("Optimizing it's Okey!");
            Console.WriteLine("Optimizing time : " + time.Elapsed.ToString());
            Console.WriteLine("Optimizing percent : " + ( (float)reducedPixelMap.Count / (float)pixelMap.Count ) * 100 + " %");


            Console.WriteLine();
            Console.WriteLine("Clustering with " + method + "...");
            Clustering(k , t , method);
            time.Stop();
            Console.WriteLine("Clustering it's Okey!");
            Console.WriteLine("Clustering time : " + time.Elapsed.ToString());
            Console.WriteLine("Clustering it's Okey!");

            time.Restart();
            Console.WriteLine("Alteration...");
            Alteration();
            time.Stop();
            Console.WriteLine("Alteration it's Okey!");
            Console.WriteLine("Alteration time : " + time.Elapsed.ToString());

            mainTime.Stop();
            Console.WriteLine("Kemeans time : " + mainTime.Elapsed.ToString());
        }

        public void Clustering (byte k , int t , String method) {
            /*
			 * k : number of cluster
			 * t : number of iteration
			 */

            clusters = new KNode[k];
            Random random = new();

            //Creation of Clusters with Random Position 
            for (byte ki = 0 ; ki < k ; ki++) {
                byte[] randomNumbers = new byte[3];
                random.NextBytes(randomNumbers);
                clusters[ki] = new KNode(randomNumbers[0] , randomNumbers[1] , randomNumbers[2]);
            }

            //Clear bind of old Clustering
            if (isClustered) {
                foreach (KNode pixel in reducedPixelMap) { // for all pixels
                    pixel.bindedCluster = null;
                }
            }


            //Iteration Loop
            for (int i = 0 ; i < t ; i++) {

                int[,] kSumPosition = new int[k , 3];
                int[] kElement = new int[k];

                //Binding of pixel with a cluster
                for (int pi = 0 ; pi < reducedPixelMap.Count ; pi++) { // for all pixels

                    //Decision of binding
                    double[] kDistance = new double[k];
                    KNode pixel = (KNode)reducedPixelMap[pi];

                    for (byte ki = 0 ; ki < k ; ki++) {// for all clusters

                        if (method == "Manhattan" || method == "MANHATTAN") {
                            kDistance[ki] = ManhattanDistance(clusters[ki] , pixel);

                        } else {
                            kDistance[ki] = EuclidianDistance(clusters[ki] , pixel);
                        }
                    }

                    pixel.bindedCluster = (byte)kDistance.ToList().IndexOf(kDistance.Min());


                    //Pre-calcul of future cluster's position

                    kSumPosition[(int)pixel.bindedCluster , 0] += pixel.red * pixel.weight;//Red sum position
                    kSumPosition[(int)pixel.bindedCluster , 1] += pixel.green * pixel.weight;//Green sum position
                    kSumPosition[(int)pixel.bindedCluster , 2] += pixel.blue * pixel.weight;//Blue sum position

                    kElement[(int)pixel.bindedCluster] += pixel.weight;
                }

                //Shifting clusters' position

                for (byte ki = 0 ; ki < k ; ki++) {// for all clusters
                    if (kElement[ki] != 0) {
                        byte r = (byte)( ( kSumPosition[ki , 0] / kElement[ki] ) );
                        byte g = (byte)( ( kSumPosition[ki , 1] / kElement[ki] ) );
                        byte b = (byte)( ( kSumPosition[ki , 2] / kElement[ki] ) );
                        clusters[ki].setPixel(r , g , b);
                    }

                }
                Console.WriteLine("End of iteration " + ( i + 1 ) + "/" + t);
            }
            
            //Print all colors of clusters
            Console.WriteLine("--------------------- Cluster's Values --------------------- ");
            Console.WriteLine("We have " + k + " Cluster(s)");
            for (byte ki = 0 ; ki < k ; ki++) {// for all clusters
                KNode temp = clusters[ki];
                Console.WriteLine("Cluster " + ki + " Color " + " : R" + temp.red + " G" + temp.green + " B" + temp.blue);
            }
            Console.WriteLine("------------------------------------------------------------ ");
            
            isClustered = true;
        }

        private double EuclidianDistance (KNode a , KNode b) {

            return Math.Pow(( a.red - b.red ) * ( a.red - b.red ) + ( a.green - b.green ) * ( a.green - b.green ) + ( a.blue - b.blue ) * ( a.blue - b.blue ) , 0.5);

        }

        private double ManhattanDistance (KNode a , KNode b) {

            return ( ( a.red - b.red ) > 0 ? ( a.red - b.red ) : -( a.red - b.red ) )
                    + ( ( a.green - b.green ) > 0 ? ( a.green - b.green ) : -( a.green - b.green ) )
                    + ( ( a.blue - b.blue ) > 0 ? ( a.blue - b.blue ) : -( a.blue - b.blue ) );

        }

        public void Alteration () {
            if (isClustered) {

                alteredPixelMap = new List<KNode>();

                foreach (KNode node in pixelMap) { // for all pixels
                    KNode workedPixel = (KNode)pixelTree.SearchNode(pixelTree.root , node).node;
                    KNode ki = clusters[(byte)workedPixel.bindedCluster];
                    alteredPixelMap.Add(new KNode(ki.red , ki.green , ki.blue));
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

    }
}

