using Cinema.Showtimes.Api.Application.Commands;
using Cinema.Showtimes.Api.Application.Exceptions;
using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Repositories;
using Cinema.Showtimes.Api.Tests.TestHelpers;
using NSubstitute;
using Xunit;
using static Cinema.Showtimes.Api.Tests.Application.Commands.ConfirmReservationPaymentCommandHandlerTestsHarness;

namespace Cinema.Showtimes.Api.Tests.Application.Commands;

public class ConfirmReservationPaymentCommandHandlerTests
{
    [Fact]
    public async Task TicketExist_ConfirmsPayment()
    {
        var ticketsRepository = CreateTicketRepository(DefaultTicketId, DefaultTicket);
        var paymentCommandHandler = CreateSut(ticketsRepository);

        await paymentCommandHandler.Handle(DefaultPaymentCommand, CancellationToken.None);

        await ticketsRepository.Received(1)
            .ConfirmPaymentAsync(Arg.Is<TicketEntity>(t => t == DefaultTicket), CancellationToken.None);
    }

    [Fact]
    public async Task TicketDoesNotExist_ThrowsException()
    {
        var ticketsRepository = CreateTicketRepository(DefaultTicketId);
        var paymentCommandHandler = CreateSut(ticketsRepository);

        Func<Task> act = async () => await paymentCommandHandler.Handle(DefaultPaymentCommand, CancellationToken.None);

        var exception = await Assert.ThrowsAsync<TicketNotFoundException>(act);
        Assert.Equal($"Ticket not found: Reservation ID '{DefaultTicketId}'.", exception.Message);
    }
}

public static class ConfirmReservationPaymentCommandHandlerTestsHarness
{
    public static readonly Guid DefaultTicketId = Guid.NewGuid();
    public static readonly TicketEntity DefaultTicket = TicketEntityBuilder.Create().WithId(DefaultTicketId).Build();

    public static readonly ConfirmReservationPaymentCommand DefaultPaymentCommand = new(DefaultTicketId);

    public static ConfirmReservationPaymentCommandHandler CreateSut(ITicketsRepository? ticketsRepository = null) =>
        new(ticketsRepository ?? CreateTicketRepository(DefaultTicketId, DefaultTicket));

    public static ITicketsRepository CreateTicketRepository(Guid reservationId, TicketEntity? ticket = null)
    {
        var ticketsRepository = Substitute.For<ITicketsRepository>();
        ticketsRepository.GetAsync(reservationId, CancellationToken.None).Returns(ticket);
        return ticketsRepository;
    }
}