
using System.Drawing;

namespace ColorClustering {
    class Pixel {
        public byte red;
        public byte green;
        public byte blue;
        

        public Pixel(byte _red, byte _green, byte _blue) {
            red = _red;
            green = _green;
            blue = _blue;

        }

        public void setPixel(byte _red, byte _green, byte _blue) {
            red = _red;
            green = _green;
            blue = _blue;
        }
        public Color ToColor() {
            return Color.FromArgb(red, green, blue);
        }
    }
}
