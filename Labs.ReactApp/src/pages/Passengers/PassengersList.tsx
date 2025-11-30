import { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { passengerApi } from '@/services/passengerApi';
import type { PassengerResponseDto } from '@/types';
import LoadingSpinner from '@/components/common/LoadingSpinner';
import ErrorAlert from '@/components/common/ErrorAlert';

export default function PassengersList() {
    const [passengers, setPassengers] = useState<PassengerResponseDto[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        loadPassengers();
    }, []);

    const loadPassengers = async () => {
        try {
            setLoading(true);
            setError(null);
            const data = await passengerApi.getAll();
            setPassengers(data);
        } catch (err) {
            setError('Failed to load passengers');
            console.error('Load passengers error:', err);
        } finally {
            setLoading(false);
        }
    };

    const handleDelete = async (id: string, name: string) => {
        if (!window.confirm(`Are you sure you want to delete ${name}?`)) {
            return;
        }

        try {
            await passengerApi.delete(id);
            // update list after delete operation
            setPassengers(passengers.filter((p) => p.id !== id));
        } catch (err) {
            setError('Failed to delete passenger');
            console.error('Delete error:', err);
        }
    };

    if (loading) return <LoadingSpinner message="Loading passengers..." />;

    return (
        <div className="container-fluid mt-4">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h1>
                    <i className="bi bi-people-fill"></i> Passengers
                </h1>
                <Link to="/passengers/create" className="btn btn-primary">
                    <i className="bi bi-person-plus"></i> Add New Passenger
                </Link>
            </div>

            {error && <ErrorAlert message={error} onDismiss={() => setError(null)} />}

            {passengers.length === 0 ? (
                <div className="alert alert-info">
                    <i className="bi bi-info-circle"></i> No passengers found.{' '}
                    <Link to="/passengers/create">Create one now</Link>
                </div>
            ) : (
                <div className="table-responsive">
                    <table className="table table-hover table-striped">
                        <thead className="table-dark">
                            <tr>
                                <th>Name</th>
                                <th>Phone</th>
                                <th>Address</th>
                                <th className="text-end">Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {passengers.map((passenger) => (
                                <tr key={passenger.id}>
                                    <td>
                                        <i className="bi bi-person-circle me-2"></i>
                                        {passenger.lastName} {passenger.firstName}{' '}
                                        {passenger.middleName || ''}
                                    </td>
                                    <td>
                                        {passenger.phoneNumber ? (
                                            <>
                                                <i className="bi bi-telephone"></i> {passenger.phoneNumber}
                                            </>
                                        ) : (
                                            <span className="text-muted">N/A</span>
                                        )}
                                    </td>
                                    <td>
                                        {passenger.address ? (
                                            <>
                                                <i className="bi bi-geo-alt"></i> {passenger.address}
                                            </>
                                        ) : (
                                            <span className="text-muted">N/A</span>
                                        )}
                                    </td>
                                    <td className="text-end">
                                        <div className="btn-group">
                                            <button
                                                className="btn btn-sm btn-info"
                                                onClick={() => navigate(`/passengers/${passenger.id}/edit`)}
                                            >
                                                <i className="bi bi-pencil"></i> Edit
                                            </button>
                                            <button
                                                className="btn btn-sm btn-danger"
                                                onClick={() =>
                                                    handleDelete(
                                                        passenger.id,
                                                        `${passenger.firstName} ${passenger.lastName}`
                                                    )
                                                }
                                            >
                                                <i className="bi bi-trash"></i> Delete
                                            </button>
                                        </div>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            )}
        </div>
    );
}