using System;
using System.Collections.Generic;

namespace Command
{
    [Serializable]
    public struct Command
    {
        public string Name;
        public List<string> Parmas;

        public Command(string name, List<string> parmas)
        {
            Name = name;
            Parmas = parmas;
        }
    }
}