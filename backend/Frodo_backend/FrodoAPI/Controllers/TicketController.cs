using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using FrodoAPI.Domain;
using FrodoAPI.JourneyRepository;
using FrodoAPI.TicketRepository;
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
        private readonly IJourneyRepository _journeyRepository;
        private readonly ITransportCompanyRepo _transportCompanyRepo;
        private readonly IStationRepository _stationRepository;

        public TicketController(ITicketProvider ticketProvider, ITicketRepository ticketRepository, IJourneyRepository journeyRepository, ITransportCompanyRepo transportCompanyRepo, IStationRepository stationRepository)
        {
            _ticketProvider = ticketProvider;
            _ticketRepository = ticketRepository;
            _journeyRepository = journeyRepository;
            _transportCompanyRepo = transportCompanyRepo;
            _stationRepository = stationRepository;
        }

        public class TicketResult
        {
            public Guid Id { get; set; }
            public Ticket[] Tickets { get; set; }
            public double TotalPrice { get; set; }
        }

        [HttpGet("SetupDemo")]
        public Guid SetupDemoJourney()
        {
            var allStations = _stationRepository.GetAllStations();

            return _journeyRepository.AddJourney(new Journey
            {
                GUID = Guid.NewGuid(),
                Stages = new List<JourneyStage>()
                {
                    new JourneyStage {From = new JourneyPoint(allStations[0]), To = new JourneyPoint(allStations[1]), TransportCompanyId = _transportCompanyRepo.GetAll().First().Id, StartingTime = DateTime.Now, TravelTime = TimeSpan.FromMinutes(1)},
                    new JourneyStage {From = new JourneyPoint(allStations[1]), To = new JourneyPoint(allStations[2]), TransportCompanyId = _transportCompanyRepo.GetAll().Last().Id, StartingTime = DateTime.Now.AddMinutes(1), TravelTime = TimeSpan.FromMinutes(2)},
                }
            });
        }


        [HttpGet("PossibleTickets")]
        public TicketResult GetPossibleTickets(Guid journeyId)
        {
            var journey = _journeyRepository.GetJourney(journeyId);

            var results = journey.Stages.Select(s => _ticketProvider.GetTicketForStage(s)).ToArray();

            var requestId = _ticketRepository.Add(journeyId, results);

            return new TicketResult
            {
                Id = requestId,
                Tickets = results,
                TotalPrice = results.Sum(e=> e.Price)
            };
        }

        [HttpGet("Buy")]
        public void BuyTickets(Guid journeyId)
        {
            _ticketRepository.Persist(journeyId);
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
                return new EmptyResult();

            var qrGenerator = new QRCodeGenerator();
            var qrCodeInfo = qrGenerator.CreateQrCode(ticket.BarcodeData, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeInfo);
            var qrBitmap = qrCode.GetGraphic(60);
            byte[] bitmapArray = qrBitmap.BitmapToByteArray();

            return File(bitmapArray, "image/png");
        }


        [HttpGet("CurrentBarcode")]
        public IActionResult GetCurrentBarcode(Guid journeyId, int size = 10)
        {
            var ticket = _ticketRepository.GetForCurrentStage(journeyId, DateTime.Now);

            if (ticket == null)
                return new EmptyResult();

            var qrGenerator = new QRCodeGenerator();
            var qrCodeInfo = qrGenerator.CreateQrCode(ticket.BarcodeData, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeInfo);
            var qrBitmap = qrCode.GetGraphic(size);
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