
using Plutus.Interfaces;

namespace Plutus
{
    public class CurrentInfoHolder : IInfoHolder
    {
        public string CurrentType { get; set; }
        public string CurrentCategory { get; set; }
        public string CurrentName { get; set; }
        public string CurrentAmout { get; set; }
    }
}
