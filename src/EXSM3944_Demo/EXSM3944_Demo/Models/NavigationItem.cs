namespace EXSM3944_Demo.Models
{
    public class NavigationItem
    {
        public NavigationItem(string name, string controller, string action, string id = "")
        {
            Name = name;
            Controller = controller;
            Action = action;
            ID = id;
        }
        public string Name = "";
        public string Controller = "";
        public string Action = "";
        public string ID = "";
    }
}
