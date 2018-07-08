using AdvancedTest.Data.Model;

namespace AdvancedTest.Common.Utils
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
