import { useState, type FormEvent } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { passengerApi } from '@/services/passengerApi';
import type { CreatePassengerRequestDto } from '@/types';
import ErrorAlert from '@/components/common/ErrorAlert';

export default function PassengerCreate() {
    const navigate = useNavigate();
    const [formData, setFormData] = useState<CreatePassengerRequestDto>({
        firstName: '',
        lastName: '',
        middleName: '',
        address: '',
        phoneNumber: '',
    });
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        setLoading(true);
        setError(null);

        try {
            const data: CreatePassengerRequestDto = {
                firstName: formData.firstName.trim(),
                lastName: formData.lastName.trim(),
                middleName: formData.middleName?.trim() || undefined,
                address: formData.address?.trim() || undefined,
                phoneNumber: formData.phoneNumber?.trim() || undefined,
            };

            await passengerApi.create(data);
            navigate('/passengers'); // Redirect to passengers list after creation
        } catch (err: any) {
            setError(err.response?.data?.message || 'Failed to create passenger');
            console.error('Create error:', err);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="container mt-4">
            <div className="row justify-content-center">
                <div className="col-md-8">
                    <div className="card shadow">
                        <div className="card-header bg-primary text-white">
                            <h4 className="mb-0">
                                <i className="bi bi-person-plus"></i> Add New Passenger
                            </h4>
                        </div>
                        <div className="card-body">
                            {error && <ErrorAlert message={error} onDismiss={() => setError(null)} />}

                            <form onSubmit={handleSubmit}>
                                <div className="row mb-3">
                                    <div className="col-md-6">
                                        <label htmlFor="firstName" className="form-label">
                                            First Name <span className="text-danger">*</span>
                                        </label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            id="firstName"
                                            value={formData.firstName}
                                            onChange={(e) =>
                                                setFormData({ ...formData, firstName: e.target.value })
                                            }
                                            required
                                        />
                                    </div>
                                    <div className="col-md-6">
                                        <label htmlFor="lastName" className="form-label">
                                            Last Name <span className="text-danger">*</span>
                                        </label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            id="lastName"
                                            value={formData.lastName}
                                            onChange={(e) =>
                                                setFormData({ ...formData, lastName: e.target.value })
                                            }
                                            required
                                        />
                                    </div>
                                </div>

                                <div className="mb-3">
                                    <label htmlFor="middleName" className="form-label">
                                        Middle Name <span className="text-muted">(optional)</span>
                                    </label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        id="middleName"
                                        value={formData.middleName}
                                        onChange={(e) =>
                                            setFormData({ ...formData, middleName: e.target.value })
                                        }
                                    />
                                </div>

                                <div className="mb-3">
                                    <label htmlFor="phoneNumber" className="form-label">
                                        Phone Number <span className="text-muted">(+380XXXXXXXXX)</span>
                                    </label>
                                    <input
                                        type="tel"
                                        className="form-control"
                                        id="phoneNumber"
                                        value={formData.phoneNumber}
                                        onChange={(e) =>
                                            setFormData({ ...formData, phoneNumber: e.target.value })
                                        }
                                        placeholder="+380501234567"
                                    />
                                </div>

                                <div className="mb-3">
                                    <label htmlFor="address" className="form-label">
                                        Address <span className="text-muted">(optional)</span>
                                    </label>
                                    <textarea
                                        className="form-control"
                                        id="address"
                                        rows={3}
                                        value={formData.address}
                                        onChange={(e) =>
                                            setFormData({ ...formData, address: e.target.value })
                                        }
                                    ></textarea>
                                </div>

                                <div className="d-flex justify-content-between">
                                    <Link to="/passengers" className="btn btn-secondary">
                                        <i className="bi bi-arrow-left"></i> Cancel
                                    </Link>
                                    <button type="submit" className="btn btn-primary" disabled={loading}>
                                        {loading ? (
                                            <>
                                                <span className="spinner-border spinner-border-sm me-2"></span>
                                                Creating...
                                            </>
                                        ) : (
                                            <>
                                                <i className="bi bi-save"></i> Create Passenger
                                            </>
                                        )}
                                    </button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}