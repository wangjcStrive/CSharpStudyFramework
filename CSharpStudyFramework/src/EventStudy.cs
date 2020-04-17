using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpStudy
{
    class EventStudy
    {
        private int value;

        public delegate void NumManipulationHndler();

        public event NumManipulationHndler ChangeNum;
    }
}
