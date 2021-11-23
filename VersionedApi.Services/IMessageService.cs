using System.Threading.Tasks;

namespace VersionedApi.Services
{
    /// <summary>
    /// Interface for performing messagging operations
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Returns a great hello world message
        /// </summary>
        /// <returns></returns>
        Task<string> GetMessage();
    }

    /// <summary>
    /// Separate interface for versioned Message Services
    /// </summary>
    public interface IVersionedMessageService : IMessageService
    {
        public string Version { get; }
    }
}
