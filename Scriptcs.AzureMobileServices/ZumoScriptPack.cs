using ScriptCs.Contracts;

namespace Scriptcs.AzureMobileServices
{
    public class ZumoScriptPack : IScriptPack
    {
        public void Initialize(IScriptPackSession session)
        {
            session.ImportNamespace("Scriptcs.AzureMobileServices");
        }

        public IScriptPackContext GetContext()
        {
            return new AzureMobileServices();
        }

        public void Terminate()
        {
        }
    }
}