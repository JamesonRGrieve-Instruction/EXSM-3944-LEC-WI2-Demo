namespace EXSM3944_Demo.Models
{
    public class NavigationItem
    {
        public NavigationItem(string name, string controller, string action)
        {
            Name = name;
            Controller = controller;
            Action = action;
        }
        public string Name = "";
        public string Controller = "";
        public string Action = "";
    }
}
