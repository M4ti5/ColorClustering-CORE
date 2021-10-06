using System;
using System.Drawing;
using System.Linq;

namespace ColorClustering {
    public class KMeans {

        private Image image;

        private Pixel[] pixelMap;
        private Pixel[] alteredPixelMap;

        private byte[] redMap;
        private byte[] greenMap;
        private byte[] blueMap;

        private Pixel[] clusters;
        private bool isClustered = false;

        public KMeans(Image _image, byte k, int t, String method) {
            image = _image;
            redMap = image.GetRedMap();
            greenMap = image.GetGreenMap();
            blueMap = image.GetBlueMap();

            pixelMap = new Pixel[image.height * image.width];
            alteredPixelMap = new Pixel[image.width * image.height];

            for (int i = 0; i < image.width * image.height; i++) {
                pixelMap[i] = new Pixel(redMap[i], greenMap[i], blueMap[i]);
            }

            Clustering(k, t, method);
            Console.WriteLine("Clustering it's Okey!");
            Alteration();
            Console.WriteLine("Alteration it's Okey!");
            Draw();
            Console.WriteLine("It's Drawn!");
        }

        public void Clustering(byte k, int t, String method) {
            /*
			 * k : number of cluster
			 * t : number of iteration
			 */

            clusters = new Pixel[k];
            Random random = new();

            //Creation of Clusters with Random Position 
            for (byte ki = 0; ki < k; ki++) {
                byte[] randomNumbers = new byte[3];
                random.NextBytes(randomNumbers);
                clusters[ki] = new Pixel(randomNumbers[0], randomNumbers[1], randomNumbers[2]);
            }

            //Clear bind with old Clusters
            if (isClustered) {
                for (int pi = 0; pi < image.width * image.height; pi++) { // for all pixels
                    pixelMap[pi].bindedCluster = null;
                }
            }



            //Iteration Loop
            for (int i = 0; i < t; i++) {

                int[,] kSumPosition = new int[k, 3];
                int[] kElement = new int[k];

                //Binding of pixel with a cluster
                for (int pi = 0; pi < image.width * image.height; pi++) { // for all pixels

                    //Decision of binding
                    double[] kDistance = new double[k];

                    for (byte ki = 0; ki < k; ki++) {// for all clusters

                        if(method == "Manhattan" || method == "MANHATTAN") {
                            kDistance[ki] = ManhattanDistance(clusters[ki], pixelMap[pi]);
                        
                        } else {
                            kDistance[ki] = EuclidianDistance(clusters[ki], pixelMap[pi]);
                        }
                    }

                    pixelMap[pi].bindedCluster = (byte)kDistance.ToList().IndexOf(kDistance.Min());


                    //Pre-calcul of future cluster's position

                    kSumPosition[(int)pixelMap[pi].bindedCluster, 0] += pixelMap[pi].red;//Red sum position
                    kSumPosition[(int)pixelMap[pi].bindedCluster, 1] += pixelMap[pi].green;//Green sum position
                    kSumPosition[(int)pixelMap[pi].bindedCluster, 2] += pixelMap[pi].blue;//Blue sum position

                    kElement[(int)pixelMap[pi].bindedCluster] += 1;
                }

                //Shifting clusters' position

                for (byte ki = 0; ki < k; ki++) {// for all clusters
                    clusters[ki].setPixel((byte)Math.Round((double)kSumPosition[ki, 0] / kElement[ki]),
                                            (byte)Math.Round((double)kSumPosition[ki, 1] / kElement[ki]),
                                            (byte)Math.Round((double)kSumPosition[ki, 2] / kElement[ki]));
                }

            }

            //Print all colors of clusters
            Console.WriteLine("We have " + k + " Cluster(s)");
            for (byte ki = 0; ki < k; ki++) {// for all clusters
                Pixel temp = clusters[ki];
                Console.WriteLine("Cluster Color"+ki+" : R" + temp.red +  " G" + temp.green + " B" + temp.blue);
            }

            isClustered = true;
        }

        private double EuclidianDistance(Pixel a, Pixel b) {

            return Math.Pow(Math.Pow(a.red - b.red, 2) + Math.Pow(a.red - b.red, 2) + Math.Pow(a.green - b.green, 2) + Math.Pow(a.blue - b.blue, 2), 0.5);

        }

        private double ManhattanDistance(Pixel a, Pixel b) {

            return Math.Abs(a.red - b.red) + Math.Abs(a.green - b.green) + Math.Abs(a.blue - b.blue);

        }

        public void Alteration() {
            if (isClustered) {
                
                for (int pi = 0; pi < image.width * image.height; pi++) { // for all pixels
                    Pixel ki = clusters[(byte)pixelMap[pi].bindedCluster];

                    alteredPixelMap[pi] = new Pixel(ki.red, ki.green, ki.blue);
                }
                

            } else {
                Console.WriteLine("You must clusterized your picture before a alteration");
            }
        }

        public Bitmap Draw() {
            if (isClustered) {
                Bitmap newImage = image.bitmap;

                for (int y = 0; y < image.height; y++) {
                    for (int x = 0; x < image.width; x++) {
                        newImage.SetPixel(x, y, alteredPixelMap[image.width * y + x].ToColor());
                    }
                }

                //Creation of new picture
                String newPath = image.path[..^4] + "New" + image.path[^4..]; //Change the name of new file
                newImage.Save(newPath); // Print the picture


                return newImage;

            } else {
                Console.WriteLine("You must clusterized your picture before drawing");
                return null;
            }
        }


    }
}

