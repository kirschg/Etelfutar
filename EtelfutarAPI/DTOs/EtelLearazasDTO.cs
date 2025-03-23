using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class EtelLearazasDTO
    {
        public EtelLearazasDTO(Learaza learazas)
        {
            EtelId = learazas.EtelId;
            Learazas = learazas.Learazas;
        }

        public int EtelId { get; set; }
        public int Learazas { get; set; }
    }


}
