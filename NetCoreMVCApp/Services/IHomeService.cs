namespace NetCoreMVCApp.Services
{
    public interface IHomeService
    {
        abstract string GetHomeData();
        public string T1 { get; set; }

        public string Test()
        {
            return "";
        }
    }
}
