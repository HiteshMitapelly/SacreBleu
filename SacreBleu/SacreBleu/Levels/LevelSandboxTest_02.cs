namespace SacreBleu.Levels
{
    class LevelSandboxTest_02 : BaseLevel
    {
        private string levelFile = @"..\..\..\..\TiledFiles\ExportedMaps\SandboxMap_props.csv";

        public LevelSandboxTest_02() : base()
        {
            CreateLevelLayout(levelFile);
        }
    }
}
