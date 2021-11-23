using System;
using System.Threading.Tasks;

namespace VersionedApi.Services.V2
{
    /// <summary>
    /// V2 implementation of IMessageService
    /// </summary>
    public class MessageService : IVersionedMessageService
    {
        public MessageService()
        {
            this.Version = "v2";
        }

        public string Version { get; }

        ///<inheritdoc cref="IMessageService.GetMessage"/>
        public Task<string> GetMessage()
        {
            return Task.FromResult(Reverse("Hello World"));
        }

        private string Reverse(string input)
        {
            var arr = input.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
