import React from 'react'
import ReactDOM from 'react-dom/client'
import { BrowserRouter as Router } from "react-router-dom"
import App from './app'
import AppProviders from './components/contexts/app-providers'
import './index.css'


ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
    <React.StrictMode>
        <Router>
            <AppProviders>
                <App />
            </AppProviders>
        </Router>
    </React.StrictMode>
)