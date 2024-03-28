using System.ComponentModel.DataAnnotations.Schema;

namespace STSPro.Model
{
    public class Devices
    {
        public int Id { get; set; }
        public string DeviceName{ get; set; }
        

        [ForeignKey("SimCardId")]
        public int SimIDNumber { get; set; }


    }
}
