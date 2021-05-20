import 'bootstrap/dist/css/bootstrap.css';
import '@fortawesome/fontawesome-pro/css/all.min.css';
import { initializeIcons } from '@uifabric/icons';
import React from 'react';
import ReactDOM from 'react-dom';

import { App } from './app/App';

import './i18n';
import './index.css';
import { BrowserRouter } from 'react-router-dom';

initializeIcons();

ReactDOM.render(
    <React.StrictMode>
        <BrowserRouter>
            <App />
        </BrowserRouter>
    </React.StrictMode>,
    document.getElementById('root')
);
