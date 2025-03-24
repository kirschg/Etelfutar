using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class EtelekLearazasDTO
    {
        public EtelekLearazasDTO(Learaza data)
        {
            EtteremId = data.EtteremId;
            EtelId = data.EtelId;
            Learazas = data.Learazas;
        }
        public int EtteremId { get; set; }

        public int EtelId { get; set; }

        public int Learazas { get; set; }

    }
}
