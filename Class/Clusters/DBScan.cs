using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColorClustering {
    class DBScan {
        private Image image;
        private List<DBSNode> pixelMap = new List<DBSNode>();
        //private List<DBSNode> reducePixelMap = new List<DBSNode>();

        public DBScan (Image _image) {
            image = _image;
            byte[] redMap = image.GetRedMap();
            byte[] greenMap = image.GetGreenMap();
            byte[] blueMap = image.GetBlueMap();

            for (int i = 0 ; i < image.width * image.height ; i++) {
                pixelMap.Add(new DBSNode(redMap[i] , greenMap[i] , blueMap[i]));
            }

            Clustering(3 , 10);

        }

        public void Clustering (int m , float r) {

            List<DBSArea> areas = new List<DBSArea>(); // List of area 

            // Attribution of each Pixel
            for (int i = 0 ; i < pixelMap.Count ; i++) {
                for (int j = i + 1 ; j < pixelMap.Count ; j++) {
                    if (InCircle(pixelMap[i] , pixelMap[j] , r)) {

                        if (!pixelMap[i].HaveArea() && !pixelMap[j].HaveArea()) { // Create Area
                            pixelMap[i].area = new DBSArea(pixelMap[i]);
                            pixelMap[j].area = pixelMap[i].area;
                            pixelMap[i].area.Add(pixelMap[j]);

                            areas.Add(pixelMap[i].area);

                        } else if (pixelMap[i].HaveArea() && !pixelMap[j].HaveArea()) { // Add Node to own Area
                            pixelMap[i].area.Add(pixelMap[j]);
                        } else if (!pixelMap[i].HaveArea() && pixelMap[j].HaveArea()) { // Add Node to other Area
                            pixelMap[j].area.Add(pixelMap[i]);

                        } else { // fusion of Area
                            if (!DBSArea.SameArea(pixelMap[i].area , pixelMap[j].area)) { // Not the same Area
                                    areas.Remove(pixelMap[j].area);
                                    pixelMap[i].area.Merge(pixelMap[j].area);
                                
                            }
                        }

                    }
                }
                if (i % 1000 == 0) {

                    Console.WriteLine(i.ToString() + "/" + pixelMap.Count.ToString() + "  Time: " + DateTime.Now.ToString());
                }
            }
            

            //Check
            Console.WriteLine("Nombre de pixel" + pixelMap.Count.ToString());
            Console.WriteLine("Nombre de Area" + areas.Count.ToString());
            int temp = 0;
            for (int i = 0 ; i < areas.Count ; i++) {
                temp += areas[i].Size();
            }
            Console.WriteLine("Nombre de pixel dans Areas" + temp.ToString());


        }

        public static bool InCircle (DBSNode a , DBSNode b , float r) {
            if (Math.Pow(a.red - b.red , 2) + Math.Pow(a.green - b.green , 2) + Math.Pow(a.blue - b.blue , 2) <= Math.Pow(r , 2)) {
                return true;
            } else {
                return false;
            }
        }
    }
}
