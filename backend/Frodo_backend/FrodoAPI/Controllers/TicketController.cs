using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using FrodoAPI.Domain;
using FrodoAPI.JourneyRepository;
using FrodoAPI.TicketRepository;
using FrodoAPI.UserRepository;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace FrodoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        private readonly ITicketProvider _ticketProvider;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJourneyRepository _journeyRepository;

        public TicketController(ITicketProvider ticketProvider, ITicketRepository ticketRepository, IUserRepository userRepository, IJourneyRepository journeyRepository)
        {
            _ticketProvider = ticketProvider;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _journeyRepository = journeyRepository;
        }

        public class TicketParameters
        {
            public Guid UserId { get; set; }
            public Guid Journey { get; set; }
        }

        public class TicketResult
        {
            public Guid Id { get; set; }
            public Ticket[] Tickets { get; set; }
        }

        [HttpGet("SetupDemo")]
        public Guid SetupDemoJourney()
        {
            return _journeyRepository.AddJourney(new Journey
            {
                Stages = new List<JourneyStage>()
                {
                    new JourneyStage {From = new GeoPoint(), To = new GeoPoint(), TransportCompanyId = 1},
                    new JourneyStage {From = new GeoPoint(), To = new GeoPoint(), TransportCompanyId = 2},
                }
            });
        }


        [HttpGet("PossibleTickets")]
        public TicketResult GetPossibleTickets(Guid userId, Guid journeyId)
        {
            var ticketRequest = new TicketParameters
            {
                Journey = journeyId,
                UserId = userId
            };
            var journey = _journeyRepository.GetJourney(ticketRequest.Journey);

            var results = journey.Stages.Select(s => _ticketProvider.GetTicketForStage(s)).ToArray();

            var requestId = _ticketRepository.Add(ticketRequest.Journey, results);

            return new TicketResult
            {
                Id = requestId,
                Tickets = results
            };
        }

        [HttpGet("Buy")]
        public void BuyTickets(Guid bundleId)
        {
            _ticketRepository.Persist(bundleId);
        }


        [HttpGet("CurrentTicket")]
        public ValidateableTicket GetCurrentTicket(Guid journeyId)
        {
            return _ticketRepository.GetForCurrentStage(journeyId, DateTime.Now);
        }

        [HttpGet("Barcode")]
        public IActionResult GetBarcode(Guid journeyId)
        {
            var ticket = _ticketRepository.GetForCurrentStage(journeyId, DateTime.Now);
            var qrGenerator = new QRCodeGenerator();
            var qrCodeInfo = qrGenerator.CreateQrCode(ticket.BarcodeData, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeInfo);
            var qrBitmap = qrCode.GetGraphic(60);
            byte[] bitmapArray = qrBitmap.BitmapToByteArray();

            return File(bitmapArray, "image/jpeg");
        }
    }

    public static class BitmapExtension
    {
        public static byte[] BitmapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }

}