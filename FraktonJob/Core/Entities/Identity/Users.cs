namespace Core.Entities.Identity
{
    public class Users
    {
        #region Properties
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        #endregion
    }
}
