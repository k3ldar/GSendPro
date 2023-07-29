using System;

using SharedPluginFeatures;

namespace gsend.pro.Internal
{
    public class ErrorManagerProvider : IErrorManager
    {
        public void ErrorRaised(in ErrorInformation errorInformation)
        {

        }

        public bool MissingPage(in string path, ref string replacePath)
        {
            if (path.Equals("/home", StringComparison.InvariantCultureIgnoreCase))
            {
                replacePath = "/";
                return true;
            }
            if (path.Equals("/home/index", StringComparison.InvariantCultureIgnoreCase))
            {
                replacePath = "/";
                return true;
            }

            return false;
        }
    }
}
