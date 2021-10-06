using System;
using System.Drawing;

namespace ColorClustering {

    class Program {
        static void Main(string[] args) {
            Image myimage = new Image(@"..\..\..\img\grogu.png");
            
            KMeans K = new KMeans(myimage);
            K.Clustering(8 , 5);
            Console.WriteLine("Clustering it's Okey!");
            K.Alteration();
            Console.WriteLine("Alteration it's Okey!");
            K.Draw();
            Console.WriteLine("it's Okey!");
            

        }
    }

}
