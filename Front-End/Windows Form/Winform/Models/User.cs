namespace TaskManagment.Models
{
    public  class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int StatusId { get; set; }

        public string EMail { get; set; }

        public int? ManagerId { get; set; }

    }
}
