using System.Data.Entity;

namespace WpfApp1
{
    public static class Core
    {
        public static up_11Entities Context = new up_11Entities();
        public static User CurrentUser { get; set; }
    }
}