import { combineReducers } from '@reduxjs/toolkit';

import authenticationReducer from './reducer/authentication';

export const reducer = combineReducers({
  authentication: authenticationReducer
});

