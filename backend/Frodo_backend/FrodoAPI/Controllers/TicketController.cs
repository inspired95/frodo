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
        private readonly ITransportCompanyRepo _transportCompanyRepo;

        public TicketController(ITicketProvider ticketProvider, ITicketRepository ticketRepository, IUserRepository userRepository, IJourneyRepository journeyRepository, ITransportCompanyRepo transportCompanyRepo)
        {
            _ticketProvider = ticketProvider;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _journeyRepository = journeyRepository;
            _transportCompanyRepo = transportCompanyRepo;
        }

        public class TicketParameters
        {
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
                    new JourneyStage {From = new GeoPoint(), To = new GeoPoint(), TransportCompanyId = _transportCompanyRepo.GetAll().First().Id, StartingTime = DateTime.Now, TravelTime = TimeSpan.FromMinutes(1)},
                    new JourneyStage {From = new GeoPoint(), To = new GeoPoint(), TransportCompanyId = _transportCompanyRepo.GetAll().Last().Id, TravelTime = TimeSpan.FromMinutes(2)},
                }
            });
        }


        [HttpGet("PossibleTickets")]
        public TicketResult GetPossibleTickets(Guid journeyId)
        {
            var ticketRequest = new TicketParameters
            {
                Journey = journeyId,
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

        [HttpGet("AllTickets")]
        public IEnumerable<ValidateableTicket> GetAllTickets(Guid journeyId)
        {
            return _ticketRepository.GetAllTickets(journeyId);
        }

        [HttpGet("Barcode")]
        public IActionResult GetBarcode(Guid journeyId, Guid ticketId)
        {
            var ticket = _ticketRepository.Get(journeyId, ticketId);

            if (ticket == null)
                return null;

            var qrGenerator = new QRCodeGenerator();
            var qrCodeInfo = qrGenerator.CreateQrCode(ticket.BarcodeData, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeInfo);
            var qrBitmap = qrCode.GetGraphic(60);
            byte[] bitmapArray = qrBitmap.BitmapToByteArray();

            return File(bitmapArray, "image/jpeg");
        }


        [HttpGet("GetCurrentBarcode")]
        public IActionResult GetCurrentBarcode(Guid journeyId)
        {
            var ticket = _ticketRepository.GetForCurrentStage(journeyId, DateTime.Now);

            if (ticket == null)
                return null;

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