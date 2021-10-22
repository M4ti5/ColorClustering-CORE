using System;
using System.Drawing;

namespace ColorClustering {

    class Program {
        static void Main(string[] args) {
            
            Image myImage = new Image(@"..\..\..\Img\grogu.png");
            
            //KMeans test = new KMeans(myImage, 8, 5, "Euclidian");
            KMeans test = new KMeans(myImage, 8, 5, "Manhattan");
            test.Print(); // Print at same path of the image
            

            //new DBScan(myImage , 3 , 5);

        }
    }

}
