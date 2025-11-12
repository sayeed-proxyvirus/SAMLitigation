namespace SAMLitigation.Models.ViewModel
{
    public class SAM_Litigation_PartyViewModel
    {
        public decimal LitigationPartyID { get; set; }
        public string? LitigationParty { get; set; }
        public string? ProjectName { get; set; }
        public Boolean IsIDCOL { get; set; }
        public Boolean IsCompany { get; set; }
    }
}
