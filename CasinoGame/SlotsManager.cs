using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CasinoGame
{
    class SlotsManager
    {
        private SlotData[] allSlots;

        public SlotsManager(SlotData[] allSlots)
        {
            this.allSlots = allSlots;
        }
    }

    class SlotData
    {
        public Image image { private set; get; }
        public string name { private set; get; }
        
        public SlotData(string imagePath, string name)
        {
            this.image = Image.FromFile(imagePath);
            this.name = name;
        }
    }
}
