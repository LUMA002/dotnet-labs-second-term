import api from './api';
import type {
    PassengerResponseDto,
    CreatePassengerRequestDto,
    UpdatePassengerRequestDto,
} from '@/types';

// Passenger API Methods

export const passengerApi = {
    /**
     * GET /api/passengers
     * get all passengers
     */
    getAll: async () => {
        const response = await api.get<PassengerResponseDto[]>('/passengers');
        return response.data;
    },

    /**
     * GET /api/passengers/{id}
     * pasanger by id
     */
    getById: async (id: string) => {
        const response = await api.get<PassengerResponseDto>(`/passengers/${id}`);
        return response.data;
    },

    /**
     * POST /api/passengers
     * create passenger
     */
    create: async (data: CreatePassengerRequestDto) => {
        const response = await api.post<PassengerResponseDto>('/passengers', data);
        return response.data;
    },

    /**
     * PUT /api/passengers/{id}
     * update passenger
     */
    update: async (id: string, data: UpdatePassengerRequestDto) => {
        await api.put(`/passengers/${id}`, data);
    },

    /**
     * DELETE /api/passengers/{id}
     * Delete passenger
     */
    delete: async (id: string) => {
        await api.delete(`/passengers/${id}`);
    },
};