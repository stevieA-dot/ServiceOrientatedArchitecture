using System.ServiceModel;


namespace Authenticator
{
    [ServiceContract]
    public interface IAuthenticator
    {
        [OperationContract]
        string Register(string username, string password);

        [OperationContract]
        int Login(string username, string password);

        [OperationContract]
        bool Validate(int token);

        void ClearTokens();

    }
}
