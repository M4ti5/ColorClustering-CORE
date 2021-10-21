namespace ColorClustering {
    public class KNode : Pixel {
        public byte? bindedCluster = null;
        public KNode (byte _red , byte _green , byte _blue) : base(_red , _green , _blue) {

        }
    }
}
