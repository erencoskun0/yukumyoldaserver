using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yukumyolda.Application.Features.Results.LoadResults
{
    public class GetByIdLoadQueryResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string LoadStatus { get; set; } = default!;

        public DateTime LoadTime { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        // Kalkış ili
        public string Departurev { get; set; } = default!;


        // Varış ili
        public string DestinationProvince { get; set; } = default!; //varış

        public decimal? Weight { get; set; } //ton 
        public decimal? Length { get; set; } //metre
    }
}
