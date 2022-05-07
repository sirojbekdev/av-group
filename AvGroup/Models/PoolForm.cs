using System.ComponentModel.DataAnnotations;

namespace AvGroup.Models
{
    public class PoolForm
    {
        [Required]
        public float width { get; set; }
        [Required]
        public float length { get; set; }
        [Required]
        public float depth { get; set; }
        [Required]
        public string PoolType { get; set; }
        [Required]
        public bool Waterheating { get; set; }
        [Required]
        public bool Lighting { get; set; }
        [Required]
        public string Majolica { get; set; }
        [Required]
        public int GeyserNumber { get; set; } = 0;
        [Required]
        public int StairsNumber { get; set; } = 0;
        [Required]
        public bool Waterslides { get; set; }
        [Required]
        public bool Cobra { get; set; }
        [Required]
        public bool ContactMe { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Location { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

        public float GetBasement()
        {
            float basement = (float)((width * depth * length) * 0.8);
            return basement;
        }
        public float GetEstimate()
        {
            float bsment = GetBasement();
            float estimate = bsment;
            if (PoolType == "skimmer")
            {
                estimate *= 45;
            }
            else
            {
                estimate *= 40;
            }
            if (Waterheating)
            {
                if (bsment <= 30)
                {
                    estimate += 200;
                }
                else if(bsment>30 && bsment < 60)
                {
                    estimate += 450;
                }
                else
                {
                    estimate += (bsment / 30) * 200;
                }
            }
            if (Cobra)
            {
                estimate += 110;
            }
            if (Waterslides)
            {
                estimate += 150;
            }
            if (Lighting)
            {
                estimate += (bsment/3)*10;
            }
            if (Majolica=="local")
            {
                estimate += (bsment * 4);
            }
            else
            {
                estimate += (bsment * 5);
            }
            if (GeyserNumber > 0)
            {
                estimate += GeyserNumber * 110;
            }
            if(StairsNumber > 0)
            {
                estimate += StairsNumber * 20;
            }
            return estimate;
        }
    }

}
