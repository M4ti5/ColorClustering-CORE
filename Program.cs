using System;
using System.Drawing;

namespace ColorClustering {

    class Program {
        static void Main(string[] args) {
            
            Image myImage = new Image(@"..\..\..\Img\grogu.png");

            //KMeans test = new KMeans(myImage, 8, 5, "Euclidian"); 
            //KMeans test = new KMeans(myImage, 64, 50, "Euclidian");
            //KMeans test = new KMeans(myImage, 8, 5, "Manhattan");
            
            

            DBScan test = new DBScan(myImage , 3 , 5);


            test.Print(); // Print at same path of the image
        }
    }

}
