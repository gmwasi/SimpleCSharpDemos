using System;

namespace Msmq
{
    [Serializable]
    class SampleData
    {
        private string text = "Queue Message";

        public SampleData()
        {
        }

        public SampleData(string _text)
        {
            text = _text;
        }

        public string GetText()
        {
            return text;
        }
    }
}