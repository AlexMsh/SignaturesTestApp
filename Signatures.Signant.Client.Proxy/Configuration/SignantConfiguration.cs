namespace Signatures.Signant.Client.Proxy.Configuration
{
    public class SignantConfiguration
    {
        public string AccessCode { get; set; }

        public string DistributorId { get; set; }

        /// <summary>
        /// We treat sender as an organization here (hope so)
        /// not an individual user, so that's going to be some configured email group/shared box
        /// </summary>
        public string SenderEmail { get; set; }

        /// <summary>
        /// Sender organization name
        /// </summary>
        public string SenderName { get; set; }
    }
}
