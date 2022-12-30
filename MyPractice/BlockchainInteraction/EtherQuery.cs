using System;

namespace BlockchainInteraction
{
    public class EtherQuery
    {
        public string rpcUrl;
        private ulong idCounter;
        private ulong max;

        public EtherQuery(string rpcUrl)
        {
            this.rpcUrl = rpcUrl;
            this.max = UInt64.MaxValue;
            Random rn = new Random();
            this.idCounter = (ulong)rn.NextInt64(0, Int64.MaxValue);
        }

        public EtherQuery(string rpcUrl, ulong idCounter, ulong max)
        {
            this.rpcUrl = rpcUrl;
            this.idCounter = idCounter;
            this.max = max;
        }
    }
}


