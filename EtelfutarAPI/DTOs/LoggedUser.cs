namespace VizsgaremekAPI.DTOs
{
    public class LoggedUser
    {
        public string FelhasznaloNev { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int Jogosultsag { get; set; }

        public int VarosId { get; set; }

        public string ProfilKepUtvonal { get; set; } = null!;

        public string Token { get; set; }
    }
}
