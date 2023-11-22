namespace PMS.Auth.Model
{
    public static class PMSRoles
    {
        public const string Admin = nameof(Admin);
        public const string PMSUser = nameof(PMSUser);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, PMSUser };

    }
}
