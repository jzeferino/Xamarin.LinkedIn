using System;
namespace Xamarin.iOS.LinkedIn
{
    /// <summary>
    /// Valid permissions
    /// </summary>
    public static class Permission
    {
        /// <summary>
        /// permission to retrieve name, photo, headline and current position
        /// </summary>
        public const string BasicProfile = "r_basicprofile";

        /// <summary>
        /// permission to retrieve full profile including experience, education, skills and recommendations
        /// This permission is not open to all developers.
        /// </summary>
        public const string FullProfile = "r_fullprofile";

        /// <summary>
        /// permission to retrieve email address
        /// </summary>
        public const string EmailAddress = "r_emailaddress";

        /// <summary>
        /// permission to post updates, make comments and like posts
        /// </summary>
        public const string Share = "w_share";

        /// <summary>
        /// permission to retrieve address, phone number, and bound accounts
        /// This permission is not open to all developers.
        /// </summary>
        public const string ContactInfo = "r_contactinfo";

        /// <summary>
        /// permission to edit company pages for which I am an Admin and post status updates on behalf of those companies
        /// </summary>
        public const string CompanyAdmin = "rw_company_admin";
    }

}
