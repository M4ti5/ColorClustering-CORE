using System;
using System.Drawing;

namespace ColorClustering {

public class Image {

    public Bitmap bitmap;
    public int height;
    public int width;

    public String path;

    public Image(String _path) {

        if (_path == string.Empty || _path == null) {
            Console.WriteLine("Chemin absolu de votre image : ");
            _path = Console.ReadLine();
        } else {
            path = _path;
        }

        bitmap = new Bitmap(_path);

        height = bitmap.Height;
        width = bitmap.Width;
        
    }

    

    public byte[] GetRedMap() {
        byte[] redMap = new byte[width* height];
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                redMap[width * y + x] = bitmap.GetPixel(x, y).R;
            }
        }
        return redMap;
    }
    public byte[] GetGreenMap() {
        byte[] greenMap = new byte[width*height];
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                greenMap[width * y + x] = bitmap.GetPixel(x, y).G;
            }
        }
        return greenMap;
    }
    public byte[] GetBlueMap() {
        byte[] blueMap = new byte[width*height];
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                blueMap[width * y + x] = bitmap.GetPixel(x, y).B;
            }
        }
          return blueMap;
    }

}
}
