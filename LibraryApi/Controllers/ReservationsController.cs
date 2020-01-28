using LibraryApi.Domain;
using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LibraryApi.Domain.Reservation;

namespace LibraryApi.Controllers
{
    public class ReservationsController : Controller
    {
        LibraryDataContext Context;
        ISendMessagesToTheReservationProcessor reservationProcessor;

        public ReservationsController(LibraryDataContext context, ISendMessagesToTheReservationProcessor reservationProcessor)
        {
            Context = context;
            this.reservationProcessor = reservationProcessor;
        }

        [HttpPost("/reservations")]
        [ValidateModel]
        public async Task<ActionResult> AddAReservation([FromBody] PostReservationRequest request)
        {
            var reservation = new Reservation
            {
                For = request.For,
                Books = string.Join(',', request.Books),
                ReservationCreated = DateTime.Now,
                Status = ReservationStatus.Pending
            };

            Context.Reservations.Add(reservation);
            await Context.SaveChangesAsync();
            GetReservationItemResponse response = MapIt(reservation);

            reservationProcessor.SendForProcessing(response);

            return CreatedAtRoute("reservations#getbyid", new { id = response.Id }, response);
        }

        [HttpGet("/reservations/{id:int}", Name = "reservations#getbyid")]
        public async Task<ActionResult<GetReservationItemResponse>> GetById(int id)
        {
            var reservation = await Context.Reservations
                .Where(r => r.Id == id)
                .SingleOrDefaultAsync();
           
            return this.Maybe(MapIt(reservation));
        }

        [HttpGet("/reservations")]
        public async Task<ActionResult<Collection<GetReservationItemResponse>>> GetAllReservations()
        {
            var reservations = await Context.Reservations.ToListAsync();

            var response = new Collection<GetReservationItemResponse>
            {
                Data = reservations.Select(r => MapIt(r)).ToList()
            };
            return Ok(response);

        }

        [HttpGet("/reservations/pending")]
        public async Task<ActionResult<Collection<GetReservationItemResponse>>> GetAllPendingReservations()
        {
            var reservations = await Context.Reservations
                .Where(r => r.Status == ReservationStatus.Pending)
                .ToListAsync();

            var response = new Collection<GetReservationItemResponse>
            {
                Data = reservations.Select(r => MapIt(r)).ToList()
            };
            return Ok(response);
        }

        [HttpPost("/reservations/approved")]
        [ValidateModel]
        public async Task<ActionResult> ApproveReservation([FromBody] GetReservationItemResponse reservation)
        {
            var storedReservation = await Context.Reservations.SingleOrDefaultAsync(r => r.Id == reservation.Id);
            if(storedReservation == null)
            {
                return BadRequest();
            }
            storedReservation.Status = ReservationStatus.Approved;
            await Context.SaveChangesAsync();
            return Accepted();
        }

        [HttpGet("/reservations/approved")]
        public async Task<ActionResult<Collection<GetReservationItemResponse>>> GetAllApprovedReservations()
        {
            var reservations = await Context.Reservations
                .Where(r => r.Status == ReservationStatus.Approved)
                .ToListAsync();

            var response = new Collection<GetReservationItemResponse>
            {
                Data = reservations.Select(r => MapIt(r)).ToList()
            };
            return Ok(response);
        }

        private GetReservationItemResponse MapIt(Reservation reservation)
        {
            var response = new GetReservationItemResponse
            {
                Id = reservation.Id,
                For = reservation.For,
                ReservationCreated = DateTime.Now,
                Status = reservation.Status,
                Books = reservation.Books.Split(',')
                    .Select(id => Url.ActionLink("GetBookById", "Books", new { id = id }))
                    .ToList()
            };

            return response;
        }


    }
}
