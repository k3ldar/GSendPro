using AppSettings;

namespace gsend.pro.Internal
{
    public class ApplicationOverride : IApplicationOverride
    {
        public bool ExpandApplicationVariable(string variableName, ref object value)
        {
            //throw new NotImplementedException();
            return false;
        }
    }
}
