import { authenticationType } from '../../Constant'

const initialState = {
  loginModalVisible: false,
  loginButtonDisable: true,
  loginValidations: {
    email: false,
    password: false
  },
  registerValidations: {
    username: false,
    email: false,
    password: false,
    passwordConfirmation: false
  },
};

export default function authenticationReducer(state = initialState, action){
  switch(action.type){
    case authenticationType.SET_LOGIN_MODAL_VISIBLE_TOGGLE:
      return {
        ...state,
        loginModalVisible: !state.loginModalVisible
      };

    case authenticationType.SET_LOGIN_BUTTON_DISABLE:
      return {
        ...state,
        loginButtonDisable: action.payload
      };

    case authenticationType.SET_LOGIN_VALIDATION_EMAIL:
      return {
        ...state,
        loginValidations: {
          ...state.loginValidations,
          email: action.payload
        }
      };

    case authenticationType.SET_LOGIN_VALIDATION_PASSWORD:
      return {
        ...state,
        loginValidations: {
          ...state.loginValidations,
          password: action.payload
        }
      };

    case authenticationType.SET_REGISTER_VALIDATION_USERNAME:
      return {
        ...state,
        registerValidations: {
          ...state.registerValidations,
          username: action.payload
        }
      };

    case authenticationType.SET_REGISTER_VALIDATION_EMAIL:
      return {
        ...state,
        registerValidations: {
          ...state.registerValidations,
          email: action.payload
        }
      };

      case authenticationType.SET_REGISTER_VALIDATION_PASSWORD:
        return {
          ...state,
          registerValidations: {
            ...state.registerValidations,
            password: action.payload
          }
        };
    
      default:
        return state
  }
}