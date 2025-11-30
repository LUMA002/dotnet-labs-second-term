import { useEffect, useState } from 'react';
import { ticketApi } from '@/services/ticketApi';
import type { TicketInfoResponseDto } from '@/types';
import LoadingSpinner from '@/components/common/LoadingSpinner';
import ErrorAlert from '@/components/common/ErrorAlert';

export default function TicketsList() {
    const [tickets, setTickets] = useState<TicketInfoResponseDto[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        loadTickets();
    }, []);

    const loadTickets = async () => {
        try {
            setLoading(true);
            setError(null);
            const data = await ticketApi.getAll();
            setTickets(data);
        } catch (err) {
            setError('Failed to load tickets');
            console.error('Load tickets error:', err);
        } finally {
            setLoading(false);
        }
    };

    if (loading) return <LoadingSpinner message="Loading tickets..." />;

    return (
        <div className="container-fluid mt-4">
            <h1>
                <i className="bi bi-ticket-perforated"></i> All Tickets
            </h1>

            {error && <ErrorAlert message={error} onDismiss={() => setError(null)} />}

            {tickets.length === 0 ? (
                <div className="alert alert-info">
                    <i className="bi bi-info-circle"></i> No tickets found.
                </div>
            ) : (
                <div className="row">
                    {tickets.map((ticket) => (
                        <div key={ticket.ticketId} className="col-md-6 col-lg-4 mb-4">
                            <div className="card h-100 shadow-sm">
                                <div className="card-header bg-primary text-white">
                                    <h5 className="mb-0">
                                        <i className="bi bi-train-front"></i> Train {ticket.trainNumber}
                                    </h5>
                                </div>
                                <div className="card-body">
                                    <p className="mb-2">
                                        <strong>
                                            <i className="bi bi-person"></i> Passenger:
                                        </strong>
                                        <br />
                                        {ticket.passengerName}
                                    </p>
                                    <p className="mb-2">
                                        <strong>
                                            <i className="bi bi-telephone"></i> Phone:
                                        </strong>
                                        <br />
                                        {ticket.passengerPhone}
                                    </p>
                                    <p className="mb-2">
                                        <strong>
                                            <i className="bi bi-geo-alt"></i> Destination:
                                        </strong>
                                        <br />
                                        {ticket.destination} ({ticket.distance} km)
                                    </p>
                                    <p className="mb-2">
                                        <strong>
                                            <i className="bi bi-calendar-event"></i> Departure:
                                        </strong>
                                        <br />
                                        {new Date(ticket.departureDateTime).toLocaleString()}
                                    </p>
                                    <p className="mb-2">
                                        <strong>
                                            <i className="bi bi-calendar-check"></i> Arrival:
                                        </strong>
                                        <br />
                                        {new Date(ticket.arrivalDateTime).toLocaleString()}
                                    </p>
                                    <hr />
                                    <p className="mb-1">
                                        <small className="text-muted">
                                            Base Price: ${ticket.basePrice.toFixed(2)}
                                        </small>
                                    </p>
                                    <p className="mb-1">
                                        <small className="text-muted">
                                            Wagon Surcharge: ${ticket.wagonSurcharge.toFixed(2)}
                                        </small>
                                    </p>
                                    <p className="mb-1">
                                        <small className="text-muted">
                                            Urgency Surcharge: ${ticket.urgencySurcharge.toFixed(2)}
                                        </small>
                                    </p>
                                    <h5 className="text-primary mt-2">
                                        <strong>Total: ${ticket.totalPrice.toFixed(2)}</strong>
                                    </h5>
                                </div>
                                <div className="card-footer text-muted">
                                    <small>
                                        {ticket.trainType} | Wagon {ticket.wagonNumber} ({ticket.wagonType})
                                    </small>
                                </div>
                            </div>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
}