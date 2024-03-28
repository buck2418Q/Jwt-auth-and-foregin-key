using System.ComponentModel.DataAnnotations.Schema;

namespace STSPro.Model
{
    public class SimCard
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserModelId")]
        public UserModel User { get; set; }


        public int ProviderId { get; set; }
        [ForeignKey("ProviderModelId")]
        public ProviderModel ProviderModel { get; set; }
        public string IsActiveUser  { get; set; }
        public int DevicesId { get; set; }
        [ForeignKey("DevicesId")]
        public Devices Devices { get; set; }

    }
}
