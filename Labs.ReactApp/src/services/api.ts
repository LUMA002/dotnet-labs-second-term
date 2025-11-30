import axios, { AxiosError } from 'axios';
import type { ApiErrorResponse } from '@/types';

// Axios instance with basic configuration

const api = axios.create({
    baseURL: '/api', // proxy base URL via Vite config
    headers: {
        'Content-Type': 'application/json',
    },
    timeout: 10000,
});

// Request interceptor (log the requests)

api.interceptors.request.use(
    (config) => {
        console.log(`[API Request] ${config.method?.toUpperCase()} ${config.url}`);
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);


// Response interceptor (errors processing)
api.interceptors.response.use(
    (response) => {
        console.log(`[API Response] ${response.status} ${response.config.url}`);
        return response;
    },
    (error: AxiosError<ApiErrorResponse>) => {
        if (error.response) {
            // server responded with an error
            console.error('[API Error]', error.response.data);
        } else if (error.request) {
            // no response received
            console.error('[Network Error]', error.message);
        } else {
            console.error('[Request Setup Error]', error.message);
        }
        return Promise.reject(error);
    }
);

export default api;