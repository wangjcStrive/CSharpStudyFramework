using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpStudy
{
    /********  publisher ********/
    class DelegateAndEventStudyPublisher
    {
        private int value;
        public DelegateAndEventStudyPublisher()
        {
            SetVal(5);
        }

        public delegate void NumManipulationHandler();

        //!!!
        public event NumManipulationHandler ChangeNum;

        protected virtual void OnNumChange()
        {
            if(ChangeNum != null)
            {
                ChangeNum();
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey();
            }
        }

        public void SetVal(int n)
        {
            if(value != n)
            {
                value = n;
                OnNumChange();
            }
        }
    }

    class Subscriber
    {
        public void printf()
        {
            Console.WriteLine("event fire");
            Console.ReadKey();
        }
    }
}
