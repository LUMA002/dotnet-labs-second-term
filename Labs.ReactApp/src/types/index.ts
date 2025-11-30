// Passenger DTOs (Correspond to C# models)
export interface PassengerResponseDto {
    id: string;
    firstName: string;
    lastName: string;
    middleName?: string;
    address?: string;
    phoneNumber?: string;
}


export interface CreatePassengerRequestDto {
    firstName: string;
    lastName: string;
    middleName?: string;
    address?: string;
    phoneNumber?: string;
}

export interface UpdatePassengerRequestDto extends CreatePassengerRequestDto {
    passengerId: string;
}


// Ticket DTOs
export interface TicketInfoResponseDto {
    ticketId: string;
    passengerName: string;
    passengerPhone: string;
    trainNumber: string;
    trainType: string;
    wagonNumber: string;
    wagonType: string;
    destination: string;
    distance: number;
    departureDateTime: string;
    arrivalDateTime: string;
    basePrice: number;
    wagonSurcharge: number;
    urgencySurcharge: number;
    totalPrice: number;
}


// API Error Response
export interface ApiErrorResponse {
    status: number;
    message: string;
    errors?: Record<string, string[]>; // validation errors from server (compatibility with server format)
    traceId?: string;
}