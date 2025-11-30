import api from './api';
import type { TicketInfoResponseDto } from '@/types';

// Ticket API Methods

export const ticketApi = {
    /**
     * GET /api/tickets
     * get all tickets
     */
    getAll: async () => {
        const response = await api.get<TicketInfoResponseDto[]>('/tickets');
        return response.data;
    },
};