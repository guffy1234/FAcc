namespace FuelAcc.Constants
{
    public static class Constants
    {
        public const string AdminUserName = "admin";
        public const string AdminPassword = "Admin!1";

        public const string AdminRoleName = "admin";
        public const string AdminEmail = "admin@gmail.com";
        public const string EmployeeRoleName = "employee";

        public const string BranchName = "Main";

        public static readonly Guid PartnersRootFolderId = new Guid();
        public const string PartnersRootFolderName = "Partners root";

        public static readonly Guid ProductsRootFolderId = new Guid();
        public const string ProductsRootFolderName = "Products root";

        public static readonly Guid FileBlobsRootFolderId = new Guid();
        public const string FileBlobsRootFolderName = "FileBlobs root";

        public static readonly KeyValuePair<Guid, string>[] Roots = new[] {
            new KeyValuePair<Guid, string>(PartnersRootFolderId, PartnersRootFolderName),
            new KeyValuePair<Guid, string>(ProductsRootFolderId, ProductsRootFolderName),
            new KeyValuePair<Guid, string>(FileBlobsRootFolderId, FileBlobsRootFolderName),
        };
    }
}