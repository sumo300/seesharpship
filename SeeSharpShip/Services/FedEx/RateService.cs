using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeeSharpShip.FedExRateService;

namespace SeeSharpShip.Services.FedEx
{
    public class RateService
    {
        //private RateReply CallRateService(RateRequest request) { return new RateReply(); }
        public RateResponse Get(RateRequest request) {
            var service = new FedExRateService.RateService();
            var response = service.getRates(request);


        }
    }
}
