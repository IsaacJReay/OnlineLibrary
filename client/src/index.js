import React from 'react';
import { createRoot } from 'react-dom/client';
import { Provider } from 'react-redux';
import { configureStore } from '@reduxjs/toolkit';

import App from './App';
import './index.css';
import { reducer } from './store'

const store = configureStore({ reducer });

const root = createRoot(document.getElementById('root'));
root.render(
  <Provider store={ store }>
    <App />
  </Provider>
);