namespace EtelfutarAPI.DTOs
{
    public class FelhasznalokPutRestrictedDTO
    {
        public int Id { get; set; }
        public string FelhasznaloNev { get; set; }
        public string TeljesNev { get; set; }
        public string Email { get; set; }
        public int VarosId { get; set; }
        public string Lakcim { get; set; }
    }
}
