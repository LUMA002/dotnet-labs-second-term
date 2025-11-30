import { useState, useEffect, type FormEvent } from 'react';
import { useNavigate, useParams, Link } from 'react-router-dom';
import { passengerApi } from '@/services/passengerApi';
import type { UpdatePassengerRequestDto } from '@/types';
import LoadingSpinner from '@/components/common/LoadingSpinner';
import ErrorAlert from '@/components/common/ErrorAlert';

export default function PassengerEdit() {
    const { id } = useParams<{ id: string }>(); // Get ID of passenger by URL
    const navigate = useNavigate();
    const [formData, setFormData] = useState<UpdatePassengerRequestDto | null>(null);
    const [loading, setLoading] = useState(true);
    const [saving, setSaving] = useState(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (id) {
            loadPassenger(id);
        }
    }, [id]);

    const loadPassenger = async (passengerId: string) => {
        try {
            setLoading(true);
            const data = await passengerApi.getById(passengerId);
            setFormData({
                passengerId: data.id,
                firstName: data.firstName,
                lastName: data.lastName,
                middleName: data.middleName || '',
                address: data.address || '',
                phoneNumber: data.phoneNumber || '',
            });
        } catch (err) {
            setError('Failed to load passenger');
            console.error('Load error:', err);
        } finally {
            setLoading(false);
        }
    };

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        if (!formData || !id) return;

        setSaving(true);
        setError(null);

        try {
            const data: UpdatePassengerRequestDto = {
                passengerId: formData.passengerId,
                firstName: formData.firstName.trim(),
                lastName: formData.lastName.trim(),
                middleName: formData.middleName?.trim() || undefined,
                address: formData.address?.trim() || undefined,
                phoneNumber: formData.phoneNumber?.trim() || undefined,
            };

            await passengerApi.update(id, data);
            navigate('/passengers');
        } catch (err: any) {
            setError(err.response?.data?.message || 'Failed to update passenger');
            console.error('Update error:', err);
        } finally {
            setSaving(false);
        }
    };

    if (loading) return <LoadingSpinner message="Loading passenger..." />;
    if (!formData) return <ErrorAlert message="Passenger not found" />;

    return (
        <div className="container mt-4">
            <div className="row justify-content-center">
                <div className="col-md-8">
                    <div className="card shadow">
                        <div className="card-header bg-warning text-dark">
                            <h4 className="mb-0">
                                <i className="bi bi-pencil"></i> Edit Passenger
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
                                        Middle Name
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
                                        Phone Number
                                    </label>
                                    <input
                                        type="tel"
                                        className="form-control"
                                        id="phoneNumber"
                                        value={formData.phoneNumber}
                                        onChange={(e) =>
                                            setFormData({ ...formData, phoneNumber: e.target.value })
                                        }
                                    />
                                </div>

                                <div className="mb-3">
                                    <label htmlFor="address" className="form-label">
                                        Address
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
                                    <button type="submit" className="btn btn-warning" disabled={saving}>
                                        {saving ? (
                                            <>
                                                <span className="spinner-border spinner-border-sm me-2"></span>
                                                Saving...
                                            </>
                                        ) : (
                                            <>
                                                <i className="bi bi-save"></i> Save Changes
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