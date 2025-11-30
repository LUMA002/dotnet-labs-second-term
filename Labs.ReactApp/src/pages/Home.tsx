import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { passengerApi } from '@/services/passengerApi';
import { ticketApi } from '@/services/ticketApi';
import type { TicketInfoResponseDto } from '@/types';
import LoadingSpinner from '@/components/common/LoadingSpinner';
import ErrorAlert from '@/components/common/ErrorAlert';

export default function Home() {
    const [totalPassengers, setTotalPassengers] = useState(0);
    const [totalTickets, setTotalTickets] = useState(0);
    const [totalRevenue, setTotalRevenue] = useState(0);
    const [recentTickets, setRecentTickets] = useState<TicketInfoResponseDto[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        loadDashboardData();
    }, []);

    const loadDashboardData = async () => {
        try {
            setLoading(true);
            setError(null);

            const [passengers, tickets] = await Promise.all([
                passengerApi.getAll(),
                ticketApi.getAll(),
            ]);

            setTotalPassengers(passengers.length);
            setTotalTickets(tickets.length);
            setTotalRevenue(tickets.reduce((sum, t) => sum + t.totalPrice, 0));
            setRecentTickets(
                tickets
                    .sort((a, b) => new Date(b.departureDateTime).getTime() - new Date(a.departureDateTime).getTime())
                    .slice(0, 5)
            );
        } catch (err) {
            setError('Failed to load dashboard data');
            console.error('Dashboard error:', err);
        } finally {
            setLoading(false);
        }
    };

    if (loading) return <LoadingSpinner message="Loading dashboard..." />;
    if (error) return <ErrorAlert message={error} onDismiss={() => setError(null)} />;

    return (
        <div className="container mt-4">
            {/* Hero Section */}
            <div className="bg-primary text-white rounded p-5 mb-4">
                <div className="row align-items-center">
                    <div className="col-md-8">
                        <h1 className="display-4">
                            <i className="bi bi-train-front-fill"></i> Ticket Reservation System
                        </h1>
                        <p className="lead">Welcome to the modern train ticket booking platform</p>
                        <p className="mb-4">Book tickets, manage passengers, and track your journeys with ease.</p>
                        <div className="btn-group">
                            <Link to="/passengers/create" className="btn btn-light btn-lg">
                                <i className="bi bi-person-plus"></i> New Passenger
                            </Link>
                            <Link to="/tickets" className="btn btn-outline-light btn-lg">
                                <i className="bi bi-ticket-perforated"></i> View Tickets
                            </Link>
                        </div>
                    </div>
                    <div className="col-md-4 text-center">
                        <i className="bi bi-train-front" style={{ fontSize: '120px', opacity: 0.3 }}></i>
                    </div>
                </div>
            </div>

            {/* Statistics Cards */}
            <div className="row mb-4">
                <div className="col-md-4">
                    <div className="card text-white bg-info shadow">
                        <div className="card-body">
                            <div className="d-flex justify-content-between align-items-center">
                                <div>
                                    <h6 className="card-subtitle mb-2 text-white-50">Total Passengers</h6>
                                    <h2 className="card-title mb-0">{totalPassengers}</h2>
                                </div>
                                <div>
                                    <i className="bi bi-people-fill" style={{ fontSize: '48px', opacity: 0.5 }}></i>
                                </div>
                            </div>
                        </div>
                        <div className="card-footer bg-transparent border-top border-white">
                            <Link to="/passengers" className="text-white text-decoration-none">
                                View all <i className="bi bi-arrow-right"></i>
                            </Link>
                        </div>
                    </div>
                </div>

                <div className="col-md-4">
                    <div className="card text-white bg-success shadow">
                        <div className="card-body">
                            <div className="d-flex justify-content-between align-items-center">
                                <div>
                                    <h6 className="card-subtitle mb-2 text-white-50">Total Tickets</h6>
                                    <h2 className="card-title mb-0">{totalTickets}</h2>
                                </div>
                                <div>
                                    <i className="bi bi-ticket-perforated-fill" style={{ fontSize: '48px', opacity: 0.5 }}></i>
                                </div>
                            </div>
                        </div>
                        <div className="card-footer bg-transparent border-top border-white">
                            <Link to="/tickets" className="text-white text-decoration-none">
                                View all <i className="bi bi-arrow-right"></i>
                            </Link>
                        </div>
                    </div>
                </div>

                <div className="col-md-4">
                    <div className="card text-white bg-warning shadow">
                        <div className="card-body">
                            <div className="d-flex justify-content-between align-items-center">
                                <div>
                                    <h6 className="card-subtitle mb-2 text-white-50">Total Revenue</h6>
                                    <h2 className="card-title mb-0">${totalRevenue.toFixed(2)}</h2>
                                </div>
                                <div>
                                    <i className="bi bi-cash-stack" style={{ fontSize: '48px', opacity: 0.5 }}></i>
                                </div>
                            </div>
                        </div>
                        <div className="card-footer bg-transparent border-top border-white">
                            <span className="text-white">From {totalTickets} bookings</span>
                        </div>
                    </div>
                </div>
            </div>

            {/* Recent Tickets */}
            <div className="card shadow">
                <div className="card-header bg-dark text-white">
                    <h4 className="mb-0">
                        <i className="bi bi-clock-history"></i> Recent Bookings
                    </h4>
                </div>
                <div className="card-body">
                    {recentTickets.length > 0 ? (
                        <div className="table-responsive">
                            <table className="table table-hover">
                                <thead>
                                    <tr>
                                        <th>Passenger</th>
                                        <th>Train</th>
                                        <th>Destination</th>
                                        <th>Departure</th>
                                        <th className="text-end">Price</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {recentTickets.map((ticket) => (
                                        <tr key={ticket.ticketId}>
                                            <td>
                                                <i className="bi bi-person-circle"></i> {ticket.passengerName}
                                            </td>
                                            <td>
                                                <i className="bi bi-train-front"></i> {ticket.trainNumber} ({ticket.trainType})
                                            </td>
                                            <td>
                                                <i className="bi bi-geo-alt"></i> {ticket.destination}
                                            </td>
                                            <td>
                                                <i className="bi bi-calendar-event"></i>{' '}
                                                {new Date(ticket.departureDateTime).toLocaleString()}
                                            </td>
                                            <td className="text-end">
                                                <strong className="text-success">${ticket.totalPrice.toFixed(2)}</strong>
                                            </td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                    ) : (
                        <div className="alert alert-info mb-0">
                            <i className="bi bi-info-circle"></i> No recent bookings found
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
}