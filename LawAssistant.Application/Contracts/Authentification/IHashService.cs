namespace LawAssistant.Application.Contracts
{
    public interface IHashService
    {
        public string Hash(string str);

        public bool Verify(string str, string hashedStr);
    }
}
