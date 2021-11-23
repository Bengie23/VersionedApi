using System.Threading.Tasks;

namespace VersionedApi.Services.V1
{
    /// <summary>
    /// V1 implementation of IMessageService
    /// </summary>
    public class MessageService : IVersionedMessageService
    {
        public MessageService()
        {
            this.Version = "v1";
        }

        public string Version { get; }

        ///<inheritdoc cref="IMessageService.GetMessage"/>
        public Task<string> GetMessage()
        {
            return Task.FromResult("Hello World");
        }
    }
}
