using System;

namespace Pixygon.NFT.Wax {
    [Serializable]
    public class endpoints {
        public string[] nodeEndpoints;
        private int currentIndex = 0;

        public string GetEndpoint {
            get {
                currentIndex++;
                if (currentIndex >= nodeEndpoints.Length)
                    currentIndex = 0;
                return nodeEndpoints[currentIndex];
            }
        }
    }
}