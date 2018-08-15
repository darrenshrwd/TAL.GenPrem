using ServiceStack;
using TAL.GenPrem.ServiceModel;

namespace TAL.GenPrem.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}
