namespace Rusada.Aviation.Core.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Validate Login credentials and return JWT token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> LoginAsync(string username, string password);
    }
}
