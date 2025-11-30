interface LoadingSpinnerProps {
    message?: string;
}

export default function LoadingSpinner({ message = 'Loading...' }: LoadingSpinnerProps) {
    return (
        <div className="text-center p-5">
            <div className="spinner-border text-primary" role="status">
                <span className="visually-hidden">{message}</span>
            </div>
            <p className="mt-3 text-muted">{message}</p>
        </div>
    );
}