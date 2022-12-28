namespace Lamorenita.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; } = false;
    }
    public class RoleUserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; } = false;
    }

    public class RoleSelectedModel
    {
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}
