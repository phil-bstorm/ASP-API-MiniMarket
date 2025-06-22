namespace MiniMarket.BLL.CustomExceptions
{
    public class UsernameAlreadyUsedException : Exception
    {
        public UsernameAlreadyUsedException() : base("Username already used.")
        {
        }

        public UsernameAlreadyUsedException(string message) : base(message)
        {
        }
    }
}