namespace keepr.Models
{
    public class VaultKeep
    {
        public int Id { get; set; }
        public int VaultId { get; set; }
        public int KeepId { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
    }
    public class VaultKeepsViewModel : VaultKeep
    {
        public int VaultKeepId { get; set; }
        public string VaultKeepCreatorId { get; set; }
    }
}
// All results include Relationship Id vaultKeepId | AssertionError: Not All Keeps returned with vaultKeepId, be sure to return VaultKeepViewModels: expected false to deeply equal true
