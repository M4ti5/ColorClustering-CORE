using System;
using System.Drawing;

namespace ColorClustering {

    class Program {
        static void Main(string[] args) {
            
            Image myImage = new Image(@"..\..\..\Img\lotus.png");

            new KMeans(myImage, 8, 5, "Euclidian");

            //new KMeans(myImage, 8, 5, "Manhattan");
        }
    }

}
