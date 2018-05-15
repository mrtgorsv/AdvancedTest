using AdvancedTest.Data.Model;

namespace AdvancedTest.Utils
{
    public interface ISecurityManager
    {
        User CurrentUser { get; set; }
    }

    public class SecurityManager:ISecurityManager
    {
        public User CurrentUser { get; set; }
    }
}
