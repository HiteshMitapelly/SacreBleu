namespace SacreBleu.Levels
{
    class LevelSandboxTest : BaseLevel
    {
        private string levelFile = @"..\..\..\..\TiledFiles\ExportedMaps\SandboxMap_props.csv";

        public LevelSandboxTest() : base()
        {
            CreateLevelLayout(levelFile);
        }
    }
}
