namespace SacreBleu.Levels
{
    class PrototypeDemo : BaseLevel
    {
        private string backgroundFile = @"..\..\..\..\TiledFiles\ExportedMaps\SacreBleu_demo_prototype_Demo.csv";
        private string levelFile = @"..\..\..\..\TiledFiles\ExportedMaps\SacreBleu_demo_Tile Layer 2.csv";

        public PrototypeDemo() : base()
        {
            CreateBackgroundLayout(backgroundFile);
            CreateLevelLayout(levelFile);
        }
    }
}
