interface ErrorAlertProps {
    message: string;
    onDismiss?: () => void;
}

export default function ErrorAlert({ message, onDismiss }: ErrorAlertProps) {
    return (
        <div className="alert alert-danger alert-dismissible fade show" role="alert">
            <i className="bi bi-exclamation-triangle-fill me-2"></i>
            {message}
            {onDismiss && (
                <button
                    type="button"
                    className="btn-close"
                    onClick={onDismiss}
                    aria-label="Close"
                ></button>
            )}
        </div>
    );
}