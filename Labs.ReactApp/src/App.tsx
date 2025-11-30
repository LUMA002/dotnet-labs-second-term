import { Routes, Route } from 'react-router-dom';
import Layout from '@/components/layout/Layout';
import Home from '@/pages/Home';
import PassengersList from '@/pages/Passengers/PassengersList';
import PassengerCreate from '@/pages/Passengers/PassengerCreate';
import PassengerEdit from '@/pages/Passengers/PassengerEdit';
import TicketsList from '@/pages/Tickets/TicketsList';
//import './App.css'

function App() {
    return (
        <Routes>
            <Route path="/" element={<Layout />}>
                {/* Nested routes */}
                <Route index element={<Home />} /> {/* "/" */}
                <Route path="passengers" element={<PassengersList />} /> {/* "/passengers" */}
                <Route path="passengers/create" element={<PassengerCreate />} /> {/* "/passengers/create" */}
                <Route path="passengers/:id/edit" element={<PassengerEdit />} /> {/* "/passengers/123/edit" */}
                <Route path="tickets" element={<TicketsList />} /> {/* "/tickets" */}
            </Route>
        </Routes>
    )
}

export default App
