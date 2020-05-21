using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpStudyFramework.src
{
    class IndexerStudy
    {
        static public int size = 10;
        private string[] nameList = new string[size];

        public IndexerStudy()
        {
            for (int i = 0; i < size; i++)
                nameList[i] = "N.A";
        }

        public string this[int index]
        {
            get
            {
                string tmp;
                if (index >= 0 && index <= size - 1)
                    tmp = nameList[index];
                else
                    tmp = "";
                return tmp;
            }
            set
            {
                if (index >= 0 && index <= size - 1)
                    nameList[index] = value;
            }
        }
    }
}
