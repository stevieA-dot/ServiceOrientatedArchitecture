using System.ServiceModel;

using ServiceOrientatedArchitecture;

namespace Authenticator
{
    [ServiceContract]
    public interface IAuthenticator
    {
        [FaultContract(typeof(ServerFault))]
        [OperationContract]
        string Register(string username, string password);

        [FaultContract(typeof(ServerFault))]
        [OperationContract]
        int Login(string username, string password);

        [FaultContract(typeof(ServerFault))]
        [OperationContract]
        bool Validate(int token);

        [FaultContract(typeof(ServerFault))]
        [OperationContract]
        void ClearTokens();

    }
}
