import { Outlet } from 'react-router-dom';
import Navbar from './Navbar';
import Footer from './Footer';

export default function Layout() {
    return (
        <div className="d-flex flex-column min-vh-100">
            <header>
                <Navbar />
            </header>

            <main className="flex-grow-1">
                <div className="container-fluid px-4 py-4">
                    <Outlet /> {/* Nested routes will be rendered here */}
                </div>
            </main>

            <Footer />
        </div>
    );
}