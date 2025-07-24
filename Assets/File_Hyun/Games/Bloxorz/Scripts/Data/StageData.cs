using System;

namespace Bloxorz.Data
{
    [Serializable]
    public class StageData
    {
        public int width;
        public int height;
        public string[] map;
        public BlockStartData blockStart;
    }

    [Serializable]
    public class BlockStartData
    {
        public int x;
        public int y;
        public string state;
    }
}