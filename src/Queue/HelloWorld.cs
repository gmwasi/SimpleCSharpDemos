using System;

namespace Queue
{
    [Serializable]
    class HelloWorld
    {
        private string text = "Queue Message";

        public HelloWorld()
        {
        }

        public HelloWorld(string _text)
        {
            text = _text;
        }

        public string GetText()
        {
            return text;
        }
    }
}