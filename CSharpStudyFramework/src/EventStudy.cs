using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpStudyFramework.src
{
    class EventStudy
    {
        private int value;

        public delegate void NumManipulationHndler();

        public event NumManipulationHndler ChangeNum;
    }
}
